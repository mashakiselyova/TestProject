import React, { useEffect, useState } from 'react';
import { Route } from 'react-router';
import Main from "./Main"
import UserProfile from "./UserProfile"
import Header from "./Header";
import PostForm from "./PostForm";
import PostPage from "./PostPage";

function Layout() {
    const [userProfile, setUserProfile] = useState({
        name: undefined, email: undefined, id: undefined, loggedIn: undefined
    });

    useEffect(() => {
        fetch("/user/getUserProfile", { mode: 'no-cors' })
            .then((response) => {
                if (response.ok) {
                    response.json().then((data) => {
                        setUserProfile({ ...data, loggedIn: true });
                    });
                }
                else {
                    setUserProfile({ loggedIn: false });
                }
            })
            .catch(() => {
                setUserProfile({ loggedIn: false });
            });
    }, [])

    return <div>
        <Header userProfile={userProfile} />
        <div>
            <Route exact path="/" render={() => <Main userProfile={userProfile} />} />
            <Route path="/account/profile" render={() => <UserProfile userProfile={userProfile} />} />
            <Route exact path="/posts/create" render={() => <PostForm userId={userProfile.id} />} />
            <Route path="/posts/edit/:id" render={(props) => <PostForm postId={props.match.params.id} editing={true} />} />
            <Route path="/posts/post/:id" render={(props) => <PostPage
                postId={props.match.params.id}
                userProfile={userProfile} />} />
        </div>
    </div>;
}

export default Layout;