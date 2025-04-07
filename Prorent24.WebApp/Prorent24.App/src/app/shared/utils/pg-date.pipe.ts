import { Pipe, PipeTransform } from "@angular/core";
import * as moment from "moment";

@Pipe({ name: "pgDate" })
export class pgDatePipe implements PipeTransform {
  transform(value: Date | number | string, formatString: string): string {
    if (moment(value).isValid()) {
      var stillUtc = moment.utc(value).toDate();
      return moment(stillUtc).local().format(formatString);
    } else {
      return "";
    }
  }
}
