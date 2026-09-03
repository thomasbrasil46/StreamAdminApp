import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Login } from './pages/login/login';
import { Navbar } from './pages/navbar/navbar';
import { RegisterUserAccess } from './pages/register-user-access/register-user-access';

export const routes: Routes = [
    {
        path:'',
        redirectTo: 'home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: Home
    },
    {
        path: 'login',
        component: Login
    },
    {
        path: 'register-user',
        component: RegisterUserAccess
    },
    {
        path: 'navbar',
        component: Navbar
    }
];
