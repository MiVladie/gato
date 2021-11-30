import React from 'react';

import { Routes, Route } from 'react-router-dom';
import { Helmet } from 'react-helmet';

import Layout from 'hoc/Layout/Layout';

import Home from 'pages/Home/Home';

const App: React.FC = () => {
	return (
		<Layout>
			<Helmet>
				<title>Gato</title>
			</Helmet>

			<Routes>
				<Route path='/' element={<Home />} />
			</Routes>
		</Layout>
	);
};

export default App;
