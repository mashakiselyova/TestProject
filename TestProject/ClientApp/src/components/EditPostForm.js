import React, { useState, useEffect } from 'react';
import PostForm from './PostForm';

function EditPostForm({ postId }) {
    const [post, setPost] = useState({ id: '', title: '', content: '' });

    useEffect(() => {
        fetch(`/posts/getPost/${postId}`, { method: 'get', mode: 'no-cors' })
            .then((response) => {
                response.json().then((data) => {
                    setPost(data);
                });
            });
    }, [])

    function handleTitleChange(value) {
        setPost({ ...post, title: value });
    };

    function handleContentChange(value) {
        setPost({ ...post, content: value });
    };

    function handleSubmit(event) {
        event.preventDefault();
        fetch("/posts/editPost", {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(post)
        }).then((response) => {
            if (response.status === 200) {
                window.location.pathname = '/';
            }
        });
    };

    return (
        <PostForm title={post.title}
            content={post.content}
            onTitleChange={(event) => handleTitleChange(event.target.value)}
            onContentChange={(event) => handleContentChange(event.target.value)}
            onSubmit={handleSubmit} />
    );
}

export default EditPostForm;