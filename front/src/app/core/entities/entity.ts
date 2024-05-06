import { IEntity } from "./i-entity";

export class Entity implements IEntity{
  id: any;
  constructor(id: string){
    this.id = id;
  }
}
