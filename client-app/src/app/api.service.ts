import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  //apiBaseURI:string =`https://z28bd4vmse.execute-api.eu-central-1.amazonaws.com/Prod`;
// apiBaseURI:string=`https://localhost:44329`;

  apiBaseURI: string = environment.apiBaseURI;

  constructor(private http: HttpClient) { }

  getProducts() {
    return this.http.get(`${this.apiBaseURI}/api/products`);
  }

  getEnv() {
    return this.http.get(`${this.apiBaseURI}/api/values/getEnv`);
  }


  getUsers() {
    return this.http.get(`${this.apiBaseURI}/api/users`);
  }

  getToken(){
    return this.http.get(`${this.apiBaseURI}/api/token`);
  }

  deleteUser(id:number){    
    return this.http.delete(`${this.apiBaseURI}/api/users/${id}`);
  }
}
