import { Injectable } from '@angular/core';
import { Abrigo, AbrigosResult, EStatusCapacidade, ETipoDeAbrigo, abrigos } from './abrigo.model';
import { Observable, Subject, map, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { AuthService } from '../../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AbrigoService {
  readonly entities: Abrigo[] = abrigos;
  readonly longitudeKey = 'longitude';
  readonly latitudeKey = 'latitude';
  readonly baseUrl = environment.api;

  enderecoPorGps: {
    cidade?: string;
    preenchido: boolean;
  } = { preenchido: false};

  aoEncontrarEnderecpPorGps = new Subject<any>();
  constructor(private _httpClient: HttpClient, private _authService: AuthService) {
    this.updateByVersion();
    // navigator.geolocation.getCurrentPosition(this.aoObterPosicao)
  }

  aoObterPosicao = (data: any) => {
    const valoresSalvos = {
      longitudeKey: localStorage.getItem(this.longitudeKey),
      latitudeKey: localStorage.getItem(this.latitudeKey)
    }
    const saoMesmosValores = valoresSalvos.longitudeKey === data.coords.longitude.toString() && valoresSalvos.latitudeKey === data.coords.latitude.toString();
    if (saoMesmosValores) {
      this.aoEncontrarEnderecpPorGps.next({ cidade: localStorage.getItem('cidade'), longitude: data.coords.longitude, latitude: data.coords.latitude });
      return;
    }
    this.getGeoLocation(data.coords.latitude, data.coords.longitude);
}




getGeoLocation(lat: number, lng: number){

}


  codAcesso?: number;
  pesquisar(value: any, auth: boolean): Observable<AbrigosResult> {
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

    if (auth) {
      return this._httpClient.get<AbrigosResult>(this.baseUrl + 'api/abrigos/GetByUserId', { params: httpParams, headers: this.getHeaderWithToken() });
    }
    return this._httpClient.get<AbrigosResult>(this.baseUrl + 'api/abrigos', { params: httpParams });
  }
  getById = (id: any): Observable<Abrigo> => {
    return this._httpClient.get<any>(this.baseUrl + 'api/abrigos/' + id);
  }
  searchByName = (data: any): Observable<any[]> => {
    return this.pesquisar(data, true).pipe(
      map(result => result.abrigos),
      map(abrigosResult =>
        abrigosResult.map(abrigoResult => {
          return {
            ...abrigoResult,
            capacidadeDesc: abrigoResult.capacidade === EStatusCapacidade.Lotado ? 'Lotado' : 'Disponível',
            precisaAjudanteDesc: abrigoResult.precisaAjudante ? 'Sim' : 'Não',
            precisaAlimentoDesc: abrigoResult.precisaAlimento ? 'Sim' : 'Não',
            capacidadeCssClass: abrigoResult.capacidade === EStatusCapacidade.Lotado ? 'alerta-perigo' : 'alerta-sucesso',
            precisaAjudanteCssClass: abrigoResult.precisaAjudante ? 'alerta-perigo' : 'alerta-sucesso',
            precisaAlimentoCssClass: abrigoResult.precisaAlimento ? 'alerta-perigo' : 'alerta-sucesso',
            tipoDeAbrigo: ETipoDeAbrigo.Animais,
            // tipoDeAbrigo: abrigoResult.tipoAbrigo,
            // tipoAbrigoDescricao: obterDescricao(abrigoResult.tipoAbrigo)
            tipoAbrigoDescricao: this.obterDescricao(ETipoDeAbrigo.Animais)
          }
        }
        ))
    );
  }

  private getHeaderWithToken(): any {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + this._authService.getToken()
    });
    return headers;
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
    return this._httpClient.post<any[]>(this.baseUrl + 'api/abrigos', entity, { headers: this.getHeaderWithToken() });
  }
  update = (entity: Abrigo): Observable<any> => {
    return this._httpClient.put<any[]>(this.baseUrl + 'api/abrigos/' + entity.id, entity, { headers: this.getHeaderWithToken() });
  }

  delete = (id: string): Observable<any> => {
    return this._httpClient.delete<any>(this.baseUrl + 'api/abrigos/' + id, { headers: this.getHeaderWithToken() });
  }

  getHeader(): any {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'codAcesso': this.codAcesso!.toString()
    });
    return { headers: headers };
  }

  updateByVersion(): void {
    this._httpClient.get<any>(this.baseUrl + 'api/abrigos/version').subscribe(result => {
      const version = result.version;
      if (version !== localStorage.getItem('version')) {
        localStorage.setItem('version', version);
        const currentUrl = window.location.href;
        window.location.href = currentUrl + '?eraseCache=' + Date.now();
      }
    });
  }

  getLocation(lat: string, long: string): any {
    return this._httpClient.get<any>(`${this.baseUrl}api/Location?latitude=${lat}&longitude=${long}`);
  }
  obterDescricao(tipoAbrigo: ETipoDeAbrigo): any {
    if (tipoAbrigo === ETipoDeAbrigo.Animais)
      return 'animal';
    if (tipoAbrigo === ETipoDeAbrigo.Idosos){
      return 'idoso'
    }
  }
}


