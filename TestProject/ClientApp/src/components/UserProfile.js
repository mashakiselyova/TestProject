import React from 'react';

function UserProfile({userProfile}) {
    return <div>
        <div>Name: {userProfile.name}</div>
        <div>Email: {userProfile.email}</div>
    </div>;
}

export default UserProfile;