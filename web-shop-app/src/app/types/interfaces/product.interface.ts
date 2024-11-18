import { ProductCategory, ProductCategoryBE } from "../enums/product-category-enum"

export interface Product{
    id:number,
    productname:string
    productdescription:string
    price:number
    productcategory:ProductCategory
    brand:string
    quantityavailable:number
    shippinhcost:number
    shippingtime:number
    originalimagepath:string
    savedimagepath:string
    discount?:number
}

export interface ProductDto{
    id:number,
    productName:string
    productDescription:string
    price:number
    brand:string
    quantityAvailable:number
    shippingTime:number
    originalImagePath:string
    discount:number
    shippingCost:number
}

export interface AddProductDto{
    productName:string
    productDescription:string
    price:number
    category:ProductCategoryBE
    brand:string
    quantityAvailable:number
    shippingCost:number
    shippingTime:number
    originalImagePath:string
    discount?:number
}