import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OnInit, Input, QueryList } from '@angular/core';
export abstract class FFSInputBase implements OnInit {

    @Input() label: string;
    @Input() key: string;
    @Input() value: string;
    @Input() unit: string;
    @Input() form: FormGroup;
    @Input() required = false;

    ngOnInit(): void {
        let group: any = {};
        group[this.key] = this.required ? new FormControl(this.value || '', Validators.required) : new FormControl(this.value || '');
        this.form = new FormGroup(group);
    }

    get isValid() {
        return this.form.controls[this.key].valid;
    }

    static toFormGroup(inputs: QueryList<FFSInputBase>) {
        var formItems: any = {};
        inputs.forEach((i) => {
            formItems[i.key] = i.form.controls[i.key];
        });

        return new FormGroup(formItems);
    }
}