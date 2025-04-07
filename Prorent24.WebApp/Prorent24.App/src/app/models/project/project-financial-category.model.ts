import { ProjectFinancialCategoryType } from "@shared/enums/project-financial-category-type.enum";

export class ProjectFinancialCategoryModel {
  id: number;
  projectId: number;
  category: ProjectFinancialCategoryType;
  name: string;
  parentId?: number;
  estimatedCosts: number = 0;
  plannedCosts: number = 0;
  actualCosts: number = 0;
  revenue: number = 0;
  discount: number = 0;
  profit: number = 0;
  total: number = 0;
  percentTotal: number = 0;
}
