import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { debounceTime } from 'rxjs';

import { RESPONSIVE_SIZE_6 } from '../../../core/constants/app_constants';
import { STARTER_ANIMATIONS } from '../../../shared/animations/on-show.animations';
import {
  InputTextCvaParams,
} from '../../../shared/components/control-value-accessor/children/input-text-cva/input-text-cva.params';
import { ControlCvaProvider } from '../../../shared/components/control-value-accessor/control-cva/control-cva.provider';
import { TemplateService } from '../../../template/template.service';
import { AbrigoService } from '../core/abrigo.service';
import { AbrigoPesquisa, EStatusCapacidade } from './../core/abrigo.model';
import {
  AbrigoAjudaPesquisaAvancadaComponent,
} from './abrigo-ajuda-pesquisa-avancada/abrigo-ajuda-pesquisa-avancada.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'cw-abrigo-ajuda',
  templateUrl: './abrigo-ajuda.component.html',
  styleUrl: './abrigo-ajuda.component.scss',
  animations: [STARTER_ANIMATIONS]

})
export class AbrigoAjudaComponent {

  form: FormGroup;
  abrigos: AbrigoPesquisa[] = [];
  quantidade?: number;
  carregando = false;

  control = {
    nome: ControlCvaProvider.inputText(() => InputTextCvaParams.text('nome', 'Nome do local', 50, 0).withCssClass(RESPONSIVE_SIZE_6)),
    cidade: ControlCvaProvider.inputText(() => InputTextCvaParams.text('cidade', 'Cidade', 100, 0).withCssClass(RESPONSIVE_SIZE_6)),
    bairro: ControlCvaProvider.inputText(() => InputTextCvaParams.text('bairro', 'Bairro', 100, 0).withCssClass(RESPONSIVE_SIZE_6)),
    alimento: ControlCvaProvider.inputText(() => InputTextCvaParams.text('alimento', 'Alimento', 50, 0).asRequired().withCssClass(RESPONSIVE_SIZE_6)),
  }
  constructor(private _snackBar: MatSnackBar, templateService: TemplateService, fb: FormBuilder, private abrigoService: AbrigoService, private dialog: MatDialog) {
    templateService.exibeMenu = false;
    this.form = fb.group({
      nome: '',
      cidade: '',
      bairro: '',
      alimento: '',
      capacidade: '',
      precisaAjudante: '',
      precisaAlimento: ''
    });

    this.form.valueChanges.pipe(
      debounceTime(500)
    ).subscribe(() => {
      this.pesquisa();
    });
    this.pesquisa();
  }

  pesquisa(): void {
    console.log(this.form.value);
    this.carregando = true;
    this.abrigoService.pesquisar(this.form.value).pipe(
      debounceTime(500),
    ).subscribe(result => {
      this.abrigos = result.abrigos.map(abrigoResult => {
        return {
          ...abrigoResult,
          capacidadeDesc: abrigoResult.capacidade === EStatusCapacidade.Lotado  ? 'Lotado' : 'Disponível',
          precisaAjudanteDesc: abrigoResult.precisaAjudante ? 'Sim' : 'Não',
          precisaAlimentoDesc: abrigoResult.precisaAlimento ? 'Sim' : 'Não',
          capacidadeCssClass: abrigoResult.capacidade === EStatusCapacidade.Lotado ? 'alerta-perigo' : 'alerta-sucesso',
          precisaAjudanteCssClass: abrigoResult.precisaAjudante ? 'alerta-perigo' : 'alerta-sucesso',
          precisaAlimentoCssClass: abrigoResult.precisaAlimento ? 'alerta-perigo' : 'alerta-sucesso',
          enderecoDesc:
            this.concatenarSePossuirValor('cidade', abrigoResult.cidade) + ' - ' +
            this.concatenarSePossuirValor('bairro', abrigoResult.bairro) + ' - ' +
            this.concatenarSePossuirValor('rua', abrigoResult.rua) + ' - ' +
            this.concatenarSePossuirValor('numero', abrigoResult.numero?.toString()) + ' - ' +
            (abrigoResult.complemento? `complemento: ${abrigoResult.complemento}` : ''),
        };
      });
      this.quantidade = result.quantidadeTotalRegistros;
      this.carregando = false;
    });

  }
  copyMessage(val: string){
    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = val;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
    this._snackBar.open('Copiado para a área de transferência', '', {duration: 1800})
  }
  copyToClipboard(val: string): void {
    this.copyMessage(val);
  }
  concatenarSePossuirValor(display: string, valor?: string): string {
    const valorNulo = valor === null || valor === undefined || valor === '';
    const valorFinal = valorNulo ? 'Não fornecido' :  valor;
    return `${display}: ${valorFinal}`;
  }

  abrirPesquisaAvancada(): void {
    this.dialog.open(AbrigoAjudaPesquisaAvancadaComponent, {
      width: '800px',
      data:{ form: this.form, control: this.control},
      disableClose: true
    }).afterClosed().subscribe(result => {
      if (result) {
        this.form.patchValue(result);
        this.pesquisa();
      }

    });
  }
}

// filtro vamos por o que?
// digitavel:
// - nome
// - cidade
// - bairro
// - alimento

// capacidade: (lotado/disponível/todos)
// precisa de ajudante (true/false/null)
// precisa de alimento (true/false/null)

// 50 resultados máximo

// trazer também quantidade total de registros encontrados(além dos 50 máximo)
