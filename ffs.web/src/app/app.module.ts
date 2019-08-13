import { AppComponent } from './app.component';
import { AppCommonModule } from './common/common.module';
import { AppRoutingModule } from './app-route.module';
import { NgModule, ErrorHandler } from '@angular/core';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GlobalErrorHandler } from './error-handler';
@NgModule({
  declarations: [AppComponent],
  imports: [
    AppCommonModule,
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpModule
  ],
  providers: [
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
