import React from 'react';
import Posts from './Posts';

function UserProfile({ userProfile }) {
    return <div>
        <div>Name: {userProfile.name}</div>
        <div>Email: {userProfile.email}</div>
        {userProfile.id !== undefined
            && <Posts userLoggedIn={true}
                userProfile={userProfile}
                url={`/posts/getUserPosts/${userProfile.id}`} />}
    </div>;
}

export default UserProfile;