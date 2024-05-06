import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EStatusCapacidade } from '../../core/abrigo.model';

@Component({
  selector: 'cw-abrigo-ajuda-pesquisa-avancada',
  templateUrl: './abrigo-ajuda-pesquisa-avancada.component.html',
  styleUrl: './abrigo-ajuda-pesquisa-avancada.component.scss'
})
export class AbrigoAjudaPesquisaAvancadaComponent {

  form: FormGroup;
  control: any;

  opcaoSimNao: any[] = [
    {value: '', viewValue: 'Todos'},
    {value: 'true', viewValue: 'Sim'},
    {value: 'false', viewValue: 'Não'},
  ];
  capacidades: any[] = [
    {value: '', viewValue: 'Todos'},
    {value: EStatusCapacidade.Disponivel, viewValue: 'Disponível'},
    {value: EStatusCapacidade.Lotado, viewValue: 'Lotado'},
  ];

  constructor( fb: FormBuilder,  public dialogRef: MatDialogRef<AbrigoAjudaPesquisaAvancadaComponent>,
    @Inject(MAT_DIALOG_DATA) params: any) {
    this.form = fb.group({
      bairro: '',
      alimento: '',
      capacidade: '',
      precisaAjudante: '',
      precisaAlimento: ''
    });
    this.form.patchValue(params.form.value);
    this.control = params.control;
  }

  pesquisar(): void {
    this.dialogRef.close(this.form.value);
  }
  fechar(): void {
    this.dialogRef.close();
  }
}
