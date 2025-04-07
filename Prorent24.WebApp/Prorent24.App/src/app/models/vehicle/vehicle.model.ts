import { BaseModel } from "@models/base.model";
import { LenghtUnit } from "@shared/enums/lenght-unit.enum";
import { WeightUnit } from "@shared/enums/weight-unit.enum";

export class VehicleModel extends BaseModel {
  name: string;
  registrationNumber: string;
  folderId: number;
  folderName: string;
  deployedMultipleTimesSimultaneously: boolean;
  motDate: Date = null;
  insuranceDate: Date = null;
  dayilCosts: number;
  variableCosts: number;
    displayInPlanner: boolean;

    loadingSurface: number;

  seats: number;
  description: string;
  isPublic: boolean = true;
  crewMembers: Array<string>;

  loadCapacity: number;
  loadCapacityUnit: WeightUnit;

  length: number;
  width: number;
  height: number;
  lengthUnit: LenghtUnit;
  widthUnit: LenghtUnit;
  heightUnit: LenghtUnit;
}
