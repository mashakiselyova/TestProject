import React, { useEffect, useState } from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faThumbsUp, faThumbsDown } from '@fortawesome/free-solid-svg-icons'

function Rating({ postId, userProfile, authorId }) {
    const [rated, setRated] = useState(false);
    const [rating, setRating] = useState(0);

    useEffect(() => {
        if (userProfile.id !== authorId) {
            checkIfRatedByCurrentUser();
        }
        getRating();
    }, [userProfile])

    function handleSetRating(value) {
        fetch("/rating/set", {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ postId, value })
        }).then((response) => {
            if (response.status === 200) {
                setRated(true);
            }
            else {
                NotificationManager.error("Couldn't set rating");
            }
        }).catch(() => {
            NotificationManager.error("Couldn't set rating");
        });
    } 

    function checkIfRatedByCurrentUser() {
        if (userProfile.loggedIn) {
            fetch(`rating/check/${postId}`)
                .then((response) => {
                    response.json().then((data) => {
                        setRated(data);
                    });
                }).catch(() => {
                    NotificationManager.error("Couldn't check rating");
                });
        }
    };

    function getRating() {
        fetch(`rating/get/${postId}`)
            .then((response) => {
                response.json().then((data) => {
                    setRating(data);
                });
            }).catch(() => {
                NotificationManager.error("Couldn't get rating");
            });
    }

    return (
        <div>
            <div>{rating}</div>
            {userProfile.loggedIn && userProfile.id !== authorId && !rated
                && <div>
                    <FontAwesomeIcon
                        style={{ cursor: "pointer", marginBottom: 5 }}
                        onClick={() => handleSetRating(0)}
                        icon={faThumbsUp}
                        size="lg" />
                    <FontAwesomeIcon s
                        tyle={{ cursor: "pointer" }}
                        onClick={() => handleSetRating(1)}
                        icon={faThumbsDown}
                        size="lg" />
                </div>
            }
            <NotificationContainer />
        </div>
    );
}

export default Rating;