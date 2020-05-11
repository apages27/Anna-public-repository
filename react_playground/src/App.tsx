import React, {useEffect, useState} from 'react';
import Clock from "./Clock-ico.svg";
import './App.css';

class App extends React.Component {
	render() {
		return (
			<div className="App">
				<header className="App-header">
					<img src={Clock} className="App-logo" alt="logo"/>
					<Welcome name="Anna"/>
					<DateFact/>
				</header>
			</div>
		);
	}
}

function Welcome(props: any) {
	return <h1>Hello, {props.name}!</h1>;
}

function DateFact() {
	useEffect(() => {
		const fetchDateFact = async () => {
			let url: string = `https://numbersapi.p.rapidapi.com/12/1/date?fragment=false&json=false`;

			try {
				setData({fact: "Fetching a new date fact...", isFetching: true});
				const response = await fetch(url, {
					"method": "GET",
					"headers": {
						"x-rapidapi-host": "numbersapi.p.rapidapi.com",
						"x-rapidapi-key": "0173102eccmsha12a431a4720adap1c04edjsn7c69820214b9"
					}
				});
				console.log(`Response: `, response);
				setData({fact: response.text, isFetching: false});
			} catch (e) {
				console.log(e);
				setData({fact: e.text, isFetching: false});
			}
		};

		fetchDateFact();
	}, []);

	const [data, setData] = useState({fact: {}, isFetching: false});

	console.log(`Fact: `, data.fact);
	return <p>Fun Date Fact: {data.fact}</p>;


}

export default App;
