import React from 'react';
import './Footer.css';
import NewsletterForm from "../Newsletter/NewsletterForm";

export function Footer() {
    return (
        <div className="row">
            <div className="column" id="left-column">
                <h3>Linkuri utile</h3>
                <ul className="useful-links">
                    <li><a href="/">Termeni &#351;i condi&#355;ii</a></li>
                    <li><a href="/">Politica de confiden&#355;ialitate</a></li>
                    <li><a href="/">Codul de conduit&#259;</a></li>
                    <li><a href="/">Code for Romania</a></li>
                    <li><a href="/">Contact</a></li>
                </ul>
            </div>
            <div className="column" id="right-column">
                <h3>Aboneaza-te la newsletter</h3>
                <NewsletterForm />
                <div className="code4ro-img-and-text">
                    <img src={ require('../../images/code4RomaniaGreyLogo.png') } className="inner" />
                    <p className="inner">Code for<br />Romania</p>
                    <p className="code4ro-copyright-ngo-description">&#9400; 2019 Code for Romania</p>
                    <p className="code4ro-copyright-ngo-description">Organizatie neguvernamental&#259; independent&#259;, neafiliat&#259; politic &#351;i apolitic&#259;</p>
                </div>
            </div>
        </div>
    )
}