import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ProductsComponent } from './products/products.component';
import { SigninCallbackComponent } from './signin-callback.component';
import { SignoutCallbackComponent } from './signout-callback.component';
import { AuthInterceptorService } from './auth-intercepter.service';

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    SigninCallbackComponent,
    SignoutCallbackComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
