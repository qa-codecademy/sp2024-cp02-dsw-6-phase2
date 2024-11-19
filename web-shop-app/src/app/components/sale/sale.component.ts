import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ProductCardComponent } from '../product-card/product-card.component';
import { MatGridListModule } from '@angular/material/grid-list'
import {  ProductDto } from '../../types/interfaces/product.interface';
import { ProductService } from '../../services/product.services';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-sale',
  standalone: true,
  imports: [CommonModule,
    MatGridListModule,
    ProductCardComponent,
    MatIconModule,
    MatButtonModule],
  templateUrl: './sale.component.html',
  styleUrl: './sale.component.scss'
})
export class SaleComponent implements OnInit {
  products: ProductDto[] = [];
  breakPoint: number = 3 
  constructor(private productServices: ProductService){}
  
  ngOnInit(): void {
    this.fetchAllProducts(); 
  this.onResize({ target: { innerWidth: window.innerWidth } });

  }
  
  fetchAllProducts(): void {
    this.productServices.getAllProducts().subscribe(
      (products)=> {
        this.products = products.filter(product => product.discount >= 5);
      }
    )
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
  