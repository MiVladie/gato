import React from 'react';

import classes from './Layout.module.scss';

import './reset.css';

const Layout: React.FC = ({ children }) => {
	return (
		<div className={classes.Layout}>
			<div className={classes.Background} />

			<div className={classes.Content}>{children}</div>
		</div>
	);
};

export default Layout;
