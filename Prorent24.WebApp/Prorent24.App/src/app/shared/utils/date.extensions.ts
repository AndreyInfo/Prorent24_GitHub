export {};

declare global {
  interface Date {
    addDays(days: number, useThis?: boolean): Date;
    isToday(): boolean;
    clone(): Date;
    isAnotherMonth(date: Date): boolean;
    isWeekend(): boolean;
    isSameDate(date: Date): boolean;
    getStringDate(): String;
  }
}

Date.prototype.addDays = function(days: number): Date {
  if (!days) {
    return this;
  }
  let date: Date = this;
  date.setDate(date.getDate() + days);

  return date;
};

Date.prototype.isToday = function(): boolean {
  let today: Date = new Date();
  return this.isSameDate(today);
};

Date.prototype.clone = function(): Date {
  return new Date(+this);
};

Date.prototype.isAnotherMonth = function(date: Date): boolean {
  return date && this.getMonth() !== date.getMonth();
};

Date.prototype.isWeekend = function(): boolean {
  return this.getDay() === 0 || this.getDay() === 6;
};

Date.prototype.isSameDate = function(date: Date): boolean {
  return (
    date &&
    this.getFullYear() === date.getFullYear() &&
    this.getMonth() === date.getMonth() &&
    this.getDate() === date.getDate()
  );
};

Date.prototype.getStringDate = function(
  locale: string = "ru-ru",
  overrides: any = {}
): String {
  let terms: any = Object.assign(
    overrides
  );
  let today: Date = new Date();
  let monthName: any = new Intl.DateTimeFormat(locale, { month: "long" }).format(
    this
  );
  if (this.getMonth() === today.getMonth() && this.getDay() === today.getDay()) {
    return terms.today;
  } else if (
    this.getMonth() === today.getMonth() &&
    this.getDay() === today.getDay() + 1
  ) {
    return terms.tomorrow;
  } else if (
    this.getMonth() === today.getMonth() &&
    this.getDay() === today.getDay() - 1
  ) {
    return terms.yesterday;
  } else {
    return `${this.getDay()} ${terms.conective} ${monthName} ${
      terms.conective
    } ${this.getFullYear()}`;
  }
};
