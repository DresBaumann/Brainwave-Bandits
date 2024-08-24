import { Component, OnDestroy } from '@angular/core';
import { PaginatedListOfWineBriefDto, WinesClient } from '../web-api-client';
import { from, Subscription } from 'rxjs';

@Component({
  selector: 'app-winelist-component',
  templateUrl: './winelist.component.html'
})
export class WinelistComponent implements OnDestroy {
    public wines: PaginatedListOfWineBriefDto;
    private subscription: Subscription;

    constructor(private client: WinesClient) {
        this.subscription = from(client.getWinesWithPagination(1, 100)).subscribe({
            next: result => this.wines = result,
            error: error => console.error(error)
        });
    }

    ngOnDestroy() {
        // Unsubscribe to avoid memory leaks
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }
}
