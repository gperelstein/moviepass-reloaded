import { User } from 'oidc-client-ts';
import { useEffect, useState } from 'react';
import { Navigate } from 'react-router';
import { ROUTES } from '../../../../constants';
import { userManager } from '../../../state-mgmt/auth/UserManager';

const Landing = () => {
    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        async function Redirect(){
            const userCheck = await userManager.getUser();
            setUser(userCheck)
            if(userCheck === null || userCheck.expired){                
                userManager.signinRedirect().then((x) => {
                    console.log(x);
                })
            }
        }
        Redirect();
    }, []);

    return(
        <>
            { user === null || user.expired ?
                null :
                <Navigate to={ROUTES.DASHBOARD.path} /> }
        </>
    );
}

export default Landing;