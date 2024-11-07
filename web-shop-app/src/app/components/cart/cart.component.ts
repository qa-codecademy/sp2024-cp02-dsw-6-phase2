import { Component, OnInit } from '@angular/core';
import { Cart } from '../../types/interfaces/cart.interface';
import { CartService } from '../../services/cart.services';
import { UserService } from '../../services/auth.services';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { OrderService } from '../../services/order.services';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule,
    MatCardModule,
    RouterLink,
    RouterLinkActive
    
  ],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent implements OnInit{
  cart: any = { items: [] }; 
  constructor(
    private cartService: CartService,
    private authService: UserService ,
    private orderService:OrderService
  ) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    const userId = Number(this.authService.getUserId());
    console.log('Loading cart for user ID:', userId);
    this.cartService.getUserCart(userId!).subscribe(
      (cart) => {
        this.cart = cart || { items: [] }; 
                console.log('Cart loaded:', this.cart);
                console.log(cart.cartItems)
      },
      (error) => console.error('Error loading cart:', error)
    );
  }

  removeProduct(productId: number): void {
    const userId = this.authService.getUserId();
    this.cartService.removeProductFromCart(userId!, productId).subscribe(() => {
      setTimeout(() => this.loadCart(), 100);
    });
  }

  clearCart(): void {
    const userId =  Number(this.authService.getUserId());
    this.cartService.clearCart(userId!).subscribe(() => {
      this.cart = null;
      
    });
    
  }
  makeAnOrder():void{
    const userId = Number(this.authService.getUserId());
  
    // Get the user cart
    this.cartService.getUserCart(userId).subscribe(
      cart => {
        
        console.log('Fetched Cart:', cart);
  
        if (cart && cart.id) {
          const cartId = cart.id; 
          this.orderService.submitOrderFromCart(cartId).subscribe(
            response => {
              
              console.log('Order submitted successfully', response);
            },
            error => {
             
              console.error('Error submitting order', error);
            }
          );
        } else {
          console.error('Cart is empty or does not have an ID');
        }
      },
      error => {
       
        console.error('Error fetching cart', error);
      }
    );}}