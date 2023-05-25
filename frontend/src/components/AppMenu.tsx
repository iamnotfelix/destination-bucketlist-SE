import { Box, AppBar, Toolbar, IconButton, Typography, Button } from "@mui/material";
import { Link, useLocation } from "react-router-dom";
import HomeIcon from '@mui/icons-material/Home';
import AirplanemodeActiveIcon from '@mui/icons-material/AirplanemodeActive';
import AppRegistrationIcon from '@mui/icons-material/AppRegistration';
import LoginIcon from '@mui/icons-material/Login';


export const AppMenu = () => {
	const location = useLocation();
	const path = location.pathname;

	return (
		<Box>
			<AppBar style={{backgroundColor:"#34495E"}}>
				<Toolbar>
					<IconButton
						component={Link}
						to="/"
						size="large"
						edge="start"
						color="inherit"
						aria-label="school"
						sx={{ mr: 2 }}>
						<HomeIcon />
					</IconButton>
					<Typography variant="h6" component="div" sx={{ mr: 5 }}>
						Destination bucket list
					</Typography>
                    <Button
						variant={path.startsWith("/register") ? "outlined" : "text"}
						to="/register"
						component={Link}
						color="inherit"
						sx={{ mr: 5 }}
						startIcon={<AppRegistrationIcon />}>
						Register
					</Button>
					<Button 
						variant={path.startsWith("/login") ? "outlined" : "text"}
						to="/login"
						component={Link}
						color="inherit"
						sx={{ mr: 5 }}
						startIcon={<LoginIcon />}>
						Login
					</Button>
					<Button
						variant={path.startsWith("/shelter") ? "outlined" : "text"}
						to="/shelter"
						component={Link}
						color="inherit"
						sx={{ mr: 5 }}
						startIcon={<AirplanemodeActiveIcon />}>
						All destinations
					</Button>


				</Toolbar>
			</AppBar>
		</Box>
	);
};