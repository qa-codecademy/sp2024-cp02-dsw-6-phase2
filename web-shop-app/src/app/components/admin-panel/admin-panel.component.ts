import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { UserService } from '../../services/auth.services';
import { ProductService } from '../../services/product.services';
import { AddProductDto, ProductDto } from '../../types/interfaces/product.interface';
import { Order, User } from '../../types/interfaces/order.interface';
import { UpdateUserDto, UserDto } from '../../types/interfaces/auth.interface';
import { OrderService } from '../../services/order.services';
import { ProductCategory, ProductCategoryBE } from '../../types/enums/product-category-enum';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCardModule

  ],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.scss'
})
export class AdminPanelComponent {
  products: ProductDto[] = [];
  users: UserDto[] = [];
  orders: Order[] = [];
  currentSection: 'addProduct' | 'manageProducts' | 'manageOrders' | 'manageUsers' = 'addProduct';
  productForm:FormGroup;
  productCategories = Object.keys(ProductCategory);

  

constructor(private authServices:UserService,
  private productServices:ProductService,
  private orderServices:OrderService,
  private fb:FormBuilder
){
  this.productForm = this.fb.group({
    productname: ['', [Validators.required, Validators.minLength(3)]],
    productdescription: ['', [Validators.required]],
    price: [0, [Validators.required, Validators.min(1)]],
    category: [ProductCategory.DESKTOPPC, [Validators.required]],
    brand: ['', [Validators.required]],
    quantityavailable: [0, [Validators.required, Validators.min(0)]],
    shippingcost: [0, [Validators.required, Validators.min(0)]],
    shippingtime: [0, [Validators.required, Validators.min(0)]],
    originalimagepath: ['', [Validators.required]],
    discount: [0, [Validators.min(0), Validators.max(100)]],
  });
}


 // Fetch all products
 fetchAllProducts(): void {
  this.productServices.getAllProducts().subscribe(
    (products) => (this.products = products),
    (error) => console.error('Error fetching products:', error)
  );
}

// Fetch all users
fetchAllUsers(): void {
  this.authServices.getUsers().subscribe(
    (users) => (this.users = users),
    (error) => console.error('Error fetching users:', error)
  );
}

// Fetch all orders
fetchAllOrders(): void {
  this.orderServices.getAllOrders().subscribe(
    (orders) => (this.orders = orders),
    (error) => console.error('Error fetching orders:', error)
  );
}

// Add product

submitForm(): void {
  if (this.productForm.valid) {
    const formData = this.productForm.value;

    // Map the selected ProductCategory to ProductCategoryBE
    const categoryMapping: Record<ProductCategory, ProductCategoryBE> = {
      [ProductCategory.HOBS]: ProductCategoryBE.HOBS,
      [ProductCategory.DESKTOPPC]: ProductCategoryBE.DESKTOPPC,
      [ProductCategory.OVENS]: ProductCategoryBE.OVENS,
      [ProductCategory.RANGE]: ProductCategoryBE.RANGE,
      [ProductCategory.GAMINGPC]: ProductCategoryBE.GAMINGPC,
      [ProductCategory.LAPTOPS]: ProductCategoryBE.LAPTOPS,
      [ProductCategory.MOUSE]: ProductCategoryBE.MOUSE,
      [ProductCategory.KEYBOARD]: ProductCategoryBE.KEYBOARD,
    };

    // Ensure the productcategory is of type ProductCategory
    const frontendCategory = formData.category as ProductCategory;

    // Map to backend category
    const backendCategory = categoryMapping[frontendCategory];

    const newProduct = {
      ...formData,
      category: backendCategory, // Use the mapped backend category
    };

      console.log(newProduct);

    this.productServices.addProduct(newProduct).subscribe(
      (response) => {
        console.log('Product added:', response);
        alert('Product added successfully!');
        this.productForm.reset();
      },
      (error) => {
        console.error('Error adding product:', error);
        alert('Failed to add product.');
      }
    );
  } else {
    alert('Please fill out the form correctly.');
  }
}








// updateProduct(product: ProductDto): void {
//   this.productServices.updatePoduct(product.id, product).subscribe(
//     () => {
//       console.log('Product updated successfully');
//       this.fetchAllProducts();
//     },
//     (error) => console.error('Error updating product:', error)
//   );
// }



// Delete product
deleteProduct(productId: number): void {
  this.productServices.deleteProduct(productId).subscribe(
    () => {
      console.log('Product deleted successfully');
      this.fetchAllProducts(); // Refresh the product list
    },
    (error) => console.error('Error deleting product:', error)
  );
}

deleteOrder(orderId: number): void {
  this.orderServices.deleteOrder(orderId).subscribe(
    () => {
      console.log('Order deleted successfully');
      this.fetchAllOrders();
    },
    (error) => console.error('Error deleting order:', error)
  );
}

deleteUser(userId: number): void {
  this.authServices.deleteUser(userId).subscribe(
    () => {
      console.log('User deleted successfully');
      this.fetchAllUsers();
    },
    (error) => console.error('Error deleting user:', error)
  );
}

updateUser(user: UpdateUserDto): void {
  this.authServices.updateUser(user).subscribe(
    () => {
      console.log('User updated successfully');
      this.fetchAllUsers();
    },
    (error) => console.error('Error updating user:', error)
  );
}


// Switch section
switchSection(section: 'addProduct' | 'manageProducts' | 'manageOrders' | 'manageUsers'): void {
  this.currentSection = section;

  if (section === 'manageProducts') this.fetchAllProducts();
  if (section === 'manageUsers') this.fetchAllUsers();
  if (section === 'manageOrders') this.fetchAllOrders();
}
}
