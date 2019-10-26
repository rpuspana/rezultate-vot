import React, { Component } from 'react';

import * as signalR from "@aspnet/signalr";
export class Home extends Component {
  static displayName = Home.name;
  constructor(props) {
      super(props);

      this.state = {
          nick: '',
          message: '',
          messages: [],
          connection: null,
      };
  }
  componentDidMount = () => {
      const connection = new signalR.HubConnectionBuilder()
          .withUrl("/live-results")
          .build();

      this.setState({ connection }, () => {
          this.state.connection
              .start()
              .then(() => console.log('Connection started!'))
              .catch(err => console.log('Error while establishing connection :('));

          this.state.connection.on('results-updated', (user, results) => {
              console.log('got results');
          });
      });
    }

  render () {
    return (
      <div>
        <h1>Hello, world!</h1>
        <p>Welcome to your new single-page application, built with:</p>
        <ul>
          <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
          <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
          <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
        </ul>
        <div>
        <br />
        
      </div>
        <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p>
      </div>
    );
  }
}
