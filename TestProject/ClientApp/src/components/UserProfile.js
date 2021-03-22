import React from 'react';
import Posts from './Posts';

function UserProfile({ userProfile }) {
    return <div>
        <div>Name: {userProfile.name}</div>
        <div>Email: {userProfile.email}</div>
        <div>Rating: {userProfile.rating}</div>
        {userProfile.id
            && <Posts userLoggedIn={true}
                userProfile={userProfile}
                filterByCurrentUser={true} />}
    </div>;
}

export default UserProfile;