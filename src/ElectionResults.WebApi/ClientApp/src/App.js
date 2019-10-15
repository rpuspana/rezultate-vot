import React, { Component } from "react";
import "./scss/reset.css";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { ChartContainer } from "./components/CandidatesChart/index";
import { FetchData } from "./components/FetchData";
import { Counter } from "./components/Counter";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path="/" component={ChartContainer} />
        <Route path="/counter" component={Counter} />
        <Route path="/fetch-data" component={FetchData} />
      </Layout>
    );
  }
}
