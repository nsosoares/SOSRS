import { Injectable, Type } from '@angular/core';

import { AppError } from '../../../../../core/erros/app-error';
import { ControlCvaProvider } from '../control-cva.provider';

@Injectable({
  providedIn: 'root'
})
export class ControlCvaService {

  getAllProvidedControls(): ControlCvaProvider<any,any>[] {
    const providedControls: ControlCvaProvider<any,any>[] = [];
    const providedMethods = this._getMethodsToProvide();

    for (const metodo of providedMethods) {
      const controlProviderGenerico = ControlCvaProvider as any;
      providedControls.push(controlProviderGenerico[metodo]());
    }
    return providedControls;
  }

  getFromProvidedByType(type: Type<any>): ControlCvaProvider<any, any> {
    const providedControl = this.getAllProvidedControls().find(r => r.componentCvaType === type);

    if (providedControl)
      return providedControl;

    throw new AppError('Registro não encontrado');
  }

  private _getMethodsToProvide(): string[] {
    const metodosIgnorados = ['length', 'name', 'prototype'];
    const todosMetodos = Object.getOwnPropertyNames(ControlCvaProvider);
    const providedMethods = todosMetodos.filter(m => !metodosIgnorados.includes(m));
    return providedMethods;
  }
}

