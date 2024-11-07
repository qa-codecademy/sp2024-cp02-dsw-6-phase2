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
  }
  
  fetchAllProducts(): void {
    this.productServices.getAllProducts().subscribe(
      (products)=> {
        this.products = products.filter(product => product.discount >= 5);
      }
    )
  }


  onResize(event: any) {
    
    this.breakPoint = Math.floor(event.target.innerWidth / 600);
  }
  
  trackByProduct(index: number, product: ProductDto): any {
    return product.productName; 
  }
  
  }
  