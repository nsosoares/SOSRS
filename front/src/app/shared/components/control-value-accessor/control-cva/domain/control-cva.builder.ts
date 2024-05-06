import { Type } from "@angular/core";

export class ControlCvaBuilder {
  readonly controlProvider: Type<any>;

  constructor(controlProvider: Type<any>){
    this.controlProvider = controlProvider;
  }
}
