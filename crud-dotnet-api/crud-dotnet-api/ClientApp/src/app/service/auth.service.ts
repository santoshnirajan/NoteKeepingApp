import { Injectable, afterNextRender } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginModel } from '../common/login.model';
import { Router } from '@angular/router';
import { RegistrationModel } from '../common/registration.model';
import { NoteModel } from '../common/note.model';
import { NotesDTO } from '../common/notes.dto';


@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private jwtHelper = new JwtHelperService();
    private loggedIn = new BehaviorSubject<boolean>(this.isTokenValid());
    public LoginUser = new LoginModel();
    private _baseUrl = 'https://localhost:7213/api'

    constructor(private http: HttpClient, private _router: Router) { }

    Login(login: LoginModel): Observable<any> {
        return this.http.post<any>(`${this._baseUrl}/Account/Login`, login).pipe(
            map(response => {
                if (response.token) {
                    this.setToken(response.token);
                    this.loggedIn.next(true);
                }
                return response;
            }),
            catchError(error => {
                console.error('Login error:', error);
                return throwError(error);
            })
        );
    }

    Register(user: RegistrationModel): Observable<any> {
        return this.http.post(`${this._baseUrl}/Account/Register`, user);
    }
    SaveNotes(note: any): Observable<any> {
        return this.http.post(`${this._baseUrl}/Notes/AddNote`, note);
    }
    SaveFile(fileData: any): Observable<any> {
        return this.http.post(`${this._baseUrl}/Files/UploadNote`, fileData);
    }
    downloadFile(filePath: string): Observable<Blob> {
        return this.http.get(`${this._baseUrl}/Files/DownloadFiles`, {
          params: { path: filePath },
          responseType: 'blob'
        });
      }
    getFiles(): Observable<any> {
        return this.http.get<any>(`${this._baseUrl}/Files/GetFiles`);
      }

      getFilesByUserId(userId: string): Observable<any> {
        return this.http.get<any>(`${this._baseUrl}/Files/GetFilesByUser/${userId}`);
    }

    GetNotes(userId: string): Observable<any> {
        return this.http.get(`${this._baseUrl}/Notes/GetNotesByUserId?userId=${userId}`);
      }

      EditNote(note:NotesDTO){
        return this.http.put(`${this._baseUrl}/Notes/UpdateNote`, note);
      }
      DeleteNote(noteId:string){
        return this.http.delete(`${this._baseUrl}/Notes/DeleteNoteById?noteId=${noteId}`)
      }

    generateQR(data:any): Observable<any> {
        return this.http.post(`${this._baseUrl}/QRCode/Generate`, data, {
            responseType: 'blob'
          });;
    }


    setToken(token: string) {
        localStorage.setItem('jwtToken', token);
    }
    getToken(): string | null {
        const token = localStorage.getItem('jwtToken');
        if (token) {
            return token;
        }
        return null;
    }

    logout(): void {
        localStorage.clear();
        this.loggedIn.next(false);
        this._router.navigate(['login']);
    }

    isLoggedIn(): Observable<boolean> {
        return this.loggedIn.asObservable();
    }

    isTokenExpired(): boolean {
        const token = this.getToken();
        return token ? this.jwtHelper.isTokenExpired(token) : true;
    }

    private isTokenValid(): boolean {
        return !this.isTokenExpired();
    }

 decodeJwt(token: string): any {
    try{
        const payload = token.split('.')[1];
        const decodedPayload = atob(payload.replace(/-/g, '+').replace(/_/g, '/'));
        return JSON.parse(decodedPayload);
 
    }catch(error){
        console.error('Error decoding the token:', error);
    return null;
    }
    }
    

}
