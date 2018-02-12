import { Component, OnInit } from '@angular/core';
import { EventService } from '../event.service';
import { ModuleBase } from './module-base.component';
import { ViewChild } from '@angular/core';
import { AfterViewInit } from '@angular/core';
import { MetalLossInputComponent } from './common/metal-loss-input/metal-loss-input.component';
import { ChangeDetectorRef } from '@angular/core';

@Component({
    selector: 'app-module-test',
    templateUrl: 'test.component.html'
})

export class TestComponent extends ModuleBase implements OnInit, AfterViewInit {



    @ViewChild(MetalLossInputComponent) metalInput: MetalLossInputComponent;

    name: string;
    initDesignInput() {

    }
    initMaterialInput() {

    }
    initFlawInput() {
    }
    initLoadInput() {
    }
    constructor(
        private df: ChangeDetectorRef,
        eventService: EventService,
    ) {
        super(eventService);

    }

    ngOnInit() {

    }

    ngAfterViewInit(): void {

        this.metalInput.form.patchValue({ thicknessDataID: 1 });


        this.df.detectChanges();
        // this.metalInput.form.valueChanges.subscribe(v => {
        //     console.log(v);
        // });
    }
}
