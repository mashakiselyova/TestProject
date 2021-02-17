import React from 'react';
import UserMenu from "./UserMenu";

function Header(props) {
    return (
        <header>
            <div className="row">
                <nav className="col-12 navbar navbar-dark bg-dark">
                    <div className="container-fluid">
                        <a className="navbar-brand" href="/">Test Project</a>
                        {props.userLoggedIn
                            ? <UserMenu name={props.name} />
                            : <a href="/account/google-login" className="btn btn-light">Sign in</a>}
                    </div>
                </nav>
            </div>
        </header>
    );
}

export default Header;
