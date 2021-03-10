import React from 'react';

function Post(props) {
    return (
        <div className="card">
            <h5 className="card-header">{props.post.user.firstName + ' ' + props.post.user.lastName}</h5>
            <div className="card-body">
                <h5 className="card-title">{props.post.title}</h5>
                <h6 className="card-subtitle mb-2 text-muted">{props.post.createDate}</h6>
                <p className="card-text">{props.post.content}</p>
            </div>
        </div>
    );
}

export default Post;