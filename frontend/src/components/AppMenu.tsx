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
						to="/login"
						component={Link}
						color="inherit"
						sx={{ mr: 5 }}
						startIcon={<LoginIcon />}>
						Login
					</Button>
					<Button
						to="/alldestinations"
						component={Link}
						color="inherit"
						sx={{ mr: 5 }}
						startIcon={<AirplanemodeActiveIcon />}>
						All destinations
					</Button>
					<Button
						to="/privatedestinations"
						component={Link}
						color="inherit"
						sx={{ mr: 5 }}
						startIcon={<AirplanemodeActiveIcon />}>
						Private destinations
					</Button>


				</Toolbar>
			</AppBar>
		</Box>
	);
};