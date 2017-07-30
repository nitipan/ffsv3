import { TabItemComponent } from './tab-item.component';
import { Component, OnInit, ContentChildren, QueryList, AfterContentInit } from '@angular/core';

@Component({
  selector: 'app-tab',
  templateUrl: './tab.component.html',
  styleUrls: ['./tab.component.scss']
})
export class TabComponent implements OnInit, AfterContentInit {


  @ContentChildren(TabItemComponent) tabs: QueryList<TabItemComponent>;

  tab: TabItemComponent;

  constructor() { }

  ngOnInit() {
  }

  ngAfterContentInit(): void {
    let activetab = this.tabs.filter((t) => t.active);
    if (activetab.length == 0)
      this.selectTab(this.tabs.first);
    else
      this.selectTab(activetab[0]);
  }

  selectTab(tab: TabItemComponent) {
    this.tabs.forEach((t) => t.active = false);

    this.tab = tab;
    this.tab.active = true;
  }

}
