import React from 'react';
import { Link } from 'react-router-dom';

function Post({ post, userLoggedIn, userProfile }) {

    function handleDelete() {
        fetch(`/posts/delete/${post.id}`, {
            method: 'post',
        }).then((response) => {
            if (response.status === 200) {
                window.location.pathname = '/';
            }
        }).catch(() => {
            window.alert("Couldn't delete this post");
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
                    <Link to={`/posts/edit/${post.id}`} className="btn btn-primary btn-sm m-1">Edit</Link>
                    <button onClick={() => { if (window.confirm("Are you sure you want to delete this post?")) handleDelete() }}
                        className="btn btn-danger btn-sm m-1">Delete</button>
                    </div>}
            </div>
        </div>
    );
}

export default Post;