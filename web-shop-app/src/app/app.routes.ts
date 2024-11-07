import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ProductlistComponent } from './components/productlist/productlist.component';
import { ProductshowcaseComponent } from './components/productshowcase/productshowcase.component';

export const routes: Routes = [

    { path: '', component: HomeComponent},

    {path: 'login',
        loadComponent: () => import('./components/login/login.component').then((module) => module.LoginComponent)
    },
    {path: 'products',
        loadComponent: () => import('./components/products-cat/products-cat.component').then((module) => module.ProductsCatComponent)
    },
    { path: 'productlist/:name', component: ProductlistComponent },
    { path: 'product/:id', component: ProductshowcaseComponent },
    {path: 'cart',
        loadComponent: () => import('./components/cart/cart.component').then((module) => module.CartComponent)
    },
    {path: 'sale',
        loadComponent: () => import('./components/sale/sale.component').then((module) => module.SaleComponent)
    },
    {path: 'checkout',
        loadComponent: () => import('./components/checkout/checkout.component').then((module) => module.CheckoutComponent)
    },



];
