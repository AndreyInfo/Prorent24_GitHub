import { ProjectFunctionType } from "@shared/enums/project-function-type.enum";

export class ProjectPlanningFilter {
  type: ProjectFunctionType = ProjectFunctionType.crew;
  functionGroupId?: number;
}
