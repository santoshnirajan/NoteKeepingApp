import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guard/auth.guard';
import { RegistrationComponent } from './registration/registration.component';


export const routes: Routes = [
    { 
        path: '', 
        redirectTo: 'login', 
        pathMatch: 'full' 
    },  // Default Route

    {
        path: 'login', 
        component: 
        LoginComponent 
    },
    {
        path: 'registration', 
        component: 
        RegistrationComponent 
    },
    { 
        
        path: 'home', loadComponent: () => import('./home/home.component').then(m => m.HomeComponent), canActivate: [AuthGuard] 
    },

    {
        path:'fileupload', loadComponent: () => import('./fileupload/fileupload.component').then ( m => m.FileuploadComponent), canActivate: [AuthGuard]
    },
    {
        path:'qr-code', loadComponent: () => import('./qr-code/qr-code.component').then (m => m.QrCodeComponent), canActivate:[AuthGuard]
    }         
];  

