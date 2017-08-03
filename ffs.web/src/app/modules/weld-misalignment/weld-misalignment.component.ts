import { FormGroup } from '@angular/forms';
import { IUnit } from '../../common/unit';
import { Observable } from 'rxjs/Rx';
import { EventService } from '../../event.service';
import { Http } from '@angular/http';
import { KV } from './../../model/kv';
import { ModuleBase } from '../module-base.component';
import { routerTransition } from '../../common/router.animations';
import { animation } from '@angular/animations';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChildren, ViewChild, QueryList } from '@angular/core';
import { FFSInputBase } from "../../common/inputs/ffs-input-base";
import { DesignInputComponent } from './../common/design-input/design-input.component';
import { EquipmentInputComponent } from './../common/equipment-input/equipment-input.component';
import { LoadInputComponent } from './../common/load-input/load-input.component';
import { MaterialInputComponent } from './../common/material-input/material-input.component';

@Component({
  selector: 'app-weld-misalignment',
  templateUrl: './weld-misalignment.component.html',
  styleUrls: ['./weld-misalignment.component.scss'],
  animations: [routerTransition()],
  host: { '[@routerTransition]': '' }
})

export class WeldMisalignmentComponent extends ModuleBase implements OnInit, AfterViewInit {
  name = "Weld Misalignment";

  form: FormGroup;
  unit: Observable<IUnit>;
  FabricationTolerance: Observable<KV[]>;
  WeldOrientarion: Observable<KV[]>;
  outOfPattern: boolean = true;

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;
  @ViewChild(MaterialInputComponent) materialInput: MaterialInputComponent;
  @ViewChild(LoadInputComponent) loadInput: LoadInputComponent;

  constructor(private http: Http, private cdRef: ChangeDetectorRef, eventService: EventService) {
    super(eventService);
    this.unit = this.moduleEvent.unit.asObservable();
  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);

    this.moduleEvent.equipmentTypeSubject.subscribe(equipmentType => {
      this.FabricationTolerance = this.http.get(`/api/lookup/fabricationTolerance/${equipmentType}`)
        .map(response => response.json() as any[])
        .map(arr => arr.map(a => { return { key: a.fabricationToleranceID, value: a.fabricationToleranceName }; }));

      this.FabricationTolerance.subscribe((m: KV[]) => {
        this.form.get('FabricationTolerance').setValue(m[0].key);
      });
    })

    this.form.get('FabricationTolerance').valueChanges.subscribe(v => {
      let farbricId = parseInt(v);
      if (farbricId === 2 || farbricId === 3 || farbricId === 5 || farbricId === 8) {
        this.outOfPattern = false;
      } else {
        this.outOfPattern = true;
      }
    });

    this.cdRef.detectChanges();
    this.moduleEvent.equipmentTypeSubject.emit(1);
  }

  ngOnInit() {
    this.WeldOrientarion = this.http.get('/api/lookup/weldorientarion/')
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.weldOrientarionID, value: a.weldOrientarionName }; }));

    // this.WeldOrientarion.subscribe((m: KV[]) => {
    //   this.form.get('WeldOrientarion').setValue(m[0].key);
    // });
  }

  initDesignInput() {

  }

  initMaterialInput() {

  }

  initFlawInput() {

  }

  initLoadInput() {

  }


}
