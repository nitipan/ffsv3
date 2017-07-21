import { FormControl, FormGroup } from '@angular/forms';
import { OnInit, Input } from '@angular/core';
export abstract class FFSInputBase implements OnInit {

    @Input() label: string;
    @Input() key: string;
    @Input() value: string;
    @Input() unit: string;
    @Input() form: FormGroup;
    @Input() options: { key: string, value: string }[] = [];

    ngOnInit(): void {
        let group: any = {};
        group[this.key] = new FormControl(this.value || '');
        this.form = new FormGroup(group);
    }
}