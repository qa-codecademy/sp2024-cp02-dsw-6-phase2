import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ProductCardComponent } from '../product-card/product-card.component';
import { MatGridListModule } from '@angular/material/grid-list'
import { ProductService } from '../../services/product.services';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ProductDto } from '../../types/interfaces/product.interface';

@Component({
  selector: 'app-product-cards-cat2',
  standalone: true,
  imports: [CommonModule,
    MatGridListModule,
    ProductCardComponent,
    MatIconModule,
    MatButtonModule],
  templateUrl: './product-cards-cat2.component.html',
  styleUrl: './product-cards-cat2.component.scss'
})
export class ProductCardsCat2Component implements OnInit {
  products: ProductDto[] = [];
  breakPoint: number = 3 
  constructor(private productServices: ProductService){}
  
  ngOnInit(): void {
    this.fetchAllProducts(); 
    this.onResize({ target: { innerWidth: window.innerWidth } });
  }
  
  fetchAllProducts(): void {
    this.productServices.filterByCategory(3).subscribe(
      (products) => {
        this.products = products;
        console.log('Fetched Products:', this.products);
    
      },
      (error) => {
        console.error('Error fetching products:', error); 
      }
    );
  }
  onResize(event: any) {
   
    const width = event.target.innerWidth;
    if (width < 600) {
      this.breakPoint = 1; // 1 column on small screens
    } else if (width < 900) {
      this.breakPoint = 2; // 2 columns on medium screens
    } else {
      this.breakPoint = 3; // 3 columns on large screens
    }
  }
  
  trackByProduct(index: number, product: ProductDto): any {
    return product.id; 
  }
  
  }
  