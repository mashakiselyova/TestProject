import React, { useEffect, useState } from 'react';
import { Route } from 'react-router';
import Main from "./Main"
import UserProfile from "./UserProfile"
import Header from "./Header";
import PostForm from "./PostForm";
import PostPage from "./PostPage";

function Layout() {
    const [currentUser, setCurrentUser] = useState({
        name: undefined, email: undefined, id: undefined, loggedIn: undefined
    });

    useEffect(() => {
        fetch("/user/getCurrentUser", { mode: 'no-cors' })
            .then((response) => {
                if (response.ok) {
                    response.json().then((data) => {
                        setCurrentUser({ ...data, loggedIn: true });
                    });
                }
                else {
                    setCurrentUser({ loggedIn: false });
                }
            })
            .catch(() => {
                setCurrentUser({ loggedIn: false });
            });
    }, [])

    return <div>
        <Header userProfile={currentUser} />
        <div>
            <Route exact path="/" render={() => <Main currentUser={currentUser} />} />
            <Route path="/account/profile" render={() => <UserProfile currentUser={currentUser} />} />
            <Route exact path="/posts/create" render={() => <PostForm userId={currentUser.id} />} />
            <Route path="/posts/edit/:id" render={(props) => <PostForm postId={props.match.params.id} editing={true} />} />
            <Route path="/posts/post/:id" render={(props) => <PostPage
                postId={props.match.params.id}
                currentUser={currentUser} />} />
            <Route path="/user/profile/:id" render={(props) => <UserProfile
                currentUser={currentUser}
                userId={props.match.params.id} />} />
        </div>
    </div>;
}

export default Layout;