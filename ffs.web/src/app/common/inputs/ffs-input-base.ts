import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OnInit, Input } from '@angular/core';
export abstract class FFSInputBase implements OnInit {

    @Input() label: string;
    @Input() key: string;
    @Input() value: string;
    @Input() unit: string;
    @Input() form: FormGroup;
    @Input() options: { key: string, value: string }[] = [];
    @Input() required = false;

    ngOnInit(): void {
        let group: any = {};
        group[this.key] = this.required ? new FormControl(this.value || '', Validators.required) : new FormControl(this.value || '');
        this.form = new FormGroup(group);
    }

    get isValid() {
        return this.form.controls[this.key].valid;
    }
}