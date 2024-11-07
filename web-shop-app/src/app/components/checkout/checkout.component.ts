import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { UserService } from '../../services/auth.services';
import { CartService } from '../../services/cart.services';
import { OrderService } from '../../services/order.services';
import { Router } from '@angular/router';
import {MatDatepicker, MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';


@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatIconModule,
    MatDatepickerModule,
    MatDatepicker,
    MatNativeDateModule
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutComponent implements OnInit {
  checkoutForm!: FormGroup;

  user: any;
  constructor( private userServices: UserService,
    private cartServices: CartService,
    private orderServices: OrderService,
    private router: Router,
    private fb: FormBuilder){}
  cart: any = { items: [] }; 


    ngOnInit(): void {
    
      this.loadUserData();
    }
    loadUserData() {     
       // Assuming you have a UserService that fetches user data
        this.userServices.getUser().subscribe(
          (userData) => {
            this.user = userData;  // Store user data in the component
            this.initializeForm(); 
          },
          (error) => {
            console.error('Failed to fetch user data', error);
           
            this.user = {};
            this.initializeForm();
          }
        );
      
       
      
    }
    
  
   
    initializeForm() {
      this.checkoutForm = this.fb.group({
        firstName: [this.user?.name || '', Validators.required],
        lastName: [this.user?.lastName || '', Validators.required],
        email: [this.user?.email || '', [Validators.required, Validators.email]],
        phone: [this.user?.phone || '', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
        address: [this.user?.address || '', [Validators.required, Validators.minLength(5)]],
        description: ['', Validators.required],
        cardNumber: ['', [Validators.required, Validators.pattern('^[0-9]{16}$')]],
        expiryDate: ['', [Validators.required, Validators.pattern('^(0[1-9]|1[0-2])\/([0-9]{2})$')]], // MM/YY format
        cvv: ['', [Validators.required, Validators.pattern('^[0-9]{3}$')]] // CVV
      });
    }
  
    onSubmitOrder() {
      if (this.checkoutForm.invalid) {
        return;
      }
  
      
    }}