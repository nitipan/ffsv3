import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { FFSTextComponent, FFSSelectComponent, FFSCheckComponent, FFSNumberComponent, FFSDateComponent, FFSBrowseComponent, FFSColorComponent } from './common/inputs/ffs-input.component';
import { NgModule } from '@angular/core';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    FFSTextComponent,
    FFSSelectComponent,
    FFSCheckComponent,
    FFSNumberComponent,
    FFSDateComponent,
    FFSBrowseComponent,
    FFSColorComponent
  ],
  exports: [
    FFSTextComponent,
    FFSSelectComponent,
    FFSCheckComponent,
    FFSNumberComponent,
    FFSDateComponent,
    FFSBrowseComponent,
    FFSColorComponent
  ]
})
export class FFSInputModule { }
