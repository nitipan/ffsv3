import { EventService } from './../../event.service';
import { Component, OnInit, EventEmitter, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {

  errorMessage = "";
  logging: boolean = false;

  username: string;

  focusEmittier = new EventEmitter<boolean>();

  constructor(private eventService: EventService) {


  }

  ngOnInit() {
    this.eventService.requestLogin.subscribe(() => {
      this.focusEmittier.emit(true);
    });
  }

  login() {
    this.logging = true;
    var comp = this;
    this.errorMessage = "";

    setTimeout(function () {

      if (comp.username != 'admin') {
        comp.errorMessage = "Username or Password invalid";
        comp.eventService.requestLogin.next(null);
        comp.logging = false;
      } else {
        comp.eventService.afterLogin.next({ username: comp.username });
        comp.logging = false;
      }

    }, 1000);
  }

  close() {
    window.close();
  }


}
