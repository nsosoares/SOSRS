import { NgModule } from '@angular/core';

import { CoreModule } from '../../core/core.module';
import { SharedModule } from '../../shared/shared.module';
import { AbrigoRoutingModule } from './abrigo-routing.module';
import { AbrigoComponent } from './abrigo.component';
import { AbrigoAjudaComponent } from './abrigo-ajuda/abrigo-ajuda.component';
import { AbrigoAjudaPesquisaAvancadaComponent } from './abrigo-ajuda/abrigo-ajuda-pesquisa-avancada/abrigo-ajuda-pesquisa-avancada.component';
import { AbrigoAjudaSobreComponent } from './abrigo-ajuda/abrigo-ajuda-sobre/abrigo-ajuda-sobre.component';
import { AbrigoAjudaDetalheComponent } from './abrigo-ajuda/abrigo-ajuda-detalhe/abrigo-ajuda-detalhe.component';



@NgModule({
  declarations: [
    AbrigoComponent,
    AbrigoAjudaComponent,
    AbrigoAjudaPesquisaAvancadaComponent,
    AbrigoAjudaSobreComponent,
    AbrigoAjudaDetalheComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    AbrigoRoutingModule,
  ]
})
export class AbrigoModule { }
