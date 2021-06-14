import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-signin-callback',
  template: `<div></div>`,
  styleUrls: ['./app.component.css']
})
export class SigninCallbackComponent {

  constructor(private _authService: AuthService, 
    private _router: Router) {  }


    ngOnInit(){
      this._authService.completeLogin().then(user=>{

        //we can ignore the user coming from the promise, bcoz authservice and userManager already have it cached.

        this._router.navigate(['/'], {replaceUrl: true});//navigate to root and also remove the signinredirect from the back navigation stack

      });
    }

}