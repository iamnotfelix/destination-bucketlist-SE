import { Box, AppBar, Toolbar, IconButton, Typography, Button } from "@mui/material";
import { Link, useLocation } from "react-router-dom";
import HomeIcon from '@mui/icons-material/Home';
import AirplanemodeActiveIcon from '@mui/icons-material/AirplanemodeActive';

export function AppMenu(props){
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
						variant={path.startsWith("/shelter") ? "outlined" : "text"}
						to="/shelter"
						component={Link}
						color="inherit"
						sx={{ mr: 5 }}
						startIcon={<AirplanemodeActiveIcon />}>
						All destinations
					</Button>
					{props.userid ? (
						<>
							<Button onClick={props.handleLogout}>
								Logout
							</Button>
						</>
					) : (
						<>
							<Typography variant="h6" component="div" sx={{ mr: 5 }}>
								User ID: {props.userid}
							</Typography>
						</>
					)}
				</Toolbar>
			</AppBar>
		</Box>
	);
}