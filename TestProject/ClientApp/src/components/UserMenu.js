import React from 'react';

function UserMenu(props) {
    return <div className="dropdown">
        <button className="btn btn-secondary dropdown-toggle" type="button" id="userMenu" data-toggle="dropdown" aria-haspopup="true"  aria-expanded="false">
            {props.name}
        </button>
        <ul className="dropdown-menu" aria-labelledby="userMenu">
            <li><a className="dropdown-item" href="/account/profile">Profile</a></li>
            <li><a className="dropdown-item" href="/account/logout">Sign out</a></li>
        </ul>
    </div>
}

export default UserMenu;