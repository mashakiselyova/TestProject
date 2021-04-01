import React from 'react';
import { Link } from 'react-router-dom';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';
import ConfirmDelete from "./ConfirmDelete";
import Rating from "./Rating";

function Post({ post, currentUser, updateRating }) {

    function handleDelete() {
        fetch(`/posts/delete/${post.id}`, {
            method: 'post',
        }).then((response) => {
            if (response.status === 200) {
                window.location.pathname = '/';
            }
            else {
                NotificationManager.error("Couldn't delete this post");
            }
        }).catch(() => {
            NotificationManager.error("Couldn't delete this post");
        });
    }

    return (
        <div className="card">
            <Link to={`/user/profile/${post.author.id}`}>
                <h5 className="card-header">{post.author.firstName + ' ' + post.author.lastName}</h5>
            </Link>
            <div className="card-body row">
                <div className="col-1">
                    <Rating
                        postId={post.id}
                        currentUser={currentUser}
                        authorId={post.author.id}
                        selectedRating={post.selectedRating}
                        totalRating={post.totalRating}
                        updateRating={updateRating}
                    />
                </div>
                <div className="col">
                    <Link to={`/posts/post/${post.id}`}><h5 className="card-title">{post.title}</h5></Link>
                    <h6 className="card-subtitle mb-2 text-muted">{new Date(post.createDate).toLocaleDateString()}</h6>
                    <p className="card-text">{post.content}</p>
                    {currentUser.loggedIn && currentUser.id === post.author.id
                        && <div>
                            <Link to={`/posts/edit/${post.id}`} className="btn btn-primary btn-sm m-1">Edit</Link>
                            <button
                                type="button"
                                data-toggle="modal"
                                data-target={`#confirmDelete${post.id}`}
                                className="btn btn-danger btn-sm m-1"
                            >
                                Delete
                        </button>
                        </div>}
                </div>
            </div>
            <NotificationContainer />
            <ConfirmDelete postId={post.id} onDelete={handleDelete} />
        </div>
    );
}

export default Post;