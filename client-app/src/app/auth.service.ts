import { Injectable } from "@angular/core";
import { UserManager, User } from "oidc-client";
import { Subject } from "rxjs";
import { Constants } from "./constants";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private _userManager: UserManager;
    private _user: User;

    private _loginChangedSubject = new Subject<boolean>();
    loginChanged = this._loginChangedSubject.asObservable();

    constructor() {

        //settings depends on IDP and Flow being used
        //we will be using Authorizatio Code + PKCE
        const stsSettings = {
            authority: Constants.stsAuthority,
            client_id: Constants.clientId,

            scope: 'openid profile api1 companyApi', //shall be configured on sts for this client
            response_type: 'code', //for authorization-code + pkce   // change to 'id_token token' for implicit-flow

            redirect_uri: `${Constants.clientRoot}signin-callback`,
            post_logout_redirect_uri: `${Constants.clientRoot}signout-callback`

        };
        console.log(stsSettings);
        this._userManager = new UserManager(stsSettings);
    }


    login(){
        return this._userManager.signinRedirect();
    }

    logout(){
        return this._userManager.signoutRedirect();
    }

    isLoggedIn():Promise<Boolean>{
        return this._userManager
        .getUser()
        .then(user=>{
            const userCurrent = !!user && !user.expired; //if not NULL and not Expired

            if(this._user != user){
                this._loginChangedSubject.next(userCurrent);
            }

            this._user = user;
            return userCurrent;
        });
    }


    
    completeLogin(){
        return this._userManager.signinRedirectCallback().then(user=>{
            this._user = user;
            this._loginChangedSubject.next(!!user && !user.expired);
            return user;
        })
    }

}

  