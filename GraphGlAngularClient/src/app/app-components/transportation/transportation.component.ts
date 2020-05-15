import { Component, OnInit } from '@angular/core';
import { GraphGlService } from 'src/app/services/graph-gl.service';
import { Apollo } from 'apollo-angular';

import gql from 'graphql-tag';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'
import { TransportationInfo, Query } from 'src/app/types/TransportationInfo';

@Component({
  selector: 'transportation',
  templateUrl: './transportation.component.html',
  styleUrls: ['./transportation.component.scss']
})
export class TransportationComponent implements OnInit {

  transinfos: Observable<TransportationInfo[]>;
  constructor(private apollo: Apollo) { }

  ngOnInit() {
    this.transinfos = this.apollo.watchQuery<Query>({
      query: gql`
        query transinfos{
          transportation{
            cargoDescription
            vehicleType
          }
        }
      `
    }).valueChanges
      .pipe(
        map(result => result.data.transportation)
      );
    // this.graphService.getInfoByQuery().subscribe(
    //   res => console.log(res)
    // );

  }

}
