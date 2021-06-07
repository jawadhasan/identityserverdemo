import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductsComponent } from './products/products.component';
// import { TokenComponent } from './token/token.component';


const routes: Routes = [ 
  { path: 'products', component: ProductsComponent },
  // { path: 'token', component: TokenComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
