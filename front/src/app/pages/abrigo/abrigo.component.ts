import { Component, EnvironmentInjector } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

import {
  RESPONSIVE_SIZE_12,
  RESPONSIVE_SIZE_2,
  RESPONSIVE_SIZE_4,
  RESPONSIVE_SIZE_6,
} from '../../core/constants/app_constants';
import {
  InputTextCvaParams,
} from '../../shared/components/control-value-accessor/children/input-text-cva/input-text-cva.params';
import { SubformCvaParams } from '../../shared/components/control-value-accessor/children/subform-cva/subform-cva.params';
import { ControlCvaProvider } from '../../shared/components/control-value-accessor/control-cva/control-cva.provider';
import { CrudListColumn, CrudListParams } from '../../shared/components/crud-list/crud-list.params';
import { CrudParams } from '../../shared/components/crud/crud.params';
import { AbrigoService } from './core/abrigo.service';
import { Subject, debounce, debounceTime } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'cw-abrigo',
  templateUrl: './abrigo.component.html',
  styleUrl: './abrigo.component.scss'
})
export class AbrigoComponent {
  params: CrudParams;
  controls: ControlCvaProvider<any, any>[] = [];
  logado = false;
  env = environment.env;
  form: FormGroup;

  constructor(private abrigoService: AbrigoService, fb: FormBuilder, private _snackBar: MatSnackBar) {
    const control = {
      codAcesso: ControlCvaProvider.inputText(() => InputTextCvaParams.text('codAcesso', 'Codigo de acesso')),
      id: ControlCvaProvider.inputText(() => InputTextCvaParams.hidden('id')),
      nome: ControlCvaProvider.inputText(() => InputTextCvaParams.text('nome', 'Nome', 50, 5).asRequired().withPlaceholder('Digite o nome do abrigo').withCssClass(RESPONSIVE_SIZE_6)),
      chavePix: ControlCvaProvider.inputText(() => InputTextCvaParams.text('chavePix', 'Chave Pix', 50, 3).withPlaceholder('Digite a chave pix').withCssClass(RESPONSIVE_SIZE_6)),
      telefone: ControlCvaProvider.inputText(() => InputTextCvaParams.text('telefone', 'telefone para contato', 50, 3).asRequired().withPlaceholder('digite o número').withCssClass(RESPONSIVE_SIZE_6)),
      quantidadeNecessariaVoluntarios: ControlCvaProvider.inputText(() => InputTextCvaParams.number('quantidadeNecessariaVoluntarios', 'Precisa de quantos voluntarios?').withCssClass(RESPONSIVE_SIZE_6)),
      capacidadeTotalPessoas: ControlCvaProvider.inputText(() => InputTextCvaParams.number('capacidadeTotalPessoas', 'Quantas pessoas o local pode suportar?').withCssClass(RESPONSIVE_SIZE_6)),
      quantidadeVagasDisponiveis: ControlCvaProvider.inputText(() => InputTextCvaParams.number('quantidadeVagasDisponiveis', 'Quantas vagas estão sobrando no momento?').withCssClass(RESPONSIVE_SIZE_6)),
      observacao: ControlCvaProvider.inputText(() => InputTextCvaParams.text('observacao', 'Observação').withCssClass(RESPONSIVE_SIZE_12)),

      endereco: ControlCvaProvider.subform(() => new SubformCvaParams('endereco', 'Endereço', [
        // ControlCvaProvider.inputText(() => InputTextCvaParams.text('cep', 'CEP', 50, 1).withPlaceholder('Digite o CEP').withCssClass(RESPONSIVE_SIZE_6)),
        ControlCvaProvider.inputText(() => InputTextCvaParams.text('cidade', 'Cidade', 50, 1).asRequired().withPlaceholder('Digite a cidade').withCssClass(RESPONSIVE_SIZE_6)),
        ControlCvaProvider.inputText(() => InputTextCvaParams.text('bairro', 'Bairro', 50, 1).asRequired().withPlaceholder('Digite o bairro').withCssClass(RESPONSIVE_SIZE_4)),
        ControlCvaProvider.inputText(() => InputTextCvaParams.text('rua', 'Rua', 50, 1).asRequired().withPlaceholder('Digite a rua').withCssClass(RESPONSIVE_SIZE_6)),
        ControlCvaProvider.inputText(() => InputTextCvaParams.number('numero', 'Número').asRequired().withCssClass(RESPONSIVE_SIZE_2)),
        ControlCvaProvider.inputText(() => InputTextCvaParams.text('complemento', 'Complemento', 50, 1).withPlaceholder('Digite o complemento').withCssClass(RESPONSIVE_SIZE_12)),
      ]).asRequired()),

      alimentos: ControlCvaProvider.subforms(() => new SubformCvaParams('alimentos', 'Recursos necessários', [
        ControlCvaProvider.inputText(() => InputTextCvaParams.text('nome', 'Recurso', 50, 5).asRequired().withPlaceholder('Digite o recurso').withCssClass(RESPONSIVE_SIZE_6)),
        ControlCvaProvider.inputText(() => InputTextCvaParams.number('quantidadeNecessaria', 'Quantidade Necessaria').withCssClass(RESPONSIVE_SIZE_6)),
      ])),

      pessoasDesaparecidas: ControlCvaProvider.subforms(() => new SubformCvaParams('pessoasDesaparecidas', 'Pessoas Desaparecidas', [
        ControlCvaProvider.inputText(() => InputTextCvaParams.text('nome', 'Nome', 50, 5).asRequired().withPlaceholder('Digite o Nome da Pessoa').withCssClass(RESPONSIVE_SIZE_6)),
        ControlCvaProvider.inputText(() => InputTextCvaParams.number('idade', 'Idade').withCssClass(RESPONSIVE_SIZE_6)),
        ControlCvaProvider.inputText(() => InputTextCvaParams.text('informacaoAdicional', 'Informações Adicionais', 500).withPlaceholder('Digite informações adicionais sobre essa pessoa').withCssClass(RESPONSIVE_SIZE_12))
      ]))
    };
    this.form = fb.group({
      codAcesso: control.codAcesso.formControl,
    });
    this.controls.push(...[
      control.id,
      control.nome,
      control.chavePix,
      control.telefone,
      control.quantidadeNecessariaVoluntarios,
      control.capacidadeTotalPessoas,
      control.quantidadeVagasDisponiveis,
      control.endereco,
      control.alimentos,
      control.pessoasDesaparecidas,
      control.observacao
    ]);
    const listParams = new CrudListParams(
      [
        new CrudListColumn('id', 'Id'),
        new CrudListColumn('nome', 'Nome'),
        new CrudListColumn('cidade', 'Cidade'),
        new CrudListColumn('bairro', 'Bairro'),
        new CrudListColumn('precisaAjudanteDesc', 'Quer ajuda'),
        new CrudListColumn('precisaAlimentoDesc', 'Quer alimento'),
        new CrudListColumn('capacidadeDesc', 'Capacidade'),

      ]);
    this.params = new CrudParams('Abrigos', listParams, this.controls, abrigoService.searchByName, abrigoService.getById, abrigoService.create, abrigoService.update, abrigoService.delete);

    this.onlogin.pipe(debounceTime(500)).subscribe((codAcesso) => {
      const usuario = abrigoService.codigosDeAcesso.find(x => x.id === parseInt(codAcesso.toString(), 10));
      if (usuario) {
        this.logado = true;
        this.abrigoService.codAcesso = usuario.id;
        this._snackBar.open('logado com sucesso - usuário: ' + usuario.nome, '', { duration: 1800 })
      } else {
        this._snackBar.open('login inválido', '', { duration: 1800 })

      }
    });

    this.logado = true;
    this.abrigoService.codAcesso = 1;
  }

  onlogin = new Subject<number>();
  logar(): void {
    this.onlogin.next(this.form.value.codAcesso);
  }
}

