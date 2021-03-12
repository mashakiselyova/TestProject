import React from 'react';

function Post({ post, userLoggedIn, userProfile }) {

    function handleDelete() {
        fetch(`/posts/delete/${post.id}`, {
            method: 'post',
        }).then((response) => {
            if (response.status === 200) {
                window.location.pathname = '/';
            }
        });
    }

    return (
        <div className="card">
            <h5 className="card-header">{post.author.firstName + ' ' + post.author.lastName}</h5>
            <div className="card-body">
                <h5 className="card-title">{post.title}</h5>
                <h6 className="card-subtitle mb-2 text-muted">{new Date(post.createDate).toLocaleDateString()}</h6>
                <p className="card-text">{post.content}</p>
                {userLoggedIn && userProfile.id === post.author.id
                    && <div>
                    <a href={`/posts/edit/${post.id}`} className="btn btn-primary btn-sm m-1">Edit</a>
                    <button onClick={handleDelete} className="btn btn-danger btn-sm m-1">Delete</button>
                    </div>}
            </div>
        </div>
    );
}

export default Post;