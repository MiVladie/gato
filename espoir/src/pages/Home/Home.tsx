import React from 'react';

import { Helmet } from 'react-helmet';

import Video from 'components/Video/Video';
import Button from 'components/Button/Button';

import logo from 'assets/icons/logo.png';

import classes from './Home.module.scss';

const Home: React.FC = () => {
	return (
		<>
			<Helmet>
				<title>Home | Gato</title>
			</Helmet>

			<div className={classes.Main}>
				<img className={classes.Logo} src={logo} />

				<div className={classes.Container}>
					<Video
						className={classes.Trailer}
						title='Gato Trailer'
						url='https://www.youtube.com/embed/OGZE16bhKgQ'
					/>

					<div className={classes.Actions}>
						<Button onClick={() => console.log('Play!')} className={classes.Play}>
							Play
						</Button>

						<Button onClick={() => console.log('Download (.exe)')}>Download (.exe)</Button>
						<Button onClick={() => console.log('Download (.apk)')}>Download (.apk)</Button>
					</div>
				</div>
			</div>
		</>
	);
};

export default Home;
