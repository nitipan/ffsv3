import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'app-tab-item',
    template: `    
    <div [hidden]="!active" class="tab-content">
        <ng-content></ng-content>
    </div>
    `,
    styleUrls: ['./tab.component.scss']
})
export class TabItemComponent implements OnInit {

    @Input()
    title: string;

    @Input()
    active: boolean;

    constructor() { }

    ngOnInit() {
    }

}
