import { Component, computed } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card'
import { RouterLink } from '@angular/router';
import { UserService } from '../../services/auth.services';
import { CarouselComponent } from '../carousel06-component/carousel06-component.component';
import { ProductCardComponent } from '../product-card/product-card.component';
import { ProductCardsComponent } from '../product-cards/product-cards.component';
import { ProductCardsCat2Component } from '../product-cards-cat2/product-cards-cat2.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    CarouselComponent,
    ProductCardsComponent,
    ProductCardsCat2Component,
    MatCardModule,
    MatButtonModule,
    RouterLink
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  isLoggedIn = computed(() => this.authService.isLoggedIn());

  constructor(
    private authService: UserService,
  ) {}
}