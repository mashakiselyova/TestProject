import React, { useState } from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';

function CommentInput({ postId, updatePost }) {
    const [comment, setComment] = useState("");

    function handleCommentChange(value){
        setComment(value);
    }

    function handleSubmit() {
        fetch("/comments/create", {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({text: comment, postId})
        }).then((response) => {
            if (response.ok) {
                updatePost();
                setComment("");
            }
            else {
                NotificationManager.error("Couldn't create comment");
            }
        }).catch(() => {
            NotificationManager.error("Couldn't create comment");
        });
    }

    return (
        <div className="card p-2">
            <form className="form-group" onSubmit={(event) => event.preventDefault()}>
                <input className="form-control" placeholder="Comment" value={comment}
                    onChange={(event) => handleCommentChange(event.target.value)} />
                <button className="btn btn-primary btn-sm mt-2 " onClick={handleSubmit}>Send</button>
            </form>
            <NotificationContainer />
        </div>
    );
}

export default CommentInput;