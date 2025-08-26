import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegistrationModel } from '../common/registration.model';
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {

  RegistrationForm: FormGroup;
  UserObj: RegistrationModel = new RegistrationModel();
  showPassword: boolean = false;
  showPasswordConfirm: boolean = false;

  IsFieldInvalid(field: string): boolean {
    const control = this.RegistrationForm.get(field);
    return !!(control && !control.valid && control.touched);
  }

  constructor(private _formBuilder: FormBuilder, private _authService: AuthService, private _router: Router) {
       
    this.RegistrationForm = this._formBuilder.group({
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      UserName: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9]+$')]],
      Password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern('^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9]).+$')
      ]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.PasswordMatchValidator });
  }
  PasswordMatchValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const password = control.get('PasswordHash');
    const confirmPassword = control.get('ConfirmPassword');

    if (password && confirmPassword && password.value !== confirmPassword.value) {
      return { 'mismatch': true };
    }
    return null;
  }

  onSubmit() {
    if (this.RegistrationForm.valid) {

      let UserData = this.RegistrationForm.value;
      this._authService.Register(UserData).subscribe(
        res => {
          alert("Registered Successfully");
          this._router.navigateByUrl("/login");
        },
        err => {
          alert("Not Registered");
        }
      );
    }
  }
  
togglePassword(field: 'password' | 'confirm') {
  if (field === 'password') {
    this.showPassword = !this.showPassword;
  } else {
    this.showPasswordConfirm = !this.showPasswordConfirm;
  }
}
}
