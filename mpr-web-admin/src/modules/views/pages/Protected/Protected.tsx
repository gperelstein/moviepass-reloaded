import Button from '@material-ui/core/Button';
import react, { useEffect } from 'react';
import { userManager } from '../../../state-mgmt/auth/UserManager';

const Protected = () => {

    useEffect(() => {
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
    );
}

export default Protected;