import { User } from "./order.interface";
import { Product } from "./product.interface";

export interface Cart {
    id: number;
    userId: number;
    cartItems: CartItem[];
    totalAmount: number;
  }
  
  export interface CartItem {
    id: number;
    productId: number;
    product: Product;
    quantity: number;
  }
  