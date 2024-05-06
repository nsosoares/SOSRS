import { Injectable } from '@angular/core';
import { Abrigo, AbrigosResult, EStatusCapacidade, abrigos } from './abrigo.model';
import { Observable, map, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AbrigoService {
  readonly entities: Abrigo[] = abrigos;

  constructor(private _httpClient: HttpClient) { }

  codigosDeAcesso = [
    { id: 20704, nome: 'user1' },
    { id: 24505, nome: 'user2' },
    { id: 34598, nome: 'user3' },
    { id: 45674, nome: 'user4' },
    { id: 56782, nome: 'user5' },
    { id: 67893, nome: 'user6' },
    { id: 78904, nome: 'user7' },
    { id: 89016, nome: 'user8' },
    { id: 90123, nome: 'user9' },
    { id: 52342, nome: 'user10' },
    { id: 23458, nome: 'user11'}

  ];
  codAcesso?: number;
  pesquisar(value: any): Observable<AbrigosResult> {
    const finalValue = {
      nome: value.nome ? value.nome : undefined,
      cidade: value.cidade ? value.cidade : undefined,
      bairro: value.bairro ? value.bairro : undefined,
      alimento: value.alimento ? value.alimento : undefined,
      capacidade: value.capacidade ? value.capacidade : undefined,
      precisaAjudante: value.precisaAjudante ? value.precisaAjudante : undefined,
      precisaAlimento: value.precisaAlimento ? value.precisaAlimento : undefined
    }
let httpParams = new HttpParams();
if (finalValue.nome) {
  httpParams = httpParams.set('nome', finalValue.nome);
}

if (finalValue.cidade) {
  httpParams = httpParams.set('cidade', finalValue.cidade);
}

if (finalValue.bairro) {
  httpParams = httpParams.set('bairro', finalValue.bairro);
}

if (finalValue.alimento) {
  httpParams = httpParams.set('alimento', finalValue.alimento);
}

if (finalValue.capacidade) {
  httpParams = httpParams.set('capacidade', finalValue.capacidade);
}

if (finalValue.precisaAjudante) {
  httpParams = httpParams.set('precisaAjudante', finalValue.precisaAjudante);
}

if (finalValue.precisaAlimento) {
  httpParams = httpParams.set('precisaAlimento', finalValue.precisaAlimento);
}
      return this._httpClient.get<AbrigosResult>('https://localhost:7228/api/abrigos', { params: httpParams });
  }
  getById = (id: any): Observable<Abrigo> => {
    return this._httpClient.get<any>('https://localhost:7228/api/abrigos/' + id);
  }
  searchByName = (data: any): Observable<any[]> => {
    return this.pesquisar(data).pipe(
      map(result => result.abrigos),
      map(abrigosResult =>
        abrigosResult.map(abrigoResult => {
          return {
          ...abrigoResult,
          capacidadeDesc: abrigoResult.capacidade === EStatusCapacidade.Lotado  ? 'Lotado' : 'Disponível',
          precisaAjudanteDesc: abrigoResult.precisaAjudante ? 'Sim' : 'Não',
          precisaAlimentoDesc: abrigoResult.precisaAlimento ? 'Sim' : 'Não',
          capacidadeCssClass: abrigoResult.capacidade === EStatusCapacidade.Lotado ? 'alerta-perigo' : 'alerta-sucesso',
          precisaAjudanteCssClass: abrigoResult.precisaAjudante ? 'alerta-perigo' : 'alerta-sucesso',
          precisaAlimentoCssClass: abrigoResult.precisaAlimento ? 'alerta-perigo' : 'alerta-sucesso',
        }}
      ))
    );
  }

  getEntities = (): Observable<Abrigo[]> => {
    return of(this.entities);
  }

  create = (entity: Abrigo): Observable<any> => {
    entity.id = 0;
    if ((entity.alimentos as any) === "") {
      entity.alimentos = [];
    }
    this.entities.push(entity);
    return this._httpClient.post<any[]>('https://localhost:7228/api/abrigos', entity, this.getHeader());
  }
  update = (entity: Abrigo): Observable<any> => {
    return this._httpClient.put<any[]>('https://localhost:7228/api/abrigos/' + entity.id, entity, this.getHeader());
  }

  delete = (id: string): Observable<any> => {
    const index = this.entities.findIndex(x => x.id === id);
    this.entities.splice(index, 1);
    return of(null);
  }

  getHeader(): any {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'codAcesso': this.codAcesso!.toString()
    });
    return { headers: headers };
  }
}
