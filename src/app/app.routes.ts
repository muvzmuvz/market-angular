import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { ProfilePage } from './pages/profile-page/profile-page';
import { DeliveryPage } from './pages/delivery-page/delivery-page';
import { CartPage } from './pages/cart-page/cart-page';
import { Install } from './pages/install/install';
import { ProductDetailComponent } from './pages/product-detail/product-detail';
import { StorePage } from './pages/store-page/store-page';
import { initGuard } from './guards/init/init-guard';
import { startupRedirectGuard } from './guards/startup-redirect-guard';
//canActivate: [startupRedirectGuard],
export const routes: Routes = [
    {
        path: '',  component: HomePage
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
        path: 'install', loadComponent: () => import('./pages/install/install').then(m => m.Install), canActivate: [initGuard]
    },
    {
        path: 'product/:id', component: ProductDetailComponent
    },
    {
        path: 'store', component: StorePage
    },
];