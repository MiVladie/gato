import React from 'react';

import { Helmet } from 'react-helmet';

const Home: React.FC = () => {
	return (
		<>
			<Helmet>
				<title>Home | Gato</title>
			</Helmet>

			<div>Welcome to Gato</div>
		</>
	);
};

export default Home;
