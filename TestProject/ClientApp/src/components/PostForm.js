import React from 'react';

function PostForm({ title, content, onTitleChange, onContentChange, onSubmit }) {
    return (
        <div>
            <form onSubmit={onSubmit}>
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

export default PostForm;