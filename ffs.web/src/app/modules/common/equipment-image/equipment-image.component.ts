import { InputBase } from './../../../model/inputbase';
import { InputBaseComponent } from './../input-base.component';
import { Component, OnInit, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-equipment-image',
  templateUrl: './equipment-image.component.html',
  styleUrls: ['./equipment-image.component.scss']
})
export class EquipmentImageComponent extends InputBaseComponent implements OnInit, AfterViewInit {

  previewEquipmentImage: any;
  currentValue: any;

  constructor() { super() }

  ngOnInit() {

  }

  ngAfterViewInit(): void {
    this.moduleEvent.equipmentInputSubject.subscribe((e: InputBase) => {
      this.previewEquipmentImage = e.equipmentImage;
      this.currentValue = e;
    });
  }

}
