import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { ProfilePage } from './pages/profile-page/profile-page';

export const routes: Routes = [
    {
        path:'', component: HomePage
    },
    {
        path:'profile', component: ProfilePage
    }
];
