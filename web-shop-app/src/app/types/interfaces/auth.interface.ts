import { UserRole } from "../enums/user-role-enum";
import { Order } from "./order.interface";

// user.dto.ts
export interface UserDto {
    name: string;
    lastName: string;
    address: string;
    userName: string;
    email: string;
    role: UserRole;
    phone: string;
    orders: Order[]; // Assuming you have an Order model
  }
  
  export interface UpdateUserDto {
    id: number;
    name: string;
    lastName: string;
    address: string;
    email: string;
    role: UserRole;
    phone: string;
    password: string;
    confirmPassword: string;
  }
  
  export interface RegisterUserDto {
    name: string;
    lastName: string;
    address: string;
    userName: string;
    email: string;
    role: UserRole;
    phone: string;
    password: string;
    confirmPassword: string;
  }
  
  export interface LoginUserDto {
    userName: string;
    password: string;
  }

  export interface LoginResponse{

    isSuccessful: boolean;
        token: string
        validTo: string
    

}