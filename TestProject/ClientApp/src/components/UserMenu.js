import React from 'react';
import { Link } from 'react-router-dom';

function UserMenu({ name }) {
    return <div className="dropdown">
        <button
            className="btn btn-secondary dropdown-toggle"
            type="button"
            id="userMenu"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false">
            {name}
        </button>
        <ul className="dropdown-menu" aria-labelledby="userMenu">
            <li><Link className="dropdown-item" to="/account/profile">Profile</Link></li>
            <li><a className="dropdown-item" href="/account/logout">Sign out</a></li>
        </ul>
    </div>
}

export default UserMenu;