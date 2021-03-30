import React, { useEffect, useState } from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';
import Rating from "./Rating";

function PostPage({ postId, userProfile, getUpdatedRating }) {
    const [post, setPost] = useState({
        id: 0,
        title: "",
        content: "",
        createDate: "",
        updateDate: "",
        updateRating: "",
        author: { firstName: "", lastName: "" },
        selectedRating: 0,
        totalRating: 0
    });

    useEffect(() => {
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
    }, [])

    async function updateRating() {
        const rating = await getUpdatedRating(post.id);
        let newPost = {...post};
        newPost.selectedRating = rating.ratingByCurrentUser;
        newPost.totalRating = rating.totalRating;
        setPost(newPost);
    }


    const dateUpdated = post.createDate !== post.updateDate;

    return (
        <div className="row">
            <div className="card col-8 offset-2">
                <h5 className="card-header">{post.author.firstName + ' ' + post.author.lastName}</h5>
                <div className="card-body row">
                    <div className="col-1">
                        <Rating
                            postId={post.id}
                            userProfile={userProfile}
                            authorId={post.author.id}
                            selectedRating={post.selectedRating}
                            totalRating={post.totalRating}
                            updateRating={updateRating}
                        />
                    </div>
                    <div className="col">
                        <h5 className="card-title">{post.title}</h5>
                        <h6 className="card-subtitle mb-2 text-muted">{new Date(post.createDate).toLocaleDateString()}</h6>
                        {dateUpdated &&
                            < h6 className="card-subtitle mb-2 text-muted">Updated {new Date(post.updateDate).toLocaleDateString()}</h6>}
                        <p className="card-text">{post.content}</p>
                    </div>
                </div>
                
            </div>
        </div>
    );
}

export default PostPage;