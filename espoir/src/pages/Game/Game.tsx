import React, { useEffect } from 'react';

import { Helmet } from 'react-helmet';
import Unity, { UnityContext } from 'react-unity-webgl';

import classes from './Game.module.scss';

const unityContext = new UnityContext({
	loaderUrl: 'project/Web.loader.js',
	dataUrl: 'project/Web.data',
	frameworkUrl: 'project/Web.framework.js',
	codeUrl: 'project/Web.wasm'
});

const Game: React.FC = () => {
	return (
		<>
			<Helmet>
				<title>Game | Gato</title>
			</Helmet>

			<div className={classes.Main}>
				<Unity
					unityContext={unityContext}
					matchWebGLToCanvasSize={false}
					style={{ width: '56.25vh', height: '100vh' }}
				/>
			</div>
		</>
	);
};

export default Game;
