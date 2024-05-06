import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CORE_APP_MATERIAL_MODULES } from './modules/material.module';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  exports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    CORE_APP_MATERIAL_MODULES
  ]
})
export class CoreModule { }
