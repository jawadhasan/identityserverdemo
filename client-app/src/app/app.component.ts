import { Component } from '@angular/core';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isLoggedIn: Boolean = false;

  constructor(private _authService: AuthService) {

    this._authService.loginChanged.subscribe(loggedIn=> {
      this.isLoggedIn = loggedIn;
    })

  }

  ngOnInit() {

    this._authService
      .isLoggedIn()
      .then(loggedIn => {
        this.isLoggedIn = loggedIn;
      });

  }

  login(){
    this._authService.login(); //this returns a promise, but rarely a reason to handle it, because app will unload anyway for the sts redirect
  }

  logout(){
    //todo:
  }
}
