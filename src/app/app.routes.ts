import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { ProfilePage } from './pages/profile-page/profile-page';
import { DeliveryPage } from './pages/delivery-page/delivery-page';
import { CartPage } from './pages/cart-page/cart-page';
import { Install } from './pages/install/install';
import { ProductDetailComponent } from './pages/product-detail/product-detail';
import { StorePage } from './pages/store-page/store-page';
import { initGuard } from './guards/init/init-guard';
import { StartupRedirectGuard } from './guards/startup-redirect-guard';
import { AuthGuard } from './guards/auth/auth.guard';
//canActivate: [startupRedirectGuard],
export const routes: Routes = [
    {
        path: '',  component: HomePage , canActivate: [StartupRedirectGuard]
    },
    {
        path: 'profile', loadComponent: () => import('./pages/profile-page/profile-page').then(m => m.ProfilePage), canActivate: [StartupRedirectGuard, AuthGuard]
    },
    {
        path: 'delivery', component: DeliveryPage, canActivate: [StartupRedirectGuard]
    },
    {
        path: 'cart', component: CartPage, canActivate: [StartupRedirectGuard]
    },
    {
        path: 'install', loadComponent: () => import('./pages/install/install').then(m => m.Install), canActivate: [initGuard]
    },
    {
        path: 'product/:id', component: ProductDetailComponent, canActivate: [StartupRedirectGuard]
    },
    {
        path: 'store', component: StorePage, canActivate: [StartupRedirectGuard]
    },
];