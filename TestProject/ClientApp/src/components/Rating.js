import React from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faThumbsUp, faThumbsDown } from '@fortawesome/free-solid-svg-icons'
import { unrated, plus, minus } from "../ratingValues"

function Rating({ postId, userProfile, authorId, selectedRating, totalRating }) {

    function handleSetRating(value) {
        fetch("/rating/set", {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ postId, value, authorId })
        }).then((response) => {
            if (response.status !== 200) {
                NotificationManager.error("Couldn't set rating");
            }
        }).catch(() => {
            NotificationManager.error("Couldn't set rating");
        });
    }

    const thumbsUpStyles = { cursor: "pointer", marginBottom: 5, color: selectedRating === plus ? "red" : "black" };
    const thumbsDownStyles = { cursor: "pointer", color: selectedRating === minus ? "red" : "black" };
    const userIsNotAuthor = userProfile.loggedIn && userProfile.id !== authorId;

    return (
        <div>
            <div>{totalRating}</div>
            {userIsNotAuthor &&
                <div>
                    <FontAwesomeIcon
                        style={thumbsUpStyles}
                        onClick={() => handleSetRating(plus)}
                        icon={faThumbsUp}
                        size="lg" />
                    <FontAwesomeIcon
                        style={thumbsDownStyles}
                        onClick={() => handleSetRating(minus)}
                        icon={faThumbsDown}
                        size="lg" />
                </div>
            }
            <NotificationContainer />
        </div>
    );
}

export default Rating;