import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.css';

// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-ignore
export const LoginForm = ({handleLogin}) => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setUsername(e.target.value);
    };

    const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setPassword(e.target.value);
    };

    const handleFormSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            // Perform API request to authenticate user
            const response = await fetch(`https://localhost:7203/api/Auth/login`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ username: username, password: password }),
            });

            if (response.ok) {
                const data = await response.json();
                const token = data.token;
                handleLogin(data.data.id);

                // Store the token in local storage or a secure cookie

                const item = {
                    username: username,
                    token: token
                };

                const itemString = JSON.stringify(item);

                localStorage.setItem('item', itemString);

                // Optionally, you can perform additional logic or redirect the user
            } else {
                throw new Error('Login failed');
            }
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <form onSubmit={handleFormSubmit}>
            <div className="mb-3">
                <label htmlFor="username" className="form-label">
                    Email
                </label>
                <input
                    type="text"
                    className="form-control"
                    id="username"
                    value={username}
                    onChange={handleEmailChange}
                />
            </div>
            <div className="mb-3">
                <label htmlFor="password" className="form-label">
                    Password
                </label>
                <input
                    type="password"
                    className="form-control"
                    id="password"
                    value={password}
                    onChange={handlePasswordChange}
                />
            </div>
            <button type="submit" className="btn btn-primary">
                Login
            </button>
        </form>
    );
};
