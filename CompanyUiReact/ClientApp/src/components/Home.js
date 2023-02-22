import React, { Component } from 'react';
import CompanyData from './CompanyData';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <CompanyData />
    );
  }
}
