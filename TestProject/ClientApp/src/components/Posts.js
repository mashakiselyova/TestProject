import React, { useEffect, useState } from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import Post from './Post'

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

    function handleUpdateRating(postId) {
        fetch(`/rating/get/${postId}`, { method: 'get' })
            .then((response) => {                
                response.json().then((data) => {
                    let newPosts = [...posts];
                    const index = newPosts.map(p => p.id).indexOf(postId);
                    newPosts[index].totalRating = data.totalRating;
                    newPosts[index].selectedRating = data.ratingByCurrentUser;
                    setPosts(newPosts);
                });
            })
            .catch(() => {
                NotificationManager.error("Couldn't get rating");
            });
    }

    return (
        <div>
            {posts.map((post) => (
                <Post
                    key={post.id}
                    post={post}
                    userProfile={userProfile}
                    updateRating={handleUpdateRating} />
            ))}
            <NotificationContainer />
        </div>
    );
}

export default Posts;