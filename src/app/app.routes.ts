import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { ProfilePage } from './pages/profile-page/profile-page';
import { DeliveryPage } from './pages/delivery-page/delivery-page';
import { CartPage } from './pages/cart-page/cart-page';

export const routes: Routes = [
    {
        path:'', component: HomePage
    },
    {
        path:'profile', component: ProfilePage
    },
    {
        path:'delivery', component: DeliveryPage
    },
    {
        path:'cart', component: CartPage
    }
];
