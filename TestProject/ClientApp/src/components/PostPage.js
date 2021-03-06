﻿import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';
import Rating from "./Rating";
import CommentInput from "./CommentInput";
import { getRating } from '../services/RatingService';

function PostPage({ postId, currentUser }) {
    const [post, setPost] = useState();

    useEffect(() => {
        loadPost();
    }, [])

    async function updateRating() {
        try {
            const rating = await getRating(post.id);
            let newPost = { ...post };
            newPost.selectedRating = rating.ratingByCurrentUser;
            newPost.totalRating = rating.totalRating;
            setPost(newPost);
        }
        catch {
            NotificationManager.error("Couldn't update rating");
        }
    }

    function loadPost() {
        fetch(`posts/getRichPost/${postId}`, { mode: 'no-cors' })
            .then((response) => {
                if (response.ok) {
                    response.json().then((data) => {
                        setPost(data);
                    });
                }
                else {
                    NotificationManager.error("Couldn't load post");
                }
            })
            .catch(() => {
                NotificationManager.error("Couldn't load post");
            });
    }, [postId])

    return (
        <div className="row">
            {post &&
                <div className="card col-8 offset-2">
                    <Link to={`/user/profile/${post.author.id}`}>
                        <h5 className="card-header">{post.author.firstName + ' ' + post.author.lastName}</h5>
                    </Link>
                    <div className="card-body row">
                        <div className="col-1">
                            <Rating
                                postId={post.id}
                                currentUser={currentUser}
                                authorId={post.author.id}
                                selectedRating={post.selectedRating}
                                totalRating={post.totalRating}
                                updateRating={updateRating}
                            />
                        </div>
                        <div className="col">
                            <h5 className="card-title">{post.title}</h5>
                            <h6 className="card-subtitle mb-2 text-muted">{new Date(post.createDate).toLocaleDateString()}</h6>
                            {post.createDate !== post.updateDate &&
                                < h6 className="card-subtitle mb-2 text-muted">Updated {new Date(post.updateDate).toLocaleDateString()}</h6>}
                            <p className="card-text">{post.content}</p>
                        </div>
                    </div>
                </div>
                {userProfile.loggedIn
                    ? <CommentInput postId={post.id} updatePost={loadPost} />
                    : <div className="card">You need to sign in to leave comments</div>
                }                
                </div>}
            <NotificationContainer />
        </div>
    );
}

export default PostPage;