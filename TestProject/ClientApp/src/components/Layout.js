import React, { useEffect, useState } from 'react';
import { Route } from 'react-router';
import Main from "./Main"
import UserProfile from "./UserProfile"
import Header from "./Header";
import CreatePostForm from "./CreatePostForm";
import EditPostForm from "./EditPostForm";

function Layout() {
    const [userProfile, setUserProfile] = useState({
        name: undefined, email: undefined, id: undefined
    });
    const [userLoggedIn, setUserLoggedIn] = useState(false)

    useEffect(() => {
        fetch("/user/getUserProfile", { mode: 'no-cors' })
            .then((response) => {
                if (response.ok) {
                    response.json().then((data) => {
                        setUserProfile(data);
                    });
                    setUserLoggedIn(true);
                }
                else {
                    setUserLoggedIn(false);
                }
            })
            .catch(() => {
                setUserLoggedIn(false);
            });
    }, [])

    return <div>
        <Header userLoggedIn={userLoggedIn} name={userProfile.name} />
        <div>
            <Route exact path="/" render={() => <Main userLoggedIn={userLoggedIn} userProfile={userProfile} />} />
            <Route path="/account/profile" render={() => <UserProfile userProfile={userProfile} />} />
            <Route exact path="/posts/create" render={() => <CreatePostForm />} />
            <Route path="/posts/edit/:id" render={(props) => <EditPostForm postId={props.match.params.id} />} />
        </div>        
    </div>;
}

export default Layout;