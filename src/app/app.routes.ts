import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { ProfilePage } from './pages/profile-page/profile-page';
import { DeliveryPage } from './pages/delivery-page/delivery-page';
import { CartPage } from './pages/cart-page/cart-page';
import { Install } from './pages/install/install';
import { ProductDetailComponent } from './pages/product-detail/product-detail';

export const routes: Routes = [
    {
        path: '', component: HomePage
    },
    {
        path: 'profile', component: ProfilePage
    },
    {
        path: 'delivery', component: DeliveryPage
    },
    {
        path: 'cart', component: CartPage
    },
    {
        path: 'install', component: Install
    },
       {
        path: 'product/:id', component: ProductDetailComponent
    },
];
