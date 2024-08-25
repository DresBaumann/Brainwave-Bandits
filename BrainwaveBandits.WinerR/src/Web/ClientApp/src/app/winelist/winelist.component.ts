import { Component, OnDestroy } from '@angular/core';
import { PaginatedListOfWineBriefDto, WinesClient } from '../web-api-client';
import { from, Subscription } from 'rxjs';

@Component({
  selector: 'app-winelist-component',
  templateUrl: './winelist.component.html',
  styleUrls: ['./winelist.component.css']
})
export class WinelistComponent implements OnDestroy {
    public wines: PaginatedListOfWineBriefDto = new PaginatedListOfWineBriefDto(); // Initialize with empty items array
    private subscription: Subscription;

    constructor(private client: WinesClient) {
        this.loadWines();
    }

    // Method to load wines from the API
    loadWines() {
        this.subscription = from(this.client.getWinesWithPagination(1, 100)).subscribe({
            next: result => this.wines = result,
            error: error => console.error(error)
        });
    }

    // Method to handle deleting a wine
    onDeleteWine(wineId: number) {
        this.client.deleteWine(wineId).subscribe({
            next: () => {
                console.log(`Wine with ID ${wineId} deleted successfully!`);
                this.loadWines(); // Reload the inventory after deletion
            },
            error: error => console.error(`Error deleting wine with ID ${wineId}`, error)
        });
    }

    // Method called when wine is added from either the microphone or search component
    onWineAdded() {
        this.loadWines(); // Reload the wine list after a new wine is added
    }

    ngOnDestroy() {
        // Unsubscribe to avoid memory leaks
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }
}
