import { RESPONSIVE_SIZE_12 } from '../../../../../core/constants/app_constants';
import { ControlCvaProvider } from '../../control-cva/control-cva.provider';
import { BaseControlCvaParams } from '../../control-cva/domain/base-control-cva.params';

export class SubformsCvaParams extends BaseControlCvaParams {
  constructor(controlName: string, label: string, readonly controlsProvider: ControlCvaProvider<any,any>[]) {
    super(controlName, label);
    this.withCssClass(RESPONSIVE_SIZE_12);
  }
}
