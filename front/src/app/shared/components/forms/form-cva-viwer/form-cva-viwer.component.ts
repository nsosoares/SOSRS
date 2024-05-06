import { Component, input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { CoreControlValueAccessor } from '../../../../core/components/core-control-value-accessor/core-control-value-accessor';
import { ControlCvaProvider } from '../../control-value-accessor/control-cva/control-cva.provider';
import { BaseControlCvaParams } from '../../control-value-accessor/control-cva/domain/base-control-cva.params';

@Component({
  selector: 'cw-form-cva-viwer',
  templateUrl: './form-cva-viwer.component.html',
  styleUrl: './form-cva-viwer.component.scss',
})
export class FormCvaViwerComponent implements OnInit {

  form = input<FormGroup>();
  cvaControls = input<ControlCvaProvider<CoreControlValueAccessor<any>, BaseControlCvaParams>[]>();

  ngOnInit(): void {
    console.log(this.cvaControls(), 'form');
  }

}

