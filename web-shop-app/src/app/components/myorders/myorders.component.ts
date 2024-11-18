import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.services';
import { UserService } from '../../services/auth.services';
import { Router } from '@angular/router';
import { Order } from '../../types/interfaces/order.interface';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';  // Import MatTableModule



@Component({
  selector: 'app-myorders',
  standalone: true,
  imports: [CommonModule,
    MatProgressSpinnerModule,
    MatListModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule
    
    

  ],
  templateUrl: './myorders.component.html',
  styleUrl: './myorders.component.scss'
})
export class MyordersComponent implements OnInit {
  orders: Order[] = [];
  isLoading: boolean = false;
  errorMessage: string | null = null;
  displayedColumns: string[] = ['id', 'orderDate', 'address', 'totalAmount', 'viewDetails']; 

  constructor(
    private orderService: OrderService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.fetchOrders();
  }


  fetchOrders(): void {
    this.isLoading = true;
  
    // Pass 'true' to get only the logged-in user's orders
    this.orderService.getOrderForUser(true).subscribe(
      (orders) => {
        this.orders = orders;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching orders:', error);
        this.isLoading = false;
      }
    );
  }

  viewOrderDetails(orderId: number): void {
    this.router.navigate(['/orderdetails', orderId]); // Assuming you have a route to view order details
  }
}