import React, { useState, useEffect } from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import Joi from "joi-browser";

function PostForm({ postId, editing = false }) {
    const [post, setPost] = useState({ id: 0, title: '', content: '' });
    const [errors, setErrors] = useState({});

    const schema = Joi.object({
        title: Joi.string().required(),
        content: Joi.string().required()
    });

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
    }, [editing, postId])

    function handleTitleChange(value) {
        setPost({ ...post, title: value });
        setErrors({});
    };

    function handleContentChange(value) {
        setPost({ ...post, content: value });
        setErrors({});
    };

    function handleSubmit() {

        const errors = validatePost();
        if (errors) {
            setErrors(errors);
            return;
        }

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
            else if (response.status === 400) {
                NotificationManager.error("Post validation failed");
            }
            else {
                NotificationManager.error(`Couldn't ${editing ? "edit" : "create"} post`);
            }
        }).catch(() => {
            NotificationManager.error(`Couldn't ${editing ? "edit" : "create"} post`);
        });
    };

    function validatePost() {
        const result = Joi.validate(post, schema);
        if (!result.error) {
            return null;
        }
        const errors = {};
        for (let item of result.error.details)
            errors[item.path[0]] = item.message;
        return errors;
    }

    return (
        <div>
            <form onSubmit={(event) => event.preventDefault()}>
                <div className="form-group">
                    <label>Title</label>
                    <input type="text" className="form-control" value={post.title}
                        onChange={(event) => handleTitleChange(event.target.value)} />
                    {errors.title && <div className="alert alert-danger">{errors.title}</div>}
                </div>
                <div className="form-group">
                    <label>Text</label>
                    <textarea rows="5" className="form-control" value={post.content}
                        onChange={(event) => handleContentChange(event.target.value)} />
                    {errors.content && <div className="alert alert-danger">{errors.content}</div>}
                </div>
                <button className="btn btn-primary" onClick={handleSubmit} >{editing ? "Edit" : "Create"}</button>
            </form>
            <NotificationContainer />
        </div>
    );
}

export default PostForm;