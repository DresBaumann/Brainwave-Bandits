import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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

import { MicrophoneButtonComponent } from './microphone-button/microphone-button.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    MicrophoneButtonComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/winelist', pathMatch: 'full' },
      { path: 'winelist', component: WinelistComponent }
    ]),
    BrowserAnimationsModule,
    ModalModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    WinesClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
