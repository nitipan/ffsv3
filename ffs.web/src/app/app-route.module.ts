import { Routes, RouterModule } from "@angular/router";
import { PlaygroundComponent } from "./playground/playground.component";
import { HomeComponent } from "./home/home.component";
import { NgModule } from "@angular/core";
import { AppCommonModule } from "./common/common.module";
import { CanActivateViaAuthGuard } from "./can-activate-via-auth-guard";


const appRoutes: Routes = [
    { path: 'playground', component: PlaygroundComponent },
    { path: 'home', component: HomeComponent },
    { path: 'module', loadChildren: './modules/calculation.module#CalculationModule' },
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    }
];


@NgModule({
    declarations: [PlaygroundComponent, HomeComponent],
    imports: [AppCommonModule, RouterModule.forRoot(appRoutes)],
    exports: [RouterModule],
    providers: [CanActivateViaAuthGuard],
})
export class AppRoutingModule { }
