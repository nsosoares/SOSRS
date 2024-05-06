import { ModuleWithProviders, Type } from '@angular/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import {MatMenuModule} from '@angular/material/menu';
import {MatSelectModule} from '@angular/material/select';
import { MatSnackBarModule} from '@angular/material/snack-bar';

export const CORE_APP_MATERIAL_MODULES: Array<Type<any> | ModuleWithProviders<{}> | any[]> = [
  MatFormFieldModule,
  MatInputModule,
  MatIconModule,
  MatAutocompleteModule,
  MatButtonModule,
  MatGridListModule,
  MatDialogModule,
  MatMenuModule,
  MatSelectModule,
  MatSnackBarModule
]

