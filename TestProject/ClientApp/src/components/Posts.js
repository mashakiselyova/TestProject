import React, { useEffect, useState } from 'react';
import Post from './Post'

function Posts({ userLoggedIn, userProfile, userPosts }) {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        const url = userPosts ? `/posts/getPosts/${userProfile.id}` : "/posts/getPosts";
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