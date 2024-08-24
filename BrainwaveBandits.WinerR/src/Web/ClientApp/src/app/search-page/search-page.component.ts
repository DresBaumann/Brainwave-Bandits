import { Component } from '@angular/core';
import { WinesClient, WineBriefDto } from '../web-api-client';  // Import the client

@Component({
  selector: 'app-search-page',
  templateUrl: './search-page.component.html',
  styleUrls: ['./search-page.component.css']
})
export class SearchPageComponent {
  dishName: string = '';  // Dish name entered by the user
  recommendedWines: WineBriefDto[] = [];  // Recommended wines

  constructor(private winesClient: WinesClient) {}

  // Method to search for wines based on the dish name
  searchDish() {
    if (this.dishName.trim()) {
      this.winesClient.recommendWine(this.dishName).subscribe(
        (response) => {
          this.recommendedWines = response;
        },
        (error) => {
          console.error('Error fetching wine recommendations', error);
        }
      );
    }
  }
}
