import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  @Input() email: string;
  @Input() password: string;

  constructor(private authService: AuthService,
    private router: Router) {
    this.email = 'user1@email.com';
    this.password = 'pass123!E';
  }

  ngOnInit(): void {
    if (this.authService.isLogged())
      this.router.navigate(['tasks']);
  }

  login(): void {
    this.authService.login(this.email, this.password)
      .then(isLogged => this.router.navigate(['tasks']));
  }

}
