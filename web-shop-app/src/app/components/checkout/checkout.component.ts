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
import { MAT_DATE_FORMATS, MatNativeDateModule } from '@angular/material/core';
import moment from 'moment';
import { MatButtonModule } from '@angular/material/button';


export const MY_FORMATS = {
  parse: { dateInput: 'MM/YYYY' },
  display: {
    dateInput: 'MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatIconModule,
    MatDatepickerModule,
    MatButtonModule,
    
    MatNativeDateModule
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss',
  providers: [{ provide:MAT_DATE_FORMATS, useValue: MY_FORMATS }],
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
        phone: [this.user?.phone || '', [Validators.required, Validators.pattern('^[0-9]{9}$')]],
        address: [this.user?.address || '', [Validators.required, Validators.minLength(5)]],
        description: ['', Validators.required],
        cardNumber: ['', [Validators.required, Validators.pattern('^[0-9]{16}$')]],
        expiryDate: ['', [Validators.required, this.expiryDateValidator]],
        cvv: ['', [Validators.required, Validators.pattern('^[0-9]{3}$')]] // CVV
      });
    }


 
    expiryDateValidator(control: any) {
      const inputValue = control.value;
      // Regular expression for MM/YYYY format
      const regex = /^(0[1-9]|1[0-2])\/(\d{2})$/;
    
      // Check if input matches MM/YYYY format
      if (!regex.test(inputValue)) {
        return { invalidDate: true };
      }
    
      const [month, year] = inputValue.split('/').map(Number);
    
      // Get current month and year
      const currentDate = new Date();
      const currentYear = currentDate.getFullYear() % 100; // Last two digits of year
      const currentMonth = currentDate.getMonth() + 1;
    
      // Check if the date is in the future
      if (year < currentYear || (year === currentYear && month < currentMonth)) {
        return { expiredDate: true };
      }
    
      return null; // Valid if no issues
    }
    

  setExpiryDate(event: any, datepicker: MatDatepicker<any>) {
    const selectedDate = moment(event).startOf('month');
    this.checkoutForm.get('expiryDate')?.setValue(selectedDate);
    datepicker.close();
  }




  makeAnOrder():void{
    const userId = Number(this.userServices.getUserId());
  
    // Get the user cart
    this.cartServices.getUserCart(userId).subscribe(
      cart => {
        
        console.log('Fetched Cart:', cart);
  
        if (cart && cart.id) {
          const cartId = cart.id; 
          this.orderServices.submitOrderFromCart(cartId).subscribe(
            response => {
              
              console.log('Order submitted successfully', response);
              setTimeout(() => {
                // Navigate to the orders page after a 3-second delay
                this.router.navigate(['/myorders']);
              }, 1000);
            },
            error => {
             
              console.error('Error submitting order', error);
            }
          );
        } else {
          console.error('Cart is empty or does not have an ID');
        }
      },
      error => {
       
        console.error('Error fetching cart', error);
      }
    );}
  
    onSubmitOrder() {
      if (this.checkoutForm.invalid) {
        return;
      }
      this.makeAnOrder();
      
      
    }}