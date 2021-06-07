import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {



  products: any;
  constructor(public apiService: ApiService,) { }

  ngOnInit(): void {

    //get data from server
    this.apiService.getProducts().subscribe((res: any) => {

      this.products = res;
      console.log(this.products);

    });
  }
  delete(id: any) {
    console.log("not implemented.", id);
  }



}
