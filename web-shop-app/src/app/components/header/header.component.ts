import { Component, computed, HostListener, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import {MatMenuModule} from '@angular/material/menu'
import { UserService } from '../../services/auth.services';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ProductService } from '../../services/product.services';
import { MatInput, MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [ CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    RouterLink,
    RouterLinkActive,
    MatMenuModule,
    FormsModule,
    MatFormFieldModule ,
    MatInputModule,
    FormsModule ,
    MatAutocompleteModule
    
    
    
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  isLoggedIn = computed(() => this.authService.isLoggedIn());
  isAdmin = computed(() => this.authService.isAdmin());
  searchQuery: string = '';
  isMenuOpen = false;
  isSmallScreen = false;
  userName: string | null = null;
  

  constructor(
    private authService: UserService,
    private productServices:ProductService,
    private router:Router
  ) {
    console.log('Constructor called');
  }
  ngOnInit(): void {
    this.checkScreenSize();
    this.userName = this.authService.getUserName();
  }
  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.checkScreenSize();
  }
  onLogout(): void {
    this.authService.logout();
  }
  checkScreenSize() {
    this.isSmallScreen = window.innerWidth < 768; 
    this.isMenuOpen = false; 
  }

 
  ngOnDestroy(): void {
    console.log('NgOnDestroy called');
  }
}
