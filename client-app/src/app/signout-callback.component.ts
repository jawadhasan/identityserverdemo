import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-signout-callback',
  template: `<div></div>`,
  styleUrls: ['./app.component.css']
})
export class SignoutCallbackComponent {

    constructor(private _authService: AuthService, 
        private _router: Router) {  }
    
    
        ngOnInit(){
          this._authService.completeLogout().then(_=>{
                  
            this._router.navigate(['/'], {replaceUrl: true});//navigate to root and also remove the signinredirect from the back navigation stack
    
          });
        }

}