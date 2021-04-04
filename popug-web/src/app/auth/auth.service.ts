import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authServiceUrl = 'https://localhost:9002/api/auth';

  constructor(private http: HttpClient,
    private cookie: CookieService) { }

  getAccessToken(): string {
    return this.cookie.get('auth');
  }

  login(email: string, password: string): Promise<User> {    
    return this.http.get<User>(this.authServiceUrl + `/login?email=${email}&password=${password}`)
      .toPromise().then(data => {
        this.cookie.set('auth', data.token);
        this.cookie.set('userName', data.userName);
        this.cookie.set('userId', data.userId);
        this.cookie.set('userRole', data.role);
        return data;
      });
  }

  isLogged(): boolean {
    var token = this.getAccessToken();
    if (token)
      return true;
    return false;
  }

  getUserName(): string {
    return this.cookie.get('userName');
  }

  getUserId(): string {
    return this.cookie.get('userId');
  }

  getUserRole(): string {
    return this.cookie.get('userRole');
  }

  logout(): Promise<boolean>{
    return this.http.get(this.authServiceUrl + '/logout')
      .toPromise().then(_ => {
        this.cookie.delete('auth');
        this.cookie.delete('userName');
        this.cookie.delete('userId');
        this.cookie.delete('userRole');
        return true;
      });
  }
}
