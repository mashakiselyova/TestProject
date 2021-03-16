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
                    setPosts(data);
                });
            }).catch(() => {
                NotificationManager.error("Couldn't get posts");
            });
    }, [userProfile, filterByCurrentUser])

    return (
        <div>
            {posts.map((post) => (
                <Post key={post.id} post={post} userProfile={userProfile} />
            ))}
            <NotificationContainer />
        </div>
    );
}

export default Posts;