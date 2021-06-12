import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { SigninCallbackComponent } from './signin-callback.component';



const routes: Routes = [ 
  { path: 'products', component: ProductsComponent },
  { path: 'signin-callback', component: SigninCallbackComponent },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
