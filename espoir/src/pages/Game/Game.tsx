import React, { useEffect, useState } from 'react';

import { Helmet } from 'react-helmet';
import { useNavigate } from 'react-router-dom';
import { isMobile } from 'util/screen';

import Unity, { UnityContext } from 'react-unity-webgl';

import back from 'assets/icons/back.png';

import classes from './Game.module.scss';

const unityContext = new UnityContext({
	loaderUrl: 'project/Web.loader.js',
	dataUrl: 'project/Web.data',
	frameworkUrl: 'project/Web.framework.js',
	codeUrl: 'project/Web.wasm'
});

const Game: React.FC = () => {
	const [progression, setProgression] = useState<number>(0);

	const navigate = useNavigate();

	useEffect(() => {
		if (isMobile()) {
			navigate('/');
		}

		unityContext.on('progress', (progression) => {
			setProgression(progression);
		});
	}, []);

	return (
		<>
			<Helmet>
				<title>Game | Gato</title>
			</Helmet>

			<div className={classes.Main}>
				<button className={classes.Back} onClick={() => navigate('/')}>
					<img src={back} alt='back' width='50' height='50' />
				</button>

				{progression !== 1 && <p className={classes.Loading}>{Math.round(progression * 100)}%</p>}

				<Unity
					unityContext={unityContext}
					style={{ width: '56.25vh', height: '100vh', backgroundColor: '#231F20' }}
				/>
			</div>
		</>
	);
};

export default Game;
