import { Entity } from "./entity";
import { INamedEntity } from "./i-named-entity";

export class NamedEntity extends Entity implements INamedEntity {
  name: string;
  constructor(id: any, name: any){
    super(id);
    this.name = name;
  }
}
