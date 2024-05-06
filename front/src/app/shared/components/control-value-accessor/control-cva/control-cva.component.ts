import { AfterViewInit, ChangeDetectorRef, Component, forwardRef, input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime } from 'rxjs';

import {
  CoreControlValueAccessor,
} from '../../../../core/components/core-control-value-accessor/core-control-value-accessor';
import { AppError } from '../../../../core/erros/app-error';
import { ControlCvaDirective } from './control-cva.directive';
import { ControlCvaProvider } from './control-cva.provider';
import { BaseControlCvaParams } from './domain/base-control-cva.params';
import { ControlCvaService } from './domain/control-cva.service';

@Component({
  selector: 'cw-control-cva',
  templateUrl: './control-cva.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ControlCvaComponent),
      multi: true,
    },
  ]
})
export class ControlCvaComponent extends CoreControlValueAccessor<object> implements AfterViewInit {
  params: any;
  form!: FormGroup;
  controlProvider = input<ControlCvaProvider<any,any>>();

  @ViewChild(ControlCvaDirective, { static: false }) controlDisplayedDirective!: ControlCvaDirective;

  constructor(private _controlCvaService: ControlCvaService, formBuilder: FormBuilder, private _changeDetectorRef: ChangeDetectorRef) {
    super();
    this._configurarRegistroDeValorCvaFilho(formBuilder);
  }

  private _configurarRegistroDeValorCvaFilho(formBuilder: FormBuilder) {
    this.form = formBuilder.group({
      currentValue: formBuilder.control('')
    });
    this.form.valueChanges.pipe(
      debounceTime(50)
    ).subscribe((value) => {
      this.registrarToqueDelegate!(value.currentValue);
      this.registerChange(value.currentValue);
    });
  }

  ngAfterViewInit(): void {
    this.setNewControToDisplay();
    this.transferCVAMethodsToNewControl();
    this.initNewControl();
    this._changeDetectorRef.detectChanges();
  }

  private initNewControl() {
    this.controlDisplayedDirective.content.addParams(this.controlProvider()!.params!);
    this.controlDisplayedDirective.content.init();
  }

  private transferCVAMethodsToNewControl() {
    this.tryWriteValue(this.form.get('currentValue')?.value);
    this.tryRegisterOnChange(this.controlDisplayedDirective?.registerOnChangeGlobal);
    this.tryRegisterOnTouched(this.controlDisplayedDirective?.registerOnTouchedGlobal);
  }

  private setNewControToDisplay() {
    this.controlDisplayedDirective.viewContainerRef.clear();

    const newControlToDisplayType = this._controlCvaService
      .getFromProvidedByType(this.controlProvider()!.componentCvaType)
      .componentCvaType;

    this.controlDisplayedDirective.content = this.controlDisplayedDirective
      .viewContainerRef
      .createComponent(newControlToDisplayType)
      .instance;
  }

  override writeValue(obj: any): void {
    this.form.get('currentValue')?.setValue(obj);
    this.tryWriteValue(obj);
  }

  private tryWriteValue(value: any, limit = 0): void {
    if (limit > 50) return;

    if (!this.controlDisplayedDirective?.content) {
      setTimeout(() => {
        this.tryWriteValue(value, limit + 1);
      }, 50);
      return;
    }
    try {
      this.controlDisplayedDirective.content.writeValue(value);
    } catch (error) {
      throw new AppError('Erro ao tentar registrar o valor setado de forma externa', error, value);
    }
  }

  private tryRegisterOnChange(fn: any, limit = 0): void {
    if (limit > 50) return;

    if (!this.controlDisplayedDirective?.content) {
      setTimeout(() => {
        this.tryRegisterOnChange(fn, limit + 1);
      }, 50);
      return;
    }
    try {
      this.controlDisplayedDirective.content.registerOnChange(fn);
    } catch (error) {
      throw new AppError('Erro ao tentar registrar a função de mudança de valor', error);
    }
  }

  private tryRegisterOnTouched(fn: any, limit = 0): void {
    if (limit > 50) return;

    if (!this.controlDisplayedDirective?.content) {
      setTimeout(() => {
        this.tryRegisterOnTouched(fn, limit + 1);
      }, 50);
      return;
    }
    try {
      this.controlDisplayedDirective?.content?.registerOnTouched(fn);
    } catch (error) {
      throw new AppError('Erro ao tentar registrar o evento de toque', error);
    }
  }


  override setDisabledState?(isDisabled: boolean): void {
    if (isDisabled)
      this.form.disable();
    else
      this.form.enable();
  }

  override addParams(params: BaseControlCvaParams): void {
    throw new AppError('Esse metodo nao deve ser chamado. É uma quebra de LSP, mas necessária neste contexto, sendo a unica exceção.');
  }
  override init(): void {
    throw new AppError('Esse metodo nao deve ser chamado. É uma quebra de LSP, mas necessária neste contexto, sendo a unica exceção.');
  }

}
