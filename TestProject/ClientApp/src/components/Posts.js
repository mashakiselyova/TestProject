import React, { useEffect, useState } from 'react';
import Post from './Post'

function Posts({ userLoggedIn, userProfile, url }) {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
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
                <Post key={post.id} post={post} userLoggedIn={userLoggedIn} userProfile={userProfile} />
            ))}
        </div>
    );
}

export default Posts;