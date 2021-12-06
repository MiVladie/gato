import React from 'react';

import { useLocation } from 'react-router';

import classes from './Layout.module.scss';

import './reset.css';

const Layout: React.FC = ({ children }) => {
	const location = useLocation();

	const getClass = (): string => {
		switch (location.pathname) {
			case '/play':
				return classes.Play;

			default:
				return '';
		}
	};

	return (
		<div className={classes.Layout}>
			<div className={[classes.Background, getClass()].join(' ')} />

			<div className={classes.Content}>{children}</div>
		</div>
	);
};

export default Layout;
