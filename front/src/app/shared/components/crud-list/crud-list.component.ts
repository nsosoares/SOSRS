import { Component, EventEmitter, Output, input } from '@angular/core';
import { CrudListParams } from './crud-list.params';
import { STARTER_ANIMATIONS } from '../../animations/on-show.animations';
import { IEntity } from '../../../core/entities/i-entity';
import { INamedEntity } from '../../../core/entities/i-named-entity';

@Component({
  selector: 'cw-crud-list',
  templateUrl: './crud-list.component.html',
  styleUrl: './crud-list.component.scss',
  animations: [STARTER_ANIMATIONS]
})
export class CrudListComponent {

  @Output() onEdit = new EventEmitter<IEntity>();
  @Output() onRemove = new EventEmitter<INamedEntity>();
  params = input<CrudListParams>();
}
