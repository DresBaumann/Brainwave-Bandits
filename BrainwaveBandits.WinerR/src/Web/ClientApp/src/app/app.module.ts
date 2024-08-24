import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { WinesClient } from './web-api-client';
import { WinelistComponent } from './winelist/winelist.component';
import { CommonModule } from '@angular/common';
import { MicrophoneButtonComponent } from './microphone-button/microphone-button.component';
import { WinesearchComponent } from './winesearch/winesearch.component';
import { SearchPageComponent } from './search-page/search-page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    WinelistComponent,
    MicrophoneButtonComponent,
    WinesearchComponent,
    SearchPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/winelist', pathMatch: 'full' },
      { path: 'winelist', component: WinelistComponent },
      { path: 'search', component: SearchPageComponent }
    ]),
    BrowserAnimationsModule,
    ModalModule.forRoot(),
    CommonModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    WinesClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
