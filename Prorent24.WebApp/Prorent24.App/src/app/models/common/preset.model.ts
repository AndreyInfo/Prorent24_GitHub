import { Entity } from "@shared/enums/entity.enum";

export class PresetModel {
  id: any;
  name: string;
  isDefault: boolean;
  moduleType: Entity;
  filters: Array<any>;
}
