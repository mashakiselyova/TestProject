import React from 'react';

function Post({ author, title, createDate, content }) {
    return (
        <div className="card">
            <h5 className="card-header">{author.firstName + ' ' + author.lastName}</h5>
            <div className="card-body">
                <h5 className="card-title">{title}</h5>
                <h6 className="card-subtitle mb-2 text-muted">{new Date(createDate).toLocaleDateString()}</h6>
                <p className="card-text">{content}</p>
            </div>
        </div>
    );
}

export default Post;