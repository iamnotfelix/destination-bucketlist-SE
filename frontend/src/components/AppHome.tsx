import { CssBaseline, Container, Typography, Button } from "@mui/material";
import React from "react";
import { useNavigate  } from "react-router-dom";

export const AppHome = () => {
	const navigate = useNavigate();

	const handleLogin = () => {
		navigate("/login");
	};

	return (
		<React.Fragment>
			<CssBaseline />

			<Container maxWidth="xl">
				<Typography variant="h2" component="h2" gutterBottom>
					Welcome to the app!
				</Typography>

				<Typography variant="h4" component="h4" gutterBottom >
					Please login in order to use functionalities
				</Typography>

				<Button variant="contained" color="primary" onClick={handleLogin}>
					Login
				</Button>

			</Container>
		</React.Fragment>
	);
};
