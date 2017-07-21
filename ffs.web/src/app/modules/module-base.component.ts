import { QueryList } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { FFSInputBase } from "../common/inputs/ffs-input-base";

export class ModuleBase {
    toFormGroup(inputs: QueryList<FFSInputBase>) {
        var formItems: any = {};
        inputs.forEach((i) => {
            formItems[i.key] = i.form.controls[i.key];
        });

        return new FormGroup(formItems);
    }
}