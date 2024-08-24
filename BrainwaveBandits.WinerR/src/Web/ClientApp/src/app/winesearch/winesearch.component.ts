import { Component, OnDestroy, OnInit } from '@angular/core';
import { CreateOrUpdateWinesByIdListCommand, ImportedWineClient, ImportedWineSearchResultDto, WinesClient } from '../web-api-client';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { debounceTime, filter, switchMap, finalize } from 'rxjs/operators'; // Import finalize operator
import { Router } from '@angular/router';

@Component({
  selector: 'app-winesearch-component',
  templateUrl: './winesearch.component.html',
  styleUrls: ['./winesearch.component.css']
})
export class WinesearchComponent implements OnInit, OnDestroy {
    public wines: ImportedWineSearchResultDto[] = [];
    public loading = false; // Loading state
    private subscription: Subscription = new Subscription();
    public searchControl: FormControl = new FormControl('');

    constructor(
        private importedWineclient: ImportedWineClient, 
        private winesClient: WinesClient,
        private router: Router
    ) {}

    ngOnInit() {
        this.subscription.add(
            this.searchControl.valueChanges.pipe(
                debounceTime(300),
                filter(value => value && value.length >= 3),
                switchMap(searchQuery => {
                    this.loading = true; // Start loading
                    return this.importedWineclient.search(searchQuery).pipe(
                        finalize(() => this.loading = false) // Stop loading after search completes
                    );
                })
            ).subscribe({
                next: result => this.wines = result,
                error: error => {
                    console.error(error);
                    this.loading = false; // Stop loading on error
                }
            })
        );
    }

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }

    addWine(wineId: string) {
        this.loading = true; // Start loading
        const command = new CreateOrUpdateWinesByIdListCommand();
        command.wineIdList = [wineId];

        this.winesClient.createOrUpdateWineByWinesIdList(command).pipe(
            finalize(() => this.loading = false) // Stop loading after operation completes
        ).subscribe({
            next: response => {
                console.log(`Wine with ID ${wineId} has been successfully added/updated.`);
                this.router.navigate(['/winelist']);
            },
            error: error => {
                console.error(`Failed to add/update wine with ID ${wineId}:`, error);
            }
        });
    }
}
