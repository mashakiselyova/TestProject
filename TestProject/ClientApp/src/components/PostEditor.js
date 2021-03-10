import React, { useState } from 'react';

function PostEditor() {
    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');

    function onTitleChange(event){
        const value = event.target.value;
        setTitle(value);
    };

    function onContentChange(event){
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
        <div onSubmit={handleSubmit}>
            <form>
                <div className="form-group">
                    <label>Title</label>
                    <input type="text" className="form-control" value={title} onChange={onTitleChange} />
                </div>
                <div className="form-group">
                    <label>Text</label>
                    <textarea rows="5" className="form-control" value={content} onChange={onContentChange} />
                </div>
                <input type="submit" className="btn btn-primary" value="Create" />
            </form>
        </div>
    );
}

export default PostEditor;