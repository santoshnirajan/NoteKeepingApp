import { FormBuilder, FormControl, FormGroup } from "@angular/forms";

export class NoteModel{
    UserId:string="";
    Label:string="";
    Titile:string="";
    Content:string="";
    BackgroundColor:string="";
    FontColor:string="";  
    NoteRegistrationForm:FormGroup;

    constructor(){
        let formBuilder = new FormBuilder();
        this.NoteRegistrationForm = formBuilder.group({
         Label: new FormControl(''),
         Titile: new FormControl(''),
         Content: new FormControl(''),
         BackgroundColor: new FormControl(''),
         FontColor: new FormControl('')
        })
    }
}