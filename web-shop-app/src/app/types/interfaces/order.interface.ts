export interface Order {
    id: number;
    orderDate: string;
    address:string;
    userId: number;
    user: User;
    orderItems: OrderItem[];
    totalAmount: number;
  }
  
  export interface OrderResponse {
    success: boolean;
    orderId?: number;
    errors?: string[];
  }
  
  export interface OrderItem {
    id: number;
    productId: number;
    product: Product;
    quantity: number;
  }
  
  export interface User {
    id: number;
    username: string;
    fullName: string;
    email: string;
  }
  
  export interface Product {
    id: number;
    productName: string;
    productDescription: string;
    price: number;
    brand: string;
    quantityAvailable: number;
  }
  