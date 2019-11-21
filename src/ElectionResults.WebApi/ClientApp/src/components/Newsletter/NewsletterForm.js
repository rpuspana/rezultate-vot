import React from 'react';
import './NewsletterForm.css';

class NewsletterForm extends React.Component {
    constructor(props) {
      super(props);
      this.state = {value: ''};
  
      this.handleChange = this.handleChange.bind(this);
      this.handleSubmit = this.handleSubmit.bind(this);
    }
  
    handleChange(event) {
      this.setState({value: event.target.value});
    }
  
    handleSubmit(event) {
      alert('A name was submitted: ' + this.state.value);
      this.handle
      event.preventDefault();
    }
  
    render() {
      return (
        <form className="footerNewsletterForm" onSubmit={this.handleSubmit}>
          <input type="text" id="footer-newsletter" placeholder="Introdu adresa de e-mail si apasa ENTER" value={this.state.value} onChange={this.handleChange} />
        </form>
      );
    }
  }

  export default NewsletterForm;