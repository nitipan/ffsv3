import { EventService } from './../../../event.service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {

  result: any;


  constructor(private eventService: EventService) {

  }

  ngOnInit() {
    this.eventService.calculatedSubject.subscribe(r => {
      this.result = r;
      console.log(this.result);

    });
  }

}
