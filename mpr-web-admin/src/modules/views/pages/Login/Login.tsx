import FormControl from '@mui/material/FormControl';
import IconButton from '@mui/material/IconButton';
import InputAdornment from '@mui/material/InputAdornment';
import InputLabel from '@mui/material/InputLabel';
import OutlinedInput from '@mui/material/OutlinedInput';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import React from 'react';
import { Visibility } from '@mui/icons-material';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';

interface State {
    amount: string;
    password: string;
    weight: string;
    weightRange: string;
    showPassword: boolean;
  }

const Login = () => {

    const [values, setValues] = React.useState<State>({
        amount: '',
        password: '',
        weight: '',
        weightRange: '',
        showPassword: false,
    });

    const [name, setName] = React.useState('Cat in the Hat');
    const handleChangTextField = (event: React.ChangeEvent<HTMLInputElement>) => {
        setName(event.target.value);
    };

    const handleChange =
        (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
            setValues({ ...values, [prop]: event.target.value });
    };

    const handleClickShowPassword = () => {
        setValues({
        ...values,
        showPassword: !values.showPassword,
        });
    };

    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };

    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        console.log("Se clickeo");
    }

    return (
        <>
            <FormControl sx={{ m: 1, width: '25ch' }} variant="outlined">
                
                <TextField
                    id="outlined-name"
                    label="Name"
                    value={name}
                    onChange={handleChangTextField}
                />
            </FormControl>
            
            <FormControl sx={{ m: 1, width: '25ch' }} variant="outlined">
                
                <InputLabel htmlFor="outlined-adornment-password">Password</InputLabel>
                <OutlinedInput
                    id="outlined-adornment-password"
                    type={values.showPassword ? 'text' : 'password'}
                    value={values.password}
                    onChange={handleChange('password')}
                    endAdornment={
                        <InputAdornment position="end">
                            <IconButton
                                aria-label="toggle password visibility"
                                onClick={handleClickShowPassword}
                                onMouseDown={handleMouseDownPassword}
                                edge="end"
                            >
                                {values.showPassword ? <VisibilityOff /> : <Visibility />}
                            </IconButton>
                        </InputAdornment>
                    }
                    label="Password"
                />
            </FormControl>
            <FormControl sx={{ m: 1, width: '25ch' }} variant="outlined">
                <Button
                    variant="outlined"
                    onClick={handleClick}
                >
                    Outlined
                </Button>
            </FormControl>
        </>
    );
}

export default Login;