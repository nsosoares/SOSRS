import { Observable } from "rxjs";
import { BaseControlCvaParams } from "../../control-cva/domain/base-control-cva.params";
import { INamedEntity } from "../../../../../core/entities/i-named-entity";

export class ComboboxCvaParams extends BaseControlCvaParams {
  private _placeholder?: string;
  get placeholder(): string | undefined {
    return this._placeholder;
  }
  constructor(name: any, label: string
    , public readonly getByIdFunc: (id: string) => Observable<INamedEntity>
    , public readonly SearchByKeyword: (name: any) => Observable<INamedEntity[]>
  ) {
    super(name, label);
  }

  withPlacehoolder(placeholder: string): this {
    this._placeholder = placeholder;
    return this;
  }
}
