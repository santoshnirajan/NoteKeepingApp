
import { Component } from '@angular/core';
import { LoginModel } from '../common/login.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../service/auth.service';
import { Router, RouterModule } from '@angular/router';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
LoginObj:LoginModel = new LoginModel();
showPassword = false;
showPasswordText: boolean = false;
errorMessage: string = '';

constructor( private _authService:AuthService, private _router:Router){}



OnLogin() {
  this._authService.Login(this.LoginObj).subscribe({
    next: (res) => {
      this.errorMessage = ''; 
      this._router.navigate(['home']);
      this.LoginObj = new LoginModel(); 
    },
    error: (err) => {
      console.error('Login failed', err);

      if (err.status === 401) {
        this.errorMessage = "Username or password doesn't match.";
      } else {
        this.errorMessage = "Something went wrong. Please try again.";
      }
    }
  });
}


Register(){
  this._router.navigate(['registration']);
}
togglePasswordVisibility() {
  this.showPassword = !this.showPassword;
}



togglePassword() {
  this.showPasswordText = !this.showPasswordText;
}

clearError() {
  this.errorMessage = '';
}
}
