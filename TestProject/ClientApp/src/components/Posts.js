import React, { useEffect, useState } from 'react';
import Post from './Post'

function Posts({ userProfile, filterByCurrentUser = false }) {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        const url = filterByCurrentUser ? `/posts/getPosts/${userProfile.id}` : "/posts/getPosts";
        fetch(url, { method: 'get', mode: 'no-cors' })
            .then((response) => {
                response.json().then((data) => {
                    setPosts(data);
                });
            });
    }, [])

    return (
        <div>
            {posts.map((post) => (
                <Post key={post.id} post={post} userProfile={userProfile} />
            ))}
        </div>
    );
}

export default Posts;