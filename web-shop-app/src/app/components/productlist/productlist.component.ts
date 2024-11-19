import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/auth.services';
import { ProductDto } from '../../types/interfaces/product.interface';
import { ProductService } from '../../services/product.services';
import { CommonModule } from '@angular/common';
import { ProductCardsComponent } from '../product-cards/product-cards.component';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { ProductCardComponent } from '../product-card/product-card.component';

@Component({
  selector: 'app-productlist',
  standalone: true,
  imports: [CommonModule,
    MatIconModule,
    MatGridListModule,ProductCardComponent
  ],
  templateUrl: './productlist.component.html',
  styleUrl: './productlist.component.scss'
})
export class ProductlistComponent implements OnInit {
  category!: number;
  products: ProductDto[] = [];
breakPoint: number = 3 


  constructor(
    private route: ActivatedRoute,
    private productServices: ProductService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const categoryParam = params.get('name'); 
      if (categoryParam) {
        this.category = Number(categoryParam); 
        this.loadProductsByCategory(this.category); 
      }
    });
    this.onResize({ target: { innerWidth: window.innerWidth } });

  }

  loadProductsByCategory(category: number): void { // Accept a number here
    this.productServices.filterByCategory(category).subscribe(
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
    return product.productName; 
  }
}