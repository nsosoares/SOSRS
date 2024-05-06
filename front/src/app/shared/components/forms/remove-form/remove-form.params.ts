import { INamedEntity } from '../../../../core/entities/i-named-entity';

export class RemoveFormParams {
  constructor(public readonly label: string, public readonly entity: INamedEntity, public readonly removeFunc: (entityId: string) => any) {
  }
}
