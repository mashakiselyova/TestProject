import React, { useState, useEffect } from 'react';

function EditPostForm({ match }) {
    const [post, setPost] = useState({ id: '', title: '', content: '' });

    useEffect(() => {
        fetch(`/posts/getPost/${match.params.id}`, { method: 'get', mode: 'no-cors' })
            .then((response) => {
                response.json().then((data) => {
                    setPost(data);
                });
            });
    }, [])

    function handleTitleChange(event) {
        const value = event.target.value;
        let editedPost = { ...post };
        editedPost.title = value;
        setPost(editedPost);
    };

    function handleContentChange(event) {
        const value = event.target.value;
        let editedPost = { ...post };
        editedPost.content = value;
        setPost(editedPost);
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
        <div>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Title</label>
                    <input type="text" className="form-control" value={post.title} onChange={handleTitleChange} />
                </div>
                <div className="form-group">
                    <label>Text</label>
                    <textarea rows="5" className="form-control" value={post.content} onChange={handleContentChange} />
                </div>
                <input type="submit" className="btn btn-primary" value="Save" />
            </form>
        </div>
        );
}

export default EditPostForm;