import React, { Component } from 'react';
import { Route } from 'react-router';
import Header from "./components/Header";

import './custom.css'

function Main() {
    return <div>
        <Header />
    </div>;
}

export default class App extends Component {
  static displayName = App.name;

    render() {
        return (
            <div>
                <Route exact path="/" component={Main} />
            </div>
        );
  }
}


