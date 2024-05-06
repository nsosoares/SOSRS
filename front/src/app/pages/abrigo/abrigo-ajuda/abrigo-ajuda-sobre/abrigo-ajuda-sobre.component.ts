import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'cw-abrigo-ajuda-sobre',
  templateUrl: './abrigo-ajuda-sobre.component.html',
  styleUrl: './abrigo-ajuda-sobre.component.scss'
})
export class AbrigoAjudaSobreComponent {

  constructor(public dialogRef: MatDialogRef<AbrigoAjudaSobreComponent>) { }
  fechar(): void {
    this.dialogRef.close();
  }
}
