import React from 'react';

import classes from './Layout.module.scss';

import './reset.css';

const Layout: React.FC = ({ children }) => {
	return <div className={classes.Layout}>{children}</div>;
};

export default Layout;
