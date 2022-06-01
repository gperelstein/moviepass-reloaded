import { UserManager, UserManagerSettings, SigninRedirectArgs } from 'oidc-client-ts';

const url = window.location.origin + "/user-manager";

export const settings : UserManagerSettings = {
    authority: "https://localhost:5000",
    client_id: "interactive",
    client_secret: "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",
    redirect_uri: "http://localhost:3000/callback",
    post_logout_redirect_uri: url + "/sample.html",
    response_type: "code",
    scope: "openid profile",
    popup_redirect_uri: url + "/sample-popup-signin.html",
    popup_post_logout_redirect_uri: url + "/sample-popup-signout.html",
    response_mode: "fragment",

    silent_redirect_uri: url + "/sample-silent.html",
    automaticSilentRenew: true,
    //silentRequestTimeout: 10000,

    filterProtocolClaims: true
};

export const args : SigninRedirectArgs = {
    redirectMethod: "assign"
};
export const userManager = new UserManager(settings);