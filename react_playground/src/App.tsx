import React from 'react';
import Clock from "./Clock-ico.svg";
import './App.css';

class App extends React.Component {
	render() {
		return (
			<div className="App">
				<header className="App-header">
					<img src={Clock} className="App-logo" alt="logo"/>
					<Welcome name="Anna"/>
					<DateFact month="12" day="1"/>
				</header>
			</div>
		);
	}
}

function Welcome(props: any) {
	return <h1>Hello, {props.name}!</h1>;
}

function DateFact(props: any) {
	let url: string = `https://numbersapi.p.rapidapi.com/${props.month}/${props.day}/date?fragment=false&json=false`;
	let fact: any = "Initial value";

	fetch(url, {
		"method": "GET",
		"headers": {
			"x-rapidapi-host": "numbersapi.p.rapidapi.com",
			"x-rapidapi-key": "0173102eccmsha12a431a4720adap1c04edjsn7c69820214b9"
		}
	})
		.then(response => {
			fact = response;
		})
		.catch(err => {
			console.log(err);
			fact = err;
		});

	return <p>Fun Date Fact: {fact}</p>;
}

export default App;
