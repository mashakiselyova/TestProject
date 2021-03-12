import React from 'react';
import Posts from './Posts';

function Main({ userLoggedIn, userProfile }) {
    return (
        <div className="row">
            <div className="col-2">
                {userLoggedIn
                    ? <a href="/posts/create" className="btn btn-primary mt-1">New Post</a>
                    : <p>You need to sign in to create new posts</p>}
            </div>
            <div className="col-8">
                <Posts userLoggedIn={userLoggedIn} userProfile={userProfile} />
            </div>
        </div>
    );
}

export default Main;