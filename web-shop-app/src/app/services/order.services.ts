import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError, Observable, of, tap } from 'rxjs';
import { Order, OrderItem, Product } from '../types/interfaces/order.interface';
import { apiUrl, snackBarConfig } from '../constants/app.constants';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  activeOrder = signal<OrderItem[]>([]);
  selectedProducts = signal<Product[]>([]);
  currentUser = signal<string | null>(null);

  constructor(
    private http: HttpClient,
    private snackBar: MatSnackBar
  ) {}

  // Update the active order
  updateActiveOrder(order: OrderItem[]): void {
    this.activeOrder.set(order);
  }

  // Update selected products
  updateSelectedProducts(products: Product[]): void {
    this.selectedProducts.set(products);
  }

  // Get orders for a specific user
  getOrderForUser(isOrderForUser: boolean): Observable<Order[]> {
    const params = new HttpParams().set('isOrderForUser', isOrderForUser.toString());

    return this.http.get<Order[]>(`${apiUrl}/Order`, { params }).pipe(
      tap((response: any) => {
        if (response && response.user && response.user.username) {
          this.currentUser.set(response.user.username);
        }
      }),
      catchError((error) => {
        this.snackBar.open(
          error?.error?.errors?.[0] || `Error while fetching orders!`,
          'Close',
          snackBarConfig
        );
        return of([]);
      })
    );
  }

  
  submitOrderFromCart(cartId: number): Observable<void> {
    return this.http.post<void>(`${apiUrl}/Order/CeateOrderFromCart?cartId=${cartId}`, {}).pipe(
      tap(() => {
        this.snackBar.open(
          'Order created successfully!',
          'Close',
          snackBarConfig
        );
      }),
      catchError((error) => {
        this.snackBar.open(
          error?.error?.errors?.[0] || 'Error while creating order from cart!',
          'Close',
          snackBarConfig
        );
        return of();
      })
    );
  }



  // Fetch a specific order by its ID
  getOrderById(orderId: number): Observable<Order> {
    return this.http.get<Order>(`${apiUrl}/Order/${orderId}`).pipe(
      catchError((error) => {
        this.snackBar.open(
          'Error fetching order details!',
          'Close',
          snackBarConfig
        );
        return of();
      })
    );
  }

  // Fetch all orders
  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${apiUrl}/Order/all`).pipe(
      catchError((error) => {
        this.snackBar.open(
          'Error fetching all orders!',
          'Close',
          snackBarConfig
        );
        return of([]);
      })
    );
  }

  // Delete an order by ID
  deleteOrder(orderId: number): Observable<void> {
    return this.http.delete<void>(`${apiUrl}/Order/${orderId}`).pipe(
      tap(() => {
        this.snackBar.open(
          'Order deleted successfully!',
          'Close',
          snackBarConfig
        );
      }),
      catchError((error) => {
        this.snackBar.open(
          'Error deleting order!',
          'Close',
          snackBarConfig
        );
        return of();
      })
    );
  }
}
