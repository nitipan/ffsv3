import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OnInit, Input, QueryList } from '@angular/core';


import * as _ from 'underscore';

export abstract class FFSInputBase implements OnInit {


    @Input() label: string;
    @Input() key: string;
    @Input() value: string;
    @Input() unit: string;
    @Input() form: FormGroup;
    @Input() required = false;
    @Input() validators: any[] = [];

    errorMessages: string[];

    ngOnInit(): void {
        let group: any = {};

        let validators: any[] = [];
        if (this.required)
            validators.push(Validators.required);

        this.validators.forEach(e => {
            validators.push(e);
        });

        group[this.key] = new FormControl(this.value || '', Validators.compose(validators));
        this.form = new FormGroup(group);

        this.form.controls[this.key].valueChanges.subscribe(v => {



            // check error
            this.errorMessages = [];
            var errors = this.form.get(this.key).errors;
            if (errors != null) {
                if (errors['required'] == true) {
                    this.errorMessages.push(`${this.label} is required`);
                    errors['required'] = undefined;
                }
                var vals = _.chain(errors)
                    .values()
                    .filter(v => v != undefined)
                    .map(v => `${this.label} ${v}`)
                    .value();

                this.errorMessages.push(...vals);
            }
        });

    }

    get hasError() {
        return this.form.get(this.key).invalid && (this.form.get(this.key).dirty || this.form.get(this.key).touched);
    }

    get enabled() {
        return this.form.controls[this.key].enabled;
    }

    static toFormGroup(inputs: QueryList<FFSInputBase>) {
        var formItems: any = {};
        inputs.forEach((i) => {
            formItems[i.key] = i.form.controls[i.key];


            if ((i as any).text != undefined)
                formItems[(i as any).text] = i.form.controls[(i as any).text];

        });

        return new FormGroup(formItems);
    }
}