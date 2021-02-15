import React from 'react';

function UserProfile(props) {
    return <div>
        <div>First Name: { props.userProfile.firstName }</div>
        <div>Last Name: {props.userProfile.lastName }</div>
        <div>Email: {props.userProfile.email }</div>
        <div>Raiting: {props.userProfile.raiting }</div>
    </div>;
}

export default UserProfile;