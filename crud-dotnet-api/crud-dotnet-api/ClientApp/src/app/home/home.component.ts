import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { JutUserModel } from '../common/jwtuser.model';
import { CommonModule } from '@angular/common';
import { NoteModel } from '../common/note.model';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NotesDTO } from '../common/notes.dto';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-notes',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule,MatButtonModule,RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  jwtPayLoad: JutUserModel = new JutUserModel()
  IsAddNote:boolean=false;
  IsUpdate:boolean=false;
  NoteDetails:NoteModel = new NoteModel();
  NoteList:Array<NotesDTO> = new Array<NotesDTO>();
  NoteRegistrationForm:FormGroup;
  SelectedNote:NotesDTO = new NotesDTO();
  SelectedId:string="";
  ShowNotes:Boolean = true;
  searchTerm: string = '';


  IsFieldInvalid(field:string):boolean{
  const control = this.NoteRegistrationForm.get(field);
  return !!(control && !control.valid && control.touched)
  }
  constructor(private _authService: AuthService, private _formBuilder:FormBuilder) {

    this.NoteRegistrationForm = this._formBuilder.group({
      UserId:[''],
      Title: ['', Validators.required],
      Label: ['', Validators.required],
      Content: ['', Validators.required],
      BackgroundColor: ['#f8f9fa'], // default background
      FontColor: ['#000000'],       // default font
    })
  }
  ngOnInit(): void {
    this.getTokenPayLoad();
    this.GetNotes();
  }
  getTokenPayLoad() {
    let token = this._authService.getToken();
    if (token) {
      this.jwtPayLoad = this._authService.decodeJwt(token);
    }
  }

  OnLogout(): void {
    localStorage.clear()
    this._authService.logout();
  }
  Add(){
    this.IsAddNote = true;
    this.ShowNotes = false;
  }
  Close(){
    this.IsAddNote = false;
    this.IsUpdate = false;
    this.ShowNotes = true;
    this.NoteRegistrationForm.reset();
  }


  onSubmit() {
    this.NoteRegistrationForm.value.UserId = this.jwtPayLoad.UserId;
    if (this.NoteRegistrationForm.valid) {
      let noteData = this.NoteRegistrationForm.value;
      this._authService.SaveNotes(noteData).subscribe(
        res => {
          this.NoteRegistrationForm.reset();
          this.GetNotes();
          this.IsAddNote = false;
          this.ShowNotes = true;
          alert("Note Saved Successfully");
        },
        err => {
          alert("Note cannot saved");
        }
      );
    }
}
GetNotes(){
  const userId = this.jwtPayLoad.UserId;
  this._authService.GetNotes(userId).subscribe(
    res =>{
      this.NoteList = res;
    }
  )
}

EditNote(item:NotesDTO){
  this.NoteRegistrationForm.patchValue(item);
  this.ShowNotes = false;
  this.IsUpdate = true;
  this.IsAddNote = true;
  this.SelectedId = item.Id;
}

UpdateNote(){
  this.NoteRegistrationForm.value.Id = this.SelectedId;
  if(this.NoteRegistrationForm.valid){
    let noteData = this.NoteRegistrationForm.value;
  this._authService.EditNote(noteData).subscribe(
    res => {
      this.NoteRegistrationForm.reset();
      this.GetNotes();
      this.IsAddNote = false;
      this.ShowNotes = true;
      this.IsUpdate = false;
      alert("Note Updated Successfully");
    },
    err => {
      alert("Note cannot Update");
    }
  );
}
}

DeleteNote(item: NotesDTO) {
  const confirmDelete = confirm("Are you sure you want to delete this note?");
  if (!confirmDelete) return;

  const noteId = item.Id;
  this._authService.DeleteNote(noteId).subscribe({
    next: () => {
      // Remove the deleted note from NoteList
      this.NoteList = this.NoteList.filter(n => n.Id !== noteId);
      alert("Note deleted successfully");
    },
    error: () => {
      alert("Note cannot be deleted");
    }
  });
}


filteredData() {
  if (!this.searchTerm) {
    return this.NoteList;
  }
  
  return this.NoteList.filter(item => 
    item.Title.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
    item.Label.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
    item.Content.toLowerCase().includes(this.searchTerm.toLowerCase())
  );
}
}

