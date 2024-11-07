import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ProductService } from '../../services/product.services';
import { CarouselModule } from 'primeng/carousel';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { MatIconModule } from '@angular/material/icon';
import { MatCardImage, MatCardModule } from '@angular/material/card';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Product, ProductDto } from '../../types/interfaces/product.interface';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'carousel-basic-demo',
    templateUrl: './carousel06-component.component.html',
    styleUrls: ['./carousel06-component.component.scss'],
    standalone: true,
    encapsulation:ViewEncapsulation.None,
    imports: [CommonModule,
        CarouselModule,
         ButtonModule, 
         TagModule,
          MatIconModule,
           MatCardModule ,
           MatCardImage],
    providers: [ProductService],
    

})
export class CarouselComponent implements OnInit, AfterViewInit {
  products: ProductDto[] = []; // Initialize the products array

  @ViewChild('carousel') carousel!: ElementRef;

  constructor(private productService: ProductService) {}

  ngOnInit() {
    // Fetch products and get only the first 10
    this.productService.getAllProducts().pipe(
      tap(products => {
       
        this.products = products.slice(5, 50);
        console.log(this.products);
      })
    ).subscribe();
  }

  ngAfterViewInit() {
    // Optional: Center initial position if needed
  }

  scrollLeft() {
    this.carousel.nativeElement.scrollBy({ left: -200, behavior: 'smooth' });
  }

  scrollRight() {
    this.carousel.nativeElement.scrollBy({ left: 200, behavior: 'smooth' });
  }
}
