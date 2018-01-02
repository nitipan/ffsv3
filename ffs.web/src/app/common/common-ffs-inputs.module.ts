import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { FFSTextComponent, FFSSelectComponent, FFSCheckComponent, FFSDateComponent, FFSNumberComponent, FFSBrowseComponent, FFSColorComponent } from './inputs/ffs-input.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule],
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
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
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
