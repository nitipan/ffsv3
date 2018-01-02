import { NgModule } from '@angular/core';
import { FireDamageComponent } from './fire-damage.component';
import { RouterModule } from '@angular/router';
import { CalculationCommonModule } from '../calculation-common.module';
import { AppCommonModule } from '../../common/common.module';


@NgModule({
    imports: [
        CalculationCommonModule,
        RouterModule.forChild([
            { path: '', component: FireDamageComponent }
        ])],
    exports: [],
    declarations: [FireDamageComponent],
    providers: [],
})
export class FireDamageModule { }
