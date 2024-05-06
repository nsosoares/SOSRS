import { Observable } from "rxjs";
import { ControlCvaProvider } from "../control-value-accessor/control-cva/control-cva.provider";
import { CrudListParams } from "../crud-list/crud-list.params";

export class CrudParams {
  private _controls: ControlCvaProvider<any, any>[];
  get controls(): ControlCvaProvider<any, any>[] { return this._controls; }

  private _funcGetEntities: (params: object) => Observable<any[]>;
  get funcGetEntities(): (params: object) => Observable<any[]> { return this._funcGetEntities; }

  private _funcGetEntity: (id: string) => Observable<any>;
  get funcGetEntity(): (id: string) => Observable<any> { return this._funcGetEntity; }

  private _funcCreateEntity: (entity: any) => Observable<any>;
  get funcCreateEntity(): (entity: any) => Observable<any> { return this._funcCreateEntity; }

  private _funcUpdateEntity: (entity: any) => Observable<any>;
  get funcUpdateEntity(): (entity: any) => Observable<any> { return this._funcUpdateEntity; }

  private _funcRemoveEntity: (id: string) => Observable<any>;
  get funcRemoveEntity(): (id: string) => Observable<any> { return this._funcRemoveEntity; }

  constructor(
    public readonly title: string,
    public readonly listParams: CrudListParams,
    controls: ControlCvaProvider<any, any>[],
    funcGetEntities: (params: any) => Observable<any[]>,
    funcGetEntity: (id: string) => Observable<any>,
    funcCreateEntity: (entity: any) => Observable<any>,
    funcUpdateEntity: (entity: any) => Observable<any>,
    funcRemoveEntity: (id: string) => Observable<any>
  ) {
    this._controls = controls;
    this._funcGetEntities = funcGetEntities;
    this._funcGetEntity = funcGetEntity;
    this._funcCreateEntity = funcCreateEntity;
    this._funcUpdateEntity = funcUpdateEntity;
    this._funcRemoveEntity = funcRemoveEntity;
  }
}
