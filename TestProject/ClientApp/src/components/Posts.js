import React, { useEffect, useState } from 'react';
import Post from './Post'

function Posts({ userLoggedIn, userProfile }) {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        fetch("/posts/getAllPosts", { method: 'get', mode: 'no-cors' })
            .then((response) => {
                response.json().then((data) => {
                    setPosts(data);
                });
            });
    }, [])

    return (
        <div>
            {posts.map((post) => (
                <Post key={post.id} post={post} userLoggedIn={userLoggedIn} userProfile={userProfile} />
            ))}
        </div>
    );
}

export default Posts;