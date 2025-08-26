import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-email',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './email.component.html',
  styleUrl: './email.component.css'
})
export class EmailComponent {
  messageForm: FormGroup;

  constructor(private _router:Router, private fb: FormBuilder, private _authService: AuthService){

    this.messageForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      subject: ['', Validators.required],
      message: ['', Validators.required]
    });
  }


  
  goBack(){
    this._router.navigateByUrl("/home");
  }


  sendMessage() {
    if (this.messageForm.valid) {
      const messageData = this.messageForm.value;
      this._authService.sendMessage(messageData).subscribe(response => {
        console.log('Message sent successfully!', response);
      }, error => {
        console.error('Error sending message:', error);
      });
    }
  }

}
