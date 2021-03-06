import { NgModule } from '@angular/core';
import { EventService } from '../event.service';
import { DatePipe } from '@angular/common';
import { AsyncKVPipe } from './asynckv.pipe';
import { FourthOrderPolyNomialComponent } from './inputs/fourth-order-poly-nomial/fourth-order-poly-nomial.component';
import { FocusDirective } from './focus.directive';
import { HelpComponent } from '../help/help.component';
import { DataGridComponent } from './datagrid/datagrid.component';
import { TopnavComponent } from './topnav/topnav.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { PanelComponent } from './panel/panel.component';
import { WizardComponent } from './wizard/wizard.component';
import { TabComponent } from './tab/tab.component';
import { TabItemComponent } from './tab/tab-item.component';
import { FFSInputModule } from './common-ffs-inputs.module';
import { ChartComponent } from './chart/chart.component';
import { DataGridItemDirective } from './datagrid/datagrid-item.directive';

@NgModule({
  imports: [FFSInputModule],
  exports: [
    FFSInputModule,
    DataGridComponent,
    TopnavComponent,
    SidenavComponent,
    WizardComponent,
    PanelComponent,
    TabComponent,
    TabItemComponent,
    HelpComponent,
    FocusDirective,
    FourthOrderPolyNomialComponent,
    AsyncKVPipe,
    ChartComponent,
    DataGridItemDirective
  ],
  declarations: [
    DataGridComponent,
    TopnavComponent,
    SidenavComponent,
    WizardComponent,
    PanelComponent,
    TabComponent,
    TabItemComponent,
    HelpComponent,
    FocusDirective,
    FourthOrderPolyNomialComponent,
    AsyncKVPipe,
    ChartComponent,
    DataGridItemDirective
  ],
  providers: [EventService, DatePipe]
})
export class AppCommonModule {}
