import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app-components/app.component';
import { TransportationComponent } from './app-components/transportation/transportation.component';
import { MainComponent } from './app-components/main-component/main.component';

import {HttpClientModule} from '@angular/common/http';
import { ApolloModule, Apollo } from 'apollo-angular';
import { HttpLinkModule, HttpLink } from 'apollo-angular-link-http';
import { InMemoryCache } from 'apollo-cache-inmemory';

@NgModule({
  declarations: [
    AppComponent,
    TransportationComponent,
    MainComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, 
    HttpClientModule,
    ApolloModule,
    HttpLinkModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 
    constructor(
      apollo: Apollo, 
      httpLink: HttpLink
    ){

      apollo.create({
        link: httpLink.create({uri:"https://localhost:44341/graphql"}),
        cache: new InMemoryCache()
      })
    }
}
