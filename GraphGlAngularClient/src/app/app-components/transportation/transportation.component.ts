import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

import { Apollo } from 'apollo-angular';
import { map } from 'rxjs/operators'
import { Observable } from 'rxjs';
import gql from 'graphql-tag';

import { TransportationInfo, Query, Mutation, TransportationML } from 'src/app/types/TransportationInfo';

@Component({
  selector: 'transportation',
  templateUrl: './transportation.component.html',
  styleUrls: ['./transportation.component.scss']
})
export class TransportationComponent implements OnInit {

  transinfos: TransportationML[];

  loaded: boolean = false;
  displayedColumns: string[] = ['action', 'loadingDate', 'vehicleType', 'cargoDescription', 'paymentType', 'routFrom', 'routTo', 'routFromCountry', 'routToCountry', '%'];
  dataSource = new MatTableDataSource<TransportationML>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;


  constructor(private apollo: Apollo) { }

  ngOnInit() {
    this.apollo.watchQuery<Query>({
      query: gql`
        query TransInformation{
          transportation {
            likeProbability
            percentage
            transportationInfo {
              amountOfLikes
              cargoDescription
              loadingDate
              paymentType
              routFrom
              routFromCountry
              routTo
              routToCountry
              transId
              vehicleType
            }
          } 
        }
        
      `
    }).valueChanges
      .pipe(
        map(result => result.data.transportation.sort((x,y) => (y.likeProbability - x.likeProbability)))
      ).subscribe(items => { this.loaded = true; this.transinfos = items; this.dataSource.data = items });

    this.dataSource.paginator = this.paginator;
  }

  likeModification(elem: TransportationML, add: boolean) {
    if (add) {
      elem.transportationInfo.amountOfLikes++;
    }
    else {
      elem.transportationInfo.amountOfLikes--;
    }


    var id = elem.transportationInfo.transId;
    var amount = elem.transportationInfo.amountOfLikes;


    this.apollo.mutate<Mutation>({
      mutation: gql`
      mutation likes($id: ID, $amount: Int){
        updateLikes(id: $id, amountOfLikes: $amount)
      }`,
      variables: { id, amount }
    }).subscribe();
  }

  getColor(value) {
    var hue = ((value / 100) * 120).toString(10);
    return ["hsl(", hue, ",100%,50%)"].join("");
  }
}
