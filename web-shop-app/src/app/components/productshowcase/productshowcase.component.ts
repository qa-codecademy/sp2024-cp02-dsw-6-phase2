import { Component, computed, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.services';
import { ProductDto } from '../../types/interfaces/product.interface';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/auth.services';
import { CartService } from '../../services/cart.services';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-productshowcase',
  standalone: true,
  imports: [CommonModule,
    MatButtonModule
  ],
  templateUrl: './productshowcase.component.html',
  styleUrl: './productshowcase.component.scss'
})
export class ProductshowcaseComponent implements OnInit {
  product: ProductDto | null = null;
  discountedPrice: number | undefined;
  quantity: number = 1;
  isLoggedIn = computed(() => this.authService.isLoggedIn());


  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private authService:UserService,
    private cartService:CartService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const productId = params.get('id');
      if (productId) {
        this.fetchProductById(Number(productId)); 
       
        
      }
     
    });
  }

  fetchProductById(id: number): void {
    this.productService.getProductById(id).subscribe(
      (product) => {
        this.product = product;
        if (this.product.discount >= 0) {
          this.calculateDiscountedPrice();
        }
      },
      (error) => {
        console.error('Error fetching product:', error);
      }
    );
  }
  calculateDiscountedPrice() {
    if (this.product) {
      const discountAmount = this.product.price * (this.product.discount / 100);
      this.discountedPrice = Math.round(this.product.price - discountAmount);
      console.log('Discounted price calculated:', this.discountedPrice);
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

