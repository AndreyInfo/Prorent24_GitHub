import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "enumToArray" })
export class EnumToArrayPipe implements PipeTransform {
  transform(data: any): any {
    const keys: any = Object.keys(data);
    return keys.slice(keys.length / 2);
  }
}
