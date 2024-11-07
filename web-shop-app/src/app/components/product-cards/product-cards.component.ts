import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ProductCardComponent } from '../product-card/product-card.component';
import { MatGridListModule } from '@angular/material/grid-list'
import {  ProductDto } from '../../types/interfaces/product.interface';
import { ProductService } from '../../services/product.services';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-product-cards',
  standalone: true,
  imports: [CommonModule,
    MatGridListModule,
    ProductCardComponent,
    MatIconModule,
    MatButtonModule
    
  ],
  templateUrl: './product-cards.component.html',
  styleUrl: './product-cards.component.scss'
})


export class ProductCardsComponent implements OnInit {
products: ProductDto[] = [];
breakPoint: number = 3;
constructor(private productServices: ProductService){}

ngOnInit(): void {
  this.fetchAllProducts(); 
}

fetchAllProducts(): void {
  this.productServices.filterByCategory(5).subscribe(
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
  
  this.breakPoint = Math.floor(event.target.innerWidth / 600);
}

trackByProduct(index: number, product: ProductDto): any {
  return product.productName; 
}

}
