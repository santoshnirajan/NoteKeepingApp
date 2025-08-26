import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-qr-code',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './qr-code.component.html',
  styleUrl: './qr-code.component.css'
})
export class QrCodeComponent {
  infoForm: FormGroup;
  qrCodeUrl: string | null = null;

  constructor(private fb: FormBuilder, private http: HttpClient, private _router: Router, private _authService: AuthService) {
    this.infoForm = this.fb.group({
      name: [''],
      email: [''],
      phone: [''],
      address: ['']
    });
  }

  generateQRCode() {
    const data = this.infoForm.value;
    this._authService.generateQR(data).subscribe(response => {
      const url = URL.createObjectURL(response);
      this.qrCodeUrl = url;
      this.infoForm.reset();
    }, error => {
      console.error('Error generating QR Code:', error);
    });
  }

goBack(){
  this._router.navigateByUrl("/home");
}
}
