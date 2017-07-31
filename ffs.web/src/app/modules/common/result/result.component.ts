import { EventService } from './../../../event.service';
import { Component, OnInit, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { InputBaseComponent } from "../input-base.component";

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent extends InputBaseComponent implements OnInit, AfterViewInit {


  result: any;

  level2Grid: any[];



  ngOnInit() {
    this.moduleEvent.calculatedSubject.subscribe(r => {
      this.result = r;

      if (this.result.param.assessmentLevel == 2) {
        this.level2Grid = [];
        for (let i = 0; i < this.result.result.resultDataGrid.length; i += 4) {
          this.level2Grid.push({
            col1: this.result.result.resultDataGrid[i],
            col2: this.result.result.resultDataGrid[i + 1],
            col3: this.result.result.resultDataGrid[i + 2],
            col4: this.result.result.resultDataGrid[i + 3],
            result: (this.result.result.resultDataGrid[i + 3].toLowerCase().indexOf("not") == -1)
          });
        }
        console.log(this.level2Grid);
      }

    });
  }

  ngAfterViewInit(): void {


  }
}
