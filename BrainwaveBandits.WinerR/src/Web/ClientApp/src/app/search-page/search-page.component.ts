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
  isLoading: boolean = false;  // To track loading state

  constructor(private winesClient: WinesClient) {}

  // Method to search for wines based on the dish name
  searchDish() {
    if (this.dishName.trim()) {
      this.isLoading = true;  // Start loading
      this.winesClient.recommendWine(this.dishName).subscribe(
        (response) => {
          this.recommendedWines = response;
          this.isLoading = false;  // Stop loading
        },
        (error) => {
          console.error('Error fetching wine recommendations', error);
          this.isLoading = false;  // Stop loading on error
        }
      );
    }
  }
}
