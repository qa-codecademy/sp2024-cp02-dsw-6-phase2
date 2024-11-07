import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Cart } from '../types/interfaces/cart.interface';
import { apiUrl, snackBarConfig } from '../constants/app.constants';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private http: HttpClient,
    private snackBar: MatSnackBar,
    private router:Router

  ) {}

  getUserCart(userId: number): Observable<Cart> {
    return this.http.get<Cart>(`https://localhost:7036/api/Cart?userId=${userId}`).pipe(map(response => {
      return response;
    }));
  }

  addProductToCart(userId: number, productId: number, quantity: number): Observable<any> {
    const url = `https://localhost:7036/api/Cart?userId=${userId}&productId=${productId}&quantity=${quantity}`;
    this.snackBar.open(
      'Product added to cart successfully',
      'Close',
      snackBarConfig
    );
    return this.http.post(url, null);
   
    
}

  clearCart(userId: number): Observable<void> {
    this.snackBar.open(
      'Cart cleard!',
      'Close',
      snackBarConfig
    );
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/cart']); 
    });
    
    return this.http.delete<void>(`${apiUrl}/Cart/clearCart?userId=${userId}`);
  }

  removeProductFromCart(userId: number, productId: number): Observable<void> {
    this.snackBar.open(
      'Product removed successfully',
      'Close',
      snackBarConfig
    );
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/cart']); 
    });
    
    return this.http.delete<void>(`${apiUrl}/Cart?userId=${userId}&productId=${productId}`);
    
  }
 
}