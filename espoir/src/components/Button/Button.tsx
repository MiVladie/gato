import React from 'react';

import background from 'assets/icons/button.png';

import classes from './Button.module.scss';

interface Props {
	children: React.ReactNode;
	onClick: () => void;
	className?: string;
	disabled?: boolean;
}

const Button: React.FC<Props> = ({ children, onClick, className, disabled }) => (
	<button className={[classes.Button, className].join(' ')} onClick={onClick} disabled={disabled}>
		<img src={background} alt='button' />
		<p>{children}</p>
	</button>
);

export default Button;
