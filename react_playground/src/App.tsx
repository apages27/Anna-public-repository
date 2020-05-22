import React, {Component} from "react";
import './App.css';

interface IProps {
	dateFacts?: string[];
	onSubmit: Function;
}

interface IState {
	dateFacts: string[];
	month?: string;
	day?: string;
}

const CardList = (props: any) => (
	<div>
		{props.dateFacts.map((dateFact: any) => <Card key={props.dateFacts.indexOf(dateFact)} {...dateFact}/>)}
	</div>
);

class Card extends Component {
	render() {
		const dateFact = this.props;
		return (
			<div className="github-profile">
				<div className="info">
					<div className="name">{dateFact}</div>
				</div>
			</div>
		);
	}
}

class Form extends Component<IProps> {
	state = {day: "", month: ""};

	handleSubmit = async (event: any) => {
		event.preventDefault();
		let url: string = `https://numbersapi.p.rapidapi.com/${this.state.month}/${this.state.day}/date?fragment=false&json=false`;

		const response = await fetch(url, {
			"method": "GET",
			"headers": {
				"x-rapidapi-host": "numbersapi.p.rapidapi.com",
				"x-rapidapi-key": "0173102eccmsha12a431a4720adap1c04edjsn7c69820214b9"
			}
		});
		this.props.onSubmit(response.text());
		this.setState({day: "", month: ""});
	};

	render() {
		return (
			<form onSubmit={this.handleSubmit}>
				<input
					type="text"
					placeholder="Month"
					value={this.state.month}
					onChange={event => this.setState({month: event.target.value})}
					required/>
				<input
					type="text"
					placeholder="Day"
					value={this.state.day}
					onChange={event => this.setState({day: event.target.value})}
					required/>
				<button>Get Fact</button>
			</form>
		);
	}
}

class App extends Component<IProps, IState> {
	constructor(props: any) {
		super(props);
		this.state = {
			dateFacts: [],
		};
	}

	addNewDateFact = (newDateFact: any) => {
		this.setState(prevState => ({dateFacts: [...prevState.dateFacts, newDateFact]}));
	};

	render() {
		return (
			<div>
				<div className="header">Choose a Date:</div>
				<Form onSubmit={this.addNewDateFact}/>
				<CardList dateFacts={this.state.dateFacts}/>
			</div>
		);
	}
}

export default App;
