import { Component, Input } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-tile',
  standalone: true,
  imports: [MatGridListModule],
  templateUrl: './category-tile.component.html',
  styleUrl: './category-tile.component.scss'
})
export class CategoryTileComponent {

  @Input() category!: { name: number; displayName: string; imgSrc: string };

  constructor(private router: Router) {}

  navigateToCategory(): void {
    this.router.navigate(['/category', this.category.name]);
  }
}
