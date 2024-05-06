import { Observable } from 'rxjs';
import { IEntity } from '../../../core/entities/i-entity';
import { ControlCvaProvider } from './../control-value-accessor/control-cva/control-cva.provider';
export abstract class BaseFormParams {

  constructor(public readonly label: string, public readonly controlsCvaProvider: ControlCvaProvider<any, any>[], public readonly submitFunc: (entity: IEntity) => Observable<any>) { }

}
