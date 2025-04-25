import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { UserBase } from '../models/userBase';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl=environment.apiUrl;

      constructor(private router:Router
        ,private http:HttpClient
      ) { }

  getUsers(filter:string){
    return this.http.get<UserBase[]>(this.baseUrl+'users?EmailLike='+filter).pipe(
      map((result:UserBase[])=>{
        return result;
      })
    )
  }
}
