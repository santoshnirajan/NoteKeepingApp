import { FormBuilder, FormControl, FormGroup } from "@angular/forms";

export class RegistrationModel{
    public FirstName:string="";
    public LastName :string="";
    public UserName:string="";
    public Email:string="";
    public Password:string="";
    public UserRegistrationForm:FormGroup;  

constructor()
{
let formBuilder = new FormBuilder();
this.UserRegistrationForm = formBuilder.group({
  FirstName:new FormControl(''),
  LastName:new FormControl(''),
  UserName:new FormControl(''),
  Email:new FormControl(''),
  Password:new FormControl('')
})
 }
}