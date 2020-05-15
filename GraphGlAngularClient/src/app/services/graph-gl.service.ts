import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GraphGlService {

  constructor(private _http: HttpClient) 
  { }


  getInfoByQuery(){
    var tt = ''
    return this._http.post("https://localhost:44341/graphql/", "");
  }
}
