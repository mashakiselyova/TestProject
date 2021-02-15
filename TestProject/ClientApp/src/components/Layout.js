import React, { useEffect, useState } from 'react';
import { Route } from 'react-router';
import Main from "./Main"
import UserProfile from "./UserProfile"
import Header from "./Header";

function Layout() {
    const [userProfile, setUserProfile] = useState({
        firstName: undefined, lastName: undefined, email: undefined, raiting: undefined
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
        <Header userLoggedIn={userLoggedIn} name={userProfile.firstName + ' ' + userProfile.lastName} />
        <div>
            <Route exact path="/" render={() => <Main />} />
            <Route path="/account/profile" render={() => <UserProfile userProfile={userProfile} />} />
        </div>        
    </div>;
}

export default Layout;