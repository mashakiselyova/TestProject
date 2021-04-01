import React, { useState, useEffect } from 'react';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';
import Posts from './Posts';

function UserProfile({ currentUser, userId }) {
    const [profile, setProfile] = useState();

    useEffect(() => {
        fetch(`user/get/${userId}`, { mode: 'no-cors' })
            .then((response) => {
                if (response.ok) {
                    response.json().then((data) => {
                        setProfile(data);
                    });
                }
                else {
                    NotificationManager.error("Couldn't get profile");
                }
            })
            .catch(() => {
                NotificationManager.error("Couldn't get profile");
            });   
    }, [userId])

    return (<div>
        {profile &&
            <div className="col-8 offset-2">
                <div className="card p-2">
                    <div>Name: {profile.name}</div>
                    <div>Email: {profile.email}</div>
                    <div>Rating: {profile.rating}</div>
                </div>
                {profile.id
                && <Posts currentUser={currentUser} authorId={userId ? userId : currentUser.id} />}
            </div>}
        <NotificationContainer />
    </div>);
}

export default UserProfile;