import { KV } from './../../../model/kv';
import { Component, OnInit } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/Rx';
import { Observable } from "rxjs/Rx";

@Component({
  selector: 'app-equipment-input',
  templateUrl: './equipment-input.component.html',
  styleUrls: ['./equipment-input.component.scss']
})
export class EquipmentInputComponent implements OnInit {

  equipmentTypes: Observable<KV[]>;
  methodologies: Observable<KV[]>;
  units: Observable<KV[]>
  assessmentLevel: Observable<KV[]>;

  constructor(private http: Http) { }

  ngOnInit() {
    this.equipmentTypes = this.http.get("/api/lookup/equipmenttypes")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.equipmentTypeID, value: a.equipmentTypeName }; }));

    this.methodologies = this.http.get("/api/lookup/methodologies")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.methodologyID, value: a.methodologyName }; }));

    this.units = this.http.get("/api/lookup/units")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.unitID, value: a.unitName }; }));

    this.assessmentLevel = this.http.get("/api/lookup/assessmentLevel")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.assessmentLevelID, value: a.assessmentLevelName }; }));
  }

}
