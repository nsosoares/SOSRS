import { CrudListOption } from "./crud-list-option";

export class CrudListParams {
  entities?: any[];
  constructor(public readonly columns: CrudListColumn[], public readonly options: CrudListOption[] = []) {}

  setEntites(newEntities: any[]) {
    if (!this.entities) this.entities = [];
    this.entities.length = 0;
    this.entities.push(...newEntities);
  }
}

export class CrudListColumn {
  constructor(public property: string, public displayName: string) { }
}
