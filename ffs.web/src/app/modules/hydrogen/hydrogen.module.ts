import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CalculationCommonModule } from '../calculation-common.module';
import { AppCommonModule } from '../../common/common.module';
import { HydrogenComponent } from './hydrogen.component';


@NgModule({
    imports: [
        CalculationCommonModule,
        RouterModule.forChild([
            { path: '', component: HydrogenComponent }
        ])],
    exports: [],
    declarations: [HydrogenComponent],
    providers: [],
})
export class HydrogenModule { }
