import { Component, OnInit } from '@angular/core';
import { Order } from '../../types/interfaces/order.interface';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from '../../services/order.services';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-orderdetails',
  standalone: true,
  imports: [CommonModule,
    MatProgressSpinnerModule,
    MatListModule

  ],
  templateUrl: './orderdetails.component.html',
  styleUrl: './orderdetails.component.scss'
})
export class OrderdetailsComponent implements OnInit {
  order: Order | null = null;
  isLoading = true;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService,
    private router:Router
  ) {}

  ngOnInit(): void {
    // Get the order ID from the route parameter and convert it to a number
    const orderId = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
  
    if (orderId) {
      // Fetch the order details with a number type orderId
      this.fetchOrderDetails(orderId);
    }
  }

  fetchOrderDetails(orderId: number): void {
    this.orderService.getOrderById(orderId).subscribe(
      (order) => {
        this.isLoading = false;
        this.order = order;
      },
      (error) => {
        this.isLoading = false;
        this.errorMessage = 'Error fetching order details';
        console.error('Error fetching order details:', error);
      }
    );
  }

  goBack(): void {
    this.router.navigate(['/myorders']);
  }
}

