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
            <div class="form-group" [class.has-error]="!isValid">
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
                     <select [id]="key" [formControlName]="key" class="form-control">
                         <option *ngFor="let opt of options" [value]="opt.key">{{opt.value}}</option>
                    </select>         
                    <label class="input-group-addon control-label" *ngIf="unit != undefined">{{unit}}</label>
                </div>
            </div>
        </div>
        `,
        providers: [{ provide: FFSInputBase, useExisting: forwardRef(() => FFSSelectComponent) }]
    }
)
export class FFSSelectComponent extends FFSInputBase {
    @Input() options: { key: string, value: string }[] = [];
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
            <div class="form-group" [class.has-error]="!isValid">
                <label class="control-label col-xs-5" [attr.for]="key">{{label}}</label>
                <div class="input-group col-xs-7">
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