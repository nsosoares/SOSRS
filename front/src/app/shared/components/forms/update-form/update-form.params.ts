import { Observable } from 'rxjs';

import { IEntity } from '../../../../core/entities/i-entity';
import { ControlCvaProvider } from '../../control-value-accessor/control-cva/control-cva.provider';
import { BaseFormParams } from '../base-form-params';

export class UpdateFormParams extends BaseFormParams {

  constructor(label: string, controlsCvaProvider: ControlCvaProvider<any, any>[], submitFunc: (entity: IEntity) => any, public readonly entity: IEntity, public readonly funcGetEntityByIdFunc: (id: string) => Observable<any>) {
    super(label, controlsCvaProvider,submitFunc);
  }
}
