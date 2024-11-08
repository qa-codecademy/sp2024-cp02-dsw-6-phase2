import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product, ProductDto } from '../types/interfaces/product.interface'; 
import { apiUrl } from '../constants/app.constants';
@Injectable({
  providedIn: 'root'
})
export class ProductService {
 

  constructor(private http: HttpClient) {}

  getAllProducts(): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(`${apiUrl}/Product`)
  }

  getProductById(productId: number): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${apiUrl}/Product/${productId}`);
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(apiUrl, product);
  }

  deleteProduct(productId: number): Observable<void> {
    return this.http.delete<void>(`${apiUrl}/Product/${productId}`);
  }
  
  filterByCategory(category:number):Observable<ProductDto[]>{
    return this.http.get<ProductDto[]>(`${apiUrl}/Product/Filter?category=${category}`)
  }
}
