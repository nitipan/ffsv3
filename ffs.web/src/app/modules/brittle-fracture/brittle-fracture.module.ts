import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CalculationCommonModule } from '../calculation-common.module';
import { AppCommonModule } from '../../common/common.module';
import { BrittleFractureComponent } from './brittle-fracture.component';


@NgModule({
    imports: [
        CalculationCommonModule,
        RouterModule.forChild([
            { path: '', component: BrittleFractureComponent }
        ])],
    exports: [],
    declarations: [BrittleFractureComponent],
    providers: [],
})
export class BrittleFractureModule { }
