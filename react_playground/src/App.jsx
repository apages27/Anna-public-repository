"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var react_1 = require("react");
var Clock_ico_svg_1 = require("./Clock-ico.svg");
require("./App.css");
var App = /** @class */ (function (_super) {
    __extends(App, _super);
    function App() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    App.prototype.render = function () {
        return (<div className="App">
				<header className="App-header">
					<img src={Clock_ico_svg_1.default} className="App-logo" alt="logo"/>
					<Welcome name="Anna"/>
					<DateFact month="12" day="1"/>
				</header>
			</div>);
    };
    return App;
}(react_1.default.Component));
function Welcome(props) {
    return <h1>Hello, {props.name}!</h1>;
}
function DateFact(props) {
    var url = "https://numbersapi.p.rapidapi.com/" + props.month + "/" + props.day + "/date?fragment=false&json=false";
    var fact = "Initial value";
    fetch(url, {
        "method": "GET",
        "headers": {
            "x-rapidapi-host": "numbersapi.p.rapidapi.com",
            "x-rapidapi-key": "0173102eccmsha12a431a4720adap1c04edjsn7c69820214b9"
        }
    })
        .then(function (response) {
        fact = response;
    })
        .catch(function (err) {
        console.log(err);
        fact = err;
    });
    return <p>Fun Date Fact: {fact}</p>;
}
exports.default = App;
