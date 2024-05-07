import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AbrigoService } from '../../core/abrigo.service';
import { Abrigo } from '../../core/abrigo.model';

@Component({
  selector: 'cw-abrigo-ajuda-detalhe',
  templateUrl: './abrigo-ajuda-detalhe.component.html',
  styleUrl: './abrigo-ajuda-detalhe.component.scss'
})
export class AbrigoAjudaDetalheComponent {

  abrigoId: number;
  carregando = true;
  abrigo!: Abrigo;
  constructor(
    private service: AbrigoService,
    public dialogRef: MatDialogRef<AbrigoAjudaDetalheComponent>,
    @Inject(MAT_DIALOG_DATA) params: any
  ) {
    this.abrigoId = params.abrigoId;

    service.getById(this.abrigoId).subscribe(abrigo => {
      this.abrigo = abrigo;
      this.carregando = false;
    });
  }

  obterNome(): string {
    return (this.abrigo as any)?.nome;
  }
}
