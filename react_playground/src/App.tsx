import React from "react";

class DateFact extends React.Component<IProps>  {


    render() {
        return (<div className="date-fact">
            Fact: On this day in {this.state.year}, {this.state.dateFact}.
        </div>)
    }
}

interface IProps {
    onSubmit?: any;
    year?: string;
    dateFact?: string
}

class Form extends React.Component<IProps> {
    state = {
        month: "",
        day: "",
        dateFact: ""
    };

    handleSubmit = async (event: any) => {
        event.preventDefault();

        let url: string = `https://numbersapi.p.rapidapi.com/${this.state.month}/${this.state.day}/date?fragment=false&json=true`;

        fetch(url, {
            "method": "GET",
            "headers": {
                "x-rapidapi-host": "numbersapi.p.rapidapi.com",
                "x-rapidapi-key": "0173102eccmsha12a431a4720adap1c04edjsn7c69820214b9"
            }
        }).then((response: Response) => {
            return response.json()
        }).then((json: any) => {
            console.log(`Json: ${json.text}`)
            this.props.onSubmit(json);

            this.setState({month: "Enter month"});
            this.setState({day: "Enter day"});
        });
    };

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <input
                    type="text"
                    value={this.state.month}
                    onChange={event => this.setState({month: event.target.value})}
                    placeholder="Enter month"
                    required
                />
                <input
                    type="text"
                    value={this.state.day}
                    onChange={event => this.setState({day: event.target.value})}
                    placeholder="Enter day"
                    required
                />
                <button>Get Fact!</button>
            </form>
        );
    }

}

class App extends React.Component {
    constructor(props: any) {
        super(props);
        this.state = {
            dateFact: "",
            year: ""
        };
    }

    setFact = (newFactInfo: any) => {
        this.setState({dateFact: newFactInfo.dateFact, year: newFactInfo.year});
    }

    render() {
        return (
            <div>
                <div className="header">The Date Facts App</div>
                <Form onSubmit={this.setFact}/>
                <br/>
                <DateFact state={this.state}/>
            </div>
        );
    }
}

export default App;
