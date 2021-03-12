import React, { useState } from 'react';
import PostForm from './PostForm';

function CreatePostForm() {
    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');

    function handleTitleChange(event){
        const value = event.target.value;
        setTitle(value);
    };

    function handleContentChange(event){
        const value = event.target.value;
        setContent(value);
    };

    function handleSubmit(event) {
        event.preventDefault();
        fetch("/posts/createPost", {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({title, content})
        }).then((response) => {
            if (response.status === 201) {
                window.location.pathname = '/';
            }
        });
    };

    return (
        <PostForm title={title}
            content={content}
            onTitleChange={handleTitleChange}
            onContentChange={handleContentChange}
            onSubmit={handleSubmit} />
    );
}

export default CreatePostForm;