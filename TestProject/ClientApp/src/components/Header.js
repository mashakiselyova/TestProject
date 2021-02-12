import React from 'react';

function Header() {
    return (
        <header>
            <div className="row">
                <nav className="col-12 navbar navbar-dark bg-dark">
                    <div className="container-fluid">
                        <a className="navbar-brand">Test Project</a>
                        <button onClick={OnClick} className="btn btn-light">Sign in</button>
                    </div>
                </nav>
            </div>
        </header>
    );
}

const OnClick = () => {
    window.location.pathname = '/account/google-login';
}

export default Header;
