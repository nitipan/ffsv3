import { Component, OnInit } from '@angular/core';
import { routerTransition } from "../../common/router.animations";

@Component({
  selector: 'app-general-metal-loss',
  templateUrl: './general-metal-loss.component.html',
  styleUrls: ['./general-metal-loss.component.scss'],
  animations: [routerTransition()],
  host: { '[@routerTransition]': '' }
})
export class GeneralMetalLossComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
