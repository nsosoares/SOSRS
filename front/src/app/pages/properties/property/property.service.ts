import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { INamedEntity } from '../../../core/entities/i-named-entity';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {

  readonly entities: INamedEntity[] =  [
    {id: '1', name: 'Imóvel 1'},
    {id: '2', name: 'Imóvel 2'},
    {id: '3', name: 'Imóvel 3'},
    {id: '4', name: 'Imóvel 4'},
    {id: '5', name: 'Imóvel 5'},
    {id: '6', name: 'Imóvel 6'},
    {id: '7', name: 'Imóvel 7'},
    {id: '8', name: 'Imóvel 8'},
    {id: '9', name: 'Imóvel 9'},
    {id: '10', name: 'Imóvel 10'},
    {id: '11', name: 'Imóvel 11'},
    {id: '12', name: 'Imóvel 12'},
    {id: '13', name: 'Imóvel 13'},
    {id: '14', name: 'Imóvel 14'},
    {id: '15', name: 'Imóvel 15'},
    {id: '16', name: 'Imóvel 16'},
    {id: '17', name: 'Imóvel 17'},
    {id: '18', name: 'Imóvel 18'},
    {id: '19', name: 'Imóvel 19'},
  ];
  constructor() { }

  getById = (id: string): Observable<INamedEntity> => {
    return of(this.entities.find(x => x.id === id)!);
  }
  searchByName = (name: any): Observable<INamedEntity[]> => {
    const lista: INamedEntity[] = this.entities;
    return of(lista.filter(x => x.name.toLowerCase().includes(name.toLowerCase())));
  }
  getEntities = (): Observable<INamedEntity[]> => {
    return of(this.entities);
  }

  create = (entity: INamedEntity): Observable<any> => {
    const ramdon = Math.floor(Math.random() * 1000);
    entity.id = ramdon.toString();
    this.entities.push(entity);
    return of(entity.id);
  }
  update = (entity: INamedEntity): Observable<any> => {
    const index = this.entities.findIndex(x => x.id === entity.id);
    this.entities[index] = entity;
    return of(null);
  }

  delete = (id: string): Observable<any> => {
    const index = this.entities.findIndex(x => x.id === id);
    this.entities.splice(index, 1);
    return of(null);
  }
}
