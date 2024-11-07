import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products-cat',
  standalone: true,
  imports: [CommonModule,
    MatGridListModule],
  templateUrl: './products-cat.component.html',
  styleUrl: './products-cat.component.scss'
})
export class ProductsCatComponent {
  categories = [
    { name: '2', displayName: 'Desktop Pc', imgSrc: 'https://cdn-dynmedia-1.microsoft.com/is/image/microsoftcorp/MSFT-All-in-One_1040x585?scl=1' },
   
    { name: '5', displayName: 'Gaming Pc', imgSrc: 'https://i.ebayimg.com/images/g/wO4AAOSwaJ1kgN2S/s-l1200.jpg' },
    { name: '6', displayName: 'Laptops', imgSrc: 'https://cdn.thewirecutter.com/wp-content/media/2024/07/laptopstopicpage-2048px-3685-2x1-1.jpg?width=2048&quality=75&crop=2:1&auto=webp' },
    { name: '7', displayName: 'Mouse', imgSrc: 'https://cdn.mos.cms.futurecdn.net/ygPpbsDRSZRfJDNdpaXsE3-1200-80.jpg' },
    { name: '8', displayName: 'Keyboard', imgSrc: 'https://vgnlab.com/cdn/shop/articles/7.21green_keyboard.jpg?v=1695377731&width=1500' },
    { name: '4', displayName: 'Range', imgSrc: 'https://static.standard.co.uk/2021/06/18/09/Smeg-Symphonyjpg?crop=8:5,smart&quality=75&auto=webp&width=1000' },
    { name: '1', displayName: 'Hobs', imgSrc: 'https://www.lukata.co.uk/cdn/shop/articles/Elica-NT-ONE-DO-83cm-Venting-Induction-Hob-Duct-Out-Version-In-Use.jpg?v=1713964718&width=1100' },
   { name: '3', displayName: 'Oven', imgSrc: 'https://www.beko.com/content/dam/bekoglobal/za/images/beko-oven.png' },



];


  constructor(private router: Router) {}

  navigateToCategory(categoryName: string): void {
    const categoryNumber = Number(categoryName); // Convert to number before navigating
    if (!isNaN(categoryNumber)) {
      this.router.navigate(['/productlist', categoryNumber]);
    } else {
      console.error('Invalid category:', categoryName);
    }
  }
}