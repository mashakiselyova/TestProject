import React, { useEffect, useState } from 'react';
import Post from './Post'

function Posts() {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        fetch("/posts/getAllPosts", { method: 'get', mode: 'no-cors' })
            .then((response) => {
                response.json().then((data) => {
                    data.forEach((element) => {
                        element.createDate = element.createDate.replace('T', ' ').substring(0, 16);
                    })
                    setPosts(data);
                });
            });
    }, [])

    return (
        <div>
            {posts.map((post) => (
                <Post key={post.id} {...post} />
            ))}
        </div>
    );
}

export default Posts;