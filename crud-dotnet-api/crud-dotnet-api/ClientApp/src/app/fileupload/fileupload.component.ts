import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../service/auth.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router'
import { JutUserModel } from '../common/jwtuser.model';

@Component({
  selector: 'app-fileupload',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CommonModule],
  templateUrl: './fileupload.component.html',
  styleUrl: './fileupload.component.css'
})
export class FileuploadComponent implements OnInit {
  FileForm: FormGroup;
  selectedFile: File | null = null;
  uploadedFiles: { fileName: string; path: string; url: string; altText: string; description: string }[] = [];
  downloadedFiles: { FileName: string; FilePath: string; Url: string; AltText: string; Description: string }[] = [];
  openPopup:boolean = false;
  showDownloads:boolean = true;
  previewUrl: string | ArrayBuffer | null | undefined = null;
  jwtPayLoad: JutUserModel = new JutUserModel()

  constructor( private _authService:AuthService, private fb: FormBuilder, private _router:Router){
    this.FileForm = this.fb.group({
      AltText: [''],
      Description: [''],
    });
  }


  ngOnInit() {
    this.openPopup = false;
    this.getTokenPayLoad();
    this.getUploadedFiles();
  }

  getTokenPayLoad() {
    let token = this._authService.getToken();
    if (token) {
      this.jwtPayLoad = this._authService.decodeJwt(token);
    }
  }

  OpenAddFilePopup(){
  this.openPopup = true;
  this.showDownloads = false;
  }

  Close(){
    this.openPopup = false;
    this.showDownloads = true;
  }

  goBack(){
    this._router.navigateByUrl("/home");
  }
  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input?.files?.length) {
      this.selectedFile = input.files[0];
    }
  }

  // Method to preview the selected file
  previewFile(file: File) {
    const reader = new FileReader();
    reader.onload = (e: ProgressEvent<FileReader>) => {
      this.previewUrl = e.target?.result ?? null; // Set previewUrl for the selected image
    };
    reader.readAsDataURL(file); // Read the file as a Data URL for previewing
  }


  OnSaveFile() {
    if (!this.selectedFile) return;
    const userId = this.jwtPayLoad.UserId;

    const formData: FormData = new FormData();
    formData.append('File', this.selectedFile);
    formData.append('AltText', this.FileForm.get('AltText')?.value || '');
    formData.append('Description', this.FileForm.get('Description')?.value || '');
    formData.append('CreatedBy', userId);
    this._authService.SaveFile(formData).subscribe(
      (res) => {
        alert('File Saved Successfully');
        this.FileForm.reset();
        this.openPopup = false;
        this.getUploadedFiles();
        this.showDownloads = true;
        this.previewUrl = null; // Reset preview after upload
      },
      (err) => {
        alert('Unable to save file');
      }
    );
  }


  getUploadedFiles() {
    const userId = this.jwtPayLoad.UserId;
    this._authService.getFilesByUserId(userId).subscribe(
      (files) => {
        this.downloadedFiles = files;
      },
      (err) => {
        console.error('Error fetching files', err);
      }
    );
  }


  downloadFile(filePath: string) {
    this._authService.downloadFile(filePath).subscribe(
      (blob) => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = filePath.split('/').pop() || 'downloaded-file';
        a.click();
        window.URL.revokeObjectURL(url);
      },
      (err) => {
        console.error('Error downloading file', err);
      }
    );
  }


  isImage(fileName: string): boolean {
    const imageExtensions = ['jpg', 'jpeg', 'png', 'gif'];
    const extension = fileName.split('.').pop()?.toLowerCase();
    return imageExtensions.includes(extension || '');
  }
  
  isSticker(fileName: string): boolean {
    // Define conditions for stickers if they differ from regular images
    const stickerExtensions = ['webp', 'sticker_extension']; // Add relevant extensions for stickers
    const extension = fileName.split('.').pop()?.toLowerCase();
    return stickerExtensions.includes(extension || '');
  }

  
}
