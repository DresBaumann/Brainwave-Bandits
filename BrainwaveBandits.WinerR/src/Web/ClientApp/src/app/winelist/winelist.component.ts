import { Component } from '@angular/core';
import { PaginatedListOfWineBriefDto, WineBriefDto, WinesClient } from '../web-api-client';
import { from } from 'rxjs';

@Component({
  selector: 'app-winelist-component',
  templateUrl: './winelist.component.html'
})
export class WinelistComponent {
    public wines: PaginatedListOfWineBriefDto;

    constructor(private client: WinesClient) {
        from(client.getWinesWithPagination(0, 100)).subscribe({
            next: result => this.wines = result,
            error: error => console.error(error)
        });
    }

    onDelete(wine: WineBriefDto) {
        if (confirm('Willst du diesen Wein wirklich löschen?')) {
            this.client.deleteWine(wine.id)
                .then(() => {
                    this.wines.items = this.wines.items.filter(x => x !== wine);
                })
                .catch(error => console.error(error));
        }
    }
}
