import { IEntity } from './../../../core/entities/i-entity';
export class CrudListOption {
  constructor(public displayName: string, public action: (entity: IEntity) => void) { }
}
