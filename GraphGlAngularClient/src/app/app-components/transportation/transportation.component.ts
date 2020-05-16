import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

import { Apollo } from 'apollo-angular';
import { map } from 'rxjs/operators'
import { Observable } from 'rxjs';
import gql from 'graphql-tag';

import { TransportationInfo, Query, Mutation } from 'src/app/types/TransportationInfo';

@Component({
  selector: 'transportation',
  templateUrl: './transportation.component.html',
  styleUrls: ['./transportation.component.scss']
})
export class TransportationComponent implements OnInit {

  transinfos: TransportationInfo[];

  displayedColumns: string[] = ['action', 'loadingDate', 'vehicleType', 'cargoDescription', 'paymentType', 'routFrom', 'routTo', 'routFromCountry', 'routToCountry'];
  dataSource = new MatTableDataSource<TransportationInfo>();

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  
  constructor(private apollo: Apollo) { }

  ngOnInit() {
    this.apollo.watchQuery<Query>({
      query: gql`
        query transinfos{
          transportation{
            transId
            amountOfLikes
            cargoDescription
            loadingDate
            paymentType
            routFrom
            routFromCountry
            routTo
            routToCountry
            vehicleType
          }
        }
      `
    }).valueChanges
      .pipe(
        map(result => result.data.transportation)
      ).subscribe(items => { this.transinfos = items;this.dataSource.data = items});

    this.dataSource.paginator = this.paginator;
  }

  likeModification(elem: TransportationInfo, add: boolean){
    if(add){
      elem.amountOfLikes++;
    }
    else{
      elem.amountOfLikes--;
    }
    

    var id = elem.transId;
    var amount = elem.amountOfLikes;
    

    this.apollo.mutate<Mutation>({
      mutation: gql`
      mutation likes($id: ID, $amount: Int){
        updateLikes(id: $id, amountOfLikes: $amount)
      }`,
      variables: {id, amount}
    }).subscribe(res => alert(res.data.updateLikes));    
  }

}
