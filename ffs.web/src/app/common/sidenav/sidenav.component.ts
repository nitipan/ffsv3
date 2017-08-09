import { EventService } from './../../event.service';
import { Component, OnInit, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {

  errorMessage = "";

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
    var comp = this;
    this.errorMessage = "";

    setTimeout(function () {

      if (comp.username != 'admin') {
        comp.errorMessage = "Username or Password invalid";
        comp.eventService.requestLogin.next(null);
      } else {
        comp.eventService.afterLogin.next({ username: comp.username });
      }

    }, 1000);
  }

  close() {
    window.close();
  }


}
