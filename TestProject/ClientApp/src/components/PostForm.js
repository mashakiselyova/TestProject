import React, { useState, useEffect } from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';

function PostForm({ postId, editing = false }) {
    const [post, setPost] = useState({ id: 0, title: '', content: '' });

    useEffect(() => {
        if (editing) {
            fetch(`/posts/get/${postId}`, { method: 'get', mode: 'no-cors' })
                .then((response) => {
                    response.json().then((data) => {
                        setPost(data);
                    });
                }).catch(() => {
                    NotificationManager.error("Couldn't load post");
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
            else {
                NotificationManager.error(`Couldn't ${editing ? "edit" : "create"} post`);
            }
        }).catch(() => {
            NotificationManager.error(`Couldn't ${editing ? "edit" : "create"} post`);
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
                <button className="btn btn-primary" onClick={handleSubmit} >{editing ? "Edit" : "Create"}</button>
            </form>
            <NotificationContainer />
        </div>
    );
}

export default PostForm;