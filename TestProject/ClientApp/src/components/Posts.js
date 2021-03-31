﻿import React, { useEffect, useState } from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import Post from './Post';
import getRating from '../services/RatingService'; 

function Posts({ userProfile, filterByCurrentUser = false }) {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        const url = `/posts/getAll/${filterByCurrentUser ? userProfile.id : ''}`;
        fetch(url, { method: 'get', mode: 'no-cors' })
            .then((response) => {
                response.json().then((data) => {
                    data.sort((a, b) => {
                        return new Date(b.createDate) - new Date(a.createDate);
                    });
                    setPosts(data);
                });
            }).catch(() => {
                NotificationManager.error("Couldn't get posts");
            });
    }, [userProfile, filterByCurrentUser])

    async function updateRating(postId) {
        try {
            const rating = await getRating(postId);
            let newPosts = [...posts];
            const index = newPosts.map(p => p.id).indexOf(postId);
            newPosts[index].totalRating = rating.totalRating;
            newPosts[index].selectedRating = rating.ratingByCurrentUser;
            setPosts(newPosts);
        }
        catch {
            NotificationManager.error("Couldn't update rating");
        }        
    }

    return (
        <div>
            {posts.map((post) => (
                <Post
                    key={post.id}
                    post={post}
                    userProfile={userProfile}
                    updateRating={updateRating} />
            ))}
            <NotificationContainer />
        </div>
    );
}

export default Posts;