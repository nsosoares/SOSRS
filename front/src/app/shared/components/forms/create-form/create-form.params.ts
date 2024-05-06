import { IEntity } from '../../../../core/entities/i-entity';
import { ControlCvaProvider } from '../../control-value-accessor/control-cva/control-cva.provider';
import { BaseFormParams } from '../base-form-params';

export class CreateFormParams extends BaseFormParams {
  constructor(label: string, controlsCvaProvider: ControlCvaProvider<any, any>[], submitFunc: (entity: IEntity) => any) {
    super(label, controlsCvaProvider,submitFunc);
  }
}
