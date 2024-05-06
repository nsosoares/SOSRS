import { Observable, of, switchMap, tap } from "rxjs";
import { INamedEntity } from "../../../../../core/entities/i-named-entity";
import { ComboboxCvaParams } from "./combobox-cva.params";

export class ComboboxService {
  lastSearchTerm: string | null = null;
  options: INamedEntity[] = [];

  private _isEnableSearch = true;
  private _params?: ComboboxCvaParams;
  get params(): ComboboxCvaParams | undefined {
    return this._params;
  }

  setParams(params: ComboboxCvaParams) {
    this._params = params;
  }

  getEntityById(id: any): Observable<INamedEntity> {
    return this.params!.getByIdFunc(id);
  }
  searchByKeyword(searchTerm: string): Observable<INamedEntity[]> {
    if (!this.params) return of([]);

    if (!this._isEnableSearch) {
      this._isEnableSearch = true;
      return of(this.options);
    }

    if (this.lastSearchTerm === searchTerm) return of(this.options);
    this.lastSearchTerm = searchTerm;

    const existCachedTearm = this.options.filter(option => option.name === searchTerm).length === 1;
    if (existCachedTearm) return of(this.options);

    return this.params.SearchByKeyword(searchTerm).pipe(
      switchMap((options) => {
        this.options = options;
        return of(this.options);
      })
    );
  }

  desableSearchToNextCall() {
    this._isEnableSearch = false;
  }
}
