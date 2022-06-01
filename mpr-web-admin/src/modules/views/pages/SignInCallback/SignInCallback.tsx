import Button from '@material-ui/core/Button';
import react, { useEffect, useState } from 'react';
import { userManager } from '../../../state-mgmt/auth/UserManager';
import { Navigate } from 'react-router';
import { ROUTES } from '../../../../constants';
import { User } from 'oidc-client-ts';
import Loader from '../../shared/Loader';

const SignInCallback = () => {
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        // the login redirect has been completed and we call the
        // signinRedirectCallback to fetch the user data
        async function signinAsync() {
            var user = await userManager.signinRedirectCallback();            
            setUser(user);
            setIsLoading(false);
          }
        signinAsync();
    }, []);

    const handleClick = async () => {
        let user = await userManager.getUser()
        console.log(user?.profile.roles)
    }

    const renderElement = () : JSX.Element => {
        if (isLoading) return <Loader />;
        if (user === null) return <Navigate to="/" />;
        return (
            <Navigate to={ROUTES.DASHBOARD.path} />
        );
      }

    return(
        renderElement()
    );

    /*useEffect(() => {
        async function signinAsync() {
          var user = await userManager.signinRedirectCallback()
          // redirect user to home page
          console.log(user);
        }
        signinAsync()
      }, [])

    const handleClick = async () => {
        let user = await userManager.getUser()
        console.log(user?.profile.roles)
    }

    return(
    <>
        <h1>
            This is the Protected
        </h1>
        <Button onClick={async event => await handleClick()}>
            GetUser
        </Button>
    </>
    );*/
}

export default SignInCallback;