import React from 'react';
import Clock from './Clock-ico.svg';
import './App.css';

class App extends React.Component {
    render() {
        return (
            <div className="App">
                <header className="App-header">
                    <img src={Clock} className="App-logo" alt="logo"/>
                    <Welcome name="Anna" />
                </header>
            </div>
        );
    }
}

function Welcome(props) {
    return <h1>Hello, {props.name}!</h1>;
}

export default App;
