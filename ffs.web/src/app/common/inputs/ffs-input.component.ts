import { FormGroup, FormControl } from '@angular/forms';
import { Input, Component, OnInit, forwardRef, ViewChild, AfterViewInit } from '@angular/core';
import { FFSInputBase } from "./ffs-input-base";
import * as $ from 'jquery';
import 'bootstrap-datepicker';

@Component(
    {
        selector: 'ffs-text',
        styleUrls: ['./ffs-input.component.scss'],
        template: `
        <div class="ffs-input form-horizontal" [formGroup]="form">
            <div class="form-group" [class.has-error]="!isValid && enabled">
                <label class="control-label col-xs-5" [attr.for]="key">{{label}}</label>
                <div class="input-group col-xs-7">
                    <input [formControlName]="key" [id]="key" class="form-control">                
                    <label class="input-group-addon control-label" *ngIf="unit != undefined">{{unit}}</label>                  
                </div>
                <div class="col-xs-offset-5 col-xs-7 control-label errorText" *ngIf="!isValid && required">* {{label}} is required</div>
            </div>
           
        </div>
        `,
        providers: [{ provide: FFSInputBase, useExisting: forwardRef(() => FFSTextComponent) }]
    }
)
export class FFSTextComponent extends FFSInputBase {

}

@Component(
    {
        selector: 'ffs-select',
        styleUrls: ['./ffs-input.component.scss'],
        template: `
        <div class="ffs-input form-horizontal" [formGroup]="form">
            <div class="form-group">
                <label class="control-label col-xs-5" [attr.for]="key">{{label}}</label>
                <div class="input-group col-xs-7">
                    <input type="hidden" *ngIf="text != undefined" [formControlName]="text" />
                     <select  [id]="key" [formControlName]="key" class="form-control">
                         <option  *ngFor="let opt of options" [value]="opt.key">{{opt.value}}</option>
                    </select>         
                    <label class="input-group-addon control-label" *ngIf="unit != undefined">{{unit}}</label>
                </div>
            </div>
        </div>
        `,
        providers: [{ provide: FFSInputBase, useExisting: forwardRef(() => FFSSelectComponent) }]
    }
)
export class FFSSelectComponent extends FFSInputBase implements OnInit {


    @Input() text: string;

    @Input() options: { key: string, value: string }[] = [];

    ngOnInit(): void {
        super.ngOnInit();
        if (this.text != undefined) {
            this.form.addControl(this.text, new FormControl(''));
            this.form.get(this.key).valueChanges.subscribe(v => {
                var item = this.options.find(o => o.key == v);
                this.form.get(this.text).setValue(item.value);
            });
        }
    }
}

@Component(
    {
        selector: 'ffs-check',
        styleUrls: ['./ffs-input.component.scss'],
        template: `
        <div class="ffs-input form-horizontal" [formGroup]="form">
           <div class="checkbox">
                <label>
                    <input type="checkbox" [id]="key" [formControlName]="key"> {{label}}
                </label>
            </div>
        </div>
        `,
        providers: [{ provide: FFSInputBase, useExisting: forwardRef(() => FFSCheckComponent) }]
    }
)
export class FFSCheckComponent extends FFSInputBase {

}


@Component(
    {
        selector: 'ffs-number',
        styleUrls: ['./ffs-input.component.scss'],
        template: `
        <div class="ffs-input form-horizontal" [formGroup]="form">
            <div class="form-group" [class.has-error]="!isValid && enabled">
                <label class="control-label col-xs-5" [attr.for]="key">{{label}}</label>
                <div class="input-group col-xs-5">
                    <input [formControlName]="key" type="number" [id]="key" class="form-control">                
                    <label class="input-group-addon control-label" *ngIf="unit != undefined">{{unit}}</label>
                    
                </div>
                
                <div class="col-xs-offset-5 col-xs-7 control-label errorText" *ngIf="!isValid && required">* {{label}} is required</div>
                
            </div>
           
        </div>
        `,
        providers: [{ provide: FFSInputBase, useExisting: forwardRef(() => FFSNumberComponent) }]
    }
)
export class FFSNumberComponent extends FFSInputBase {
    @Input() min: number;
    @Input() max: number;
}


@Component(
    {
        selector: 'ffs-date',
        styleUrls: ['./ffs-input.component.scss'],
        template: `
       <div class="ffs-input form-horizontal" [formGroup]="form">
            <div class="form-group" [class.has-error]="!isValid">
                <label class="control-label col-xs-5" [attr.for]="key">{{label}}</label>
                <div class="input-group date col-xs-7" #datepicker>
                <input [formControlName]="key" [id]="key" class="form-control">
                <div class="input-group-addon">
                    <span class="glyphicon glyphicon-calendar"></span>
                </div>
                </div>
                <div class="col-xs-offset-5 col-xs-7 control-label errorText" *ngIf="!isValid && required">* {{label}} is required</div>
            </div>
        </div>
        `,
        providers: [{ provide: FFSInputBase, useExisting: forwardRef(() => FFSDateComponent) }]
    }
)
export class FFSDateComponent extends FFSInputBase implements AfterViewInit {

    @ViewChild("datepicker") datepicker;

    constructor() {
        super();
    }

    ngAfterViewInit(): void {
        var input = this.form.get(this.key);

        ($(this.datepicker.nativeElement) as any)
            .datepicker({
                autoclose: true,
                todayHighlight: true,
                format: 'mm/dd/yyyy'
            })
            .on("changeDate", function (e) {
                input.setValue(e.date);
            });
    }
}


@Component(
    {
        selector: 'ffs-browse',
        styleUrls: ['./ffs-input.component.scss'],
        template: `
     <div class="ffs-input form-horizontal" [formGroup]="form">
            <div class="form-group">
                <label class="control-label col-xs-5" [attr.for]="key">{{label}}</label>
                <input type="file" [attr.accept]="accept" [attr.for]="key" size="chars" #file (change)="fileBrowserChanged($event)">
                <button class="btn btn-primary col-xs-7" (click)="browseFile()">Browse</button>
            </div>
            <div class=" col-xs-7 col-xs-offset-5" *ngIf="filename != ''">
                <div class="chip">
                {{filename}}
                <span class="closebtn" (click)="removeFile()">&times;</span>
                </div>
            </div>
            </div>
        `,
        providers: [{ provide: FFSInputBase, useExisting: forwardRef(() => FFSBrowseComponent) }]
    }
)
export class FFSBrowseComponent extends FFSInputBase implements AfterViewInit {

    @Input() accept = "*.*";
    @Input() type;

    @ViewChild("file") fileBrowserInput;
    filename: string = '';

    browseFile() {
        this.fileBrowserInput.nativeElement.click();
    }

    fileBrowserChanged(event: any) {
        var inputForm = this.form.get(this.key);
        let input = event.target;
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                inputForm.setValue((e.target as any).result);
            }

            this.filename = input.files[0].name;

            if (this.type == 'text')
                reader.readAsText(input.files[0]);
            else
                reader.readAsDataURL(input.files[0]);
        }
    }

    removeFile() {
        this.filename = '';
        var inputForm = this.form.get(this.key);
        inputForm.setValue('');
        $(this.fileBrowserInput.nativeElement).val('');
    }

    ngAfterViewInit(): void {

    }

}