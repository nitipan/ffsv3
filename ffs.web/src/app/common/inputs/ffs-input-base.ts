import { FFSSelectComponent } from './ffs-input.component';
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

    get enabled() {
        return this.form.controls[this.key].enabled;
    }

    static toFormGroup(inputs: QueryList<FFSInputBase>) {
        var formItems: any = {};
        inputs.forEach((i) => {
            formItems[i.key] = i.form.controls[i.key];

            if (i instanceof FFSSelectComponent) {
                if ((i as FFSSelectComponent).text != undefined)
                    formItems[i.text] = i.form.controls[i.text];
            }
        });

        return new FormGroup(formItems);
    }
}