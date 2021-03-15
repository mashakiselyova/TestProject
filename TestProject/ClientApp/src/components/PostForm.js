import React, { useState, useEffect } from 'react';

function PostForm({ postId, create }) {
    const [post, setPost] = useState({ id: '', title: '', content: '' });

    useEffect(() => {
        if (!create) {
            fetch(`/posts/getPost/${postId}`, { method: 'get', mode: 'no-cors' })
                .then((response) => {
                    response.json().then((data) => {
                        setPost(data);
                    });
                });
        }
    }, [])

    function handleTitleChange(value) {
        setPost({ ...post, title: value });
    };

    function handleContentChange(value) {
        setPost({ ...post, content: value });
    };

    function handleSubmit(event) {
        const url = create ? "/posts/createPost" : "/posts/editPost";
        const status = create ? 201 : 200;
        event.preventDefault();
        fetch(url, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(post)
        }).then((response) => {
            if (response.status === status) {
                window.location.pathname = '/';
            }
        });
    };

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Title</label>
                    <input type="text" className="form-control" value={post.title}
                        onChange={(event) => handleTitleChange(event.target.value)} />
                </div>
                <div className="form-group">
                    <label>Text</label>
                    <textarea rows="5" className="form-control" value={post.content}
                        onChange={(event) => handleContentChange(event.target.value)} />
                </div>
                <input type="submit" className="btn btn-primary" value="Create" />
            </form>
        </div>
    );
}

export default PostForm;