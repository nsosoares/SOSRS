import { Component } from '@angular/core';
import { ControlCvaProvider } from '../../../shared/components/control-value-accessor/control-cva/control-cva.provider';
import { InputTextCvaParams } from '../../../shared/components/control-value-accessor/children/input-text-cva/input-text-cva.params';
import { ComboboxCvaParams } from '../../../shared/components/control-value-accessor/children/combobox-cva/combobox-cva.params';
import { PropertyService } from './property.service';
import { CrudParams } from '../../../shared/components/crud/crud.params';
import { CrudListColumn, CrudListParams } from '../../../shared/components/crud-list/crud-list.params';

@Component({
  selector: 'cw-property',
  templateUrl: './property.component.html',
  styleUrl: './property.component.scss'
})
export class PropertyComponent {

  params: CrudParams;

  controls: ControlCvaProvider<any,any>[] =  [];
  idControl = ControlCvaProvider.inputText(() => InputTextCvaParams.hidden('id'));
  nameControl = ControlCvaProvider.inputText(() => InputTextCvaParams.text('name', 'Name', 50, 5).asRequired().withPlaceholder('Digite o name do imóvel'));
  subnameControl = ControlCvaProvider.combobox(() => new ComboboxCvaParams('subname', 'Imóvel', this.imovelService.getById, this.imovelService.searchByName).asRequired());

  constructor(private imovelService: PropertyService) {
    console.log(navigator.geolocation.getCurrentPosition((position) => {
      console.log(position);
    }
      ));
    this.controls = [this.idControl, this.nameControl, this.subnameControl];

    const listParams = new CrudListParams([new CrudListColumn('id', 'Id'),new CrudListColumn('name', 'Name')]);
    this.params = new CrudParams('Imovel', listParams, this.controls, imovelService.searchByName, imovelService.getById, imovelService.create, imovelService.update, imovelService.delete);

    setTimeout(() => {
      this.imovelService.searchByName('').subscribe(entities => listParams.setEntites(entities));
    }, 1000);

  }
}
