import React from 'react';

function Main(props) {
    return <div>
        {props.userLoggedIn
            ? <a href="/posts/create" className="btn btn-primary mt-1">New Post</a>
            : <p>You need to sign in to create new posts</p>}        
    </div>;
}

export default Main;