import { Component, computed,  Input, OnInit, SimpleChange } from '@angular/core';
import { Product, ProductDto } from '../../types/interfaces/product.interface';
import { UserService } from '../../services/auth.services';
import { ProductService } from '../../services/product.services';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { CartService } from '../../services/cart.services';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule,
    MatCardModule,
    MatChipsModule,
    MatIconModule,
    MatButtonModule
    

  ],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss'
})
export class ProductCardComponent implements OnInit {
  @Input() product: ProductDto | undefined;
  isLoggedIn = computed(() => this.authService.isLoggedIn());
  quantity: number = 1;

  constructor(private authService:UserService,
  private   productService:ProductService,
  private cartService:CartService,
  private router :Router

  ){}
  ngOnInit() {
  
  }
  openProductShowcase(): void {
    if (this.product?.id) {
      this.router.navigate(['/product', this.product.id]);
    }
  }


 
  
  addToCart(): void {
    const userId = Number(this.authService.getUserId());
    console.log('Attempting to add to cart with:', { userId, productId: this.product!.id, quantity: this.quantity });
    if (userId) {
      this.cartService.addProductToCart( userId, this.product!.id, this.quantity).subscribe(
        () => {
          console.log(`Added ${this.product!.productName} to cart.`);
        },
        (error) => {
          console.log('Error response body:', error.error);
          console.error('Error adding product to cart:', error);
        }
      );
    } else {
      console.error('User is not logged in.');
    }
  }
  }

