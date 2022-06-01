import React, { memo, useEffect, useState } from 'react';
import { Route, Navigate, Outlet } from 'react-router';
import Main from '../Main';
import Loader from '../Loader';
import { userManager } from '../../../state-mgmt/auth/UserManager';
import { User } from 'oidc-client-ts';

const ProtectedRoute = () => {

  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [user, setUser] = useState<User | null>(null)

  useEffect(() => {
    async function signinAsync() {
      let user = await userManager.getUser();
      debugger;            
      if(user?.expired){
        await userManager.removeUser();
        await userManager.revokeTokens(["access_token"]);
        user = await userManager.getUser();
      }
      setUser(user);
      console.log(user);
      setIsLoading(false);
    }
    signinAsync()
  }, [])
  
  const renderElement = () : JSX.Element => {
    if (isLoading) return <Loader />;
    if (user === null || user.expired) return <Navigate to="/" />;
    return (
      <Main>
        <Outlet />
      </Main>
    );
  }
  
  return (
    renderElement()
  );
};

export default ProtectedRoute;