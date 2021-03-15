import React, { useState, useEffect } from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';

function PostForm({ userId, postId, editing = false }) {
    const [post, setPost] = useState({ id: 0, title: '', content: '', userId });

    useEffect(() => {
        if (editing) {
            fetch(`/posts/get/${postId}`, { method: 'get', mode: 'no-cors' })
                .then((response) => {
                    response.json().then((data) => {
                        setPost(data);
                    });
                });
        }
    }, [editing])

    function handleTitleChange(value) {
        setPost({ ...post, title: value });
    };

    function handleContentChange(value) {
        setPost({ ...post, content: value });
    };

    function handleSubmit() {
        fetch("/posts/edit", {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(post)
        }).then((response) => {
            if (response.status === 200) {
                window.location.pathname = '/';
            }
            else throw new Error("Couldn't create post")
        }).catch((error) => {
            NotificationManager.error(error);
        });
    };

    return (
        <div>
            <form>
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
                <button className="btn btn-primary" onClick={handleSubmit} >Create</button>
            </form>
            <NotificationContainer />
        </div>
    );
}

export default PostForm;