import React from 'react';
import { Link } from 'react-router-dom';
import Posts from './Posts';

function Main({ userProfile }) {
    return (
        <div className="row">
            <div className="col-2">
                {userProfile.loggedIn
                    ? <Link to="/posts/create" className="btn btn-primary mt-1">New Post</Link>
                    : <p>You need to sign in to create new posts</p>}
            </div>
            <div className="col-8">
                <Posts userProfile={userProfile} />
            </div>
        </div>
    );
}

export default Main;