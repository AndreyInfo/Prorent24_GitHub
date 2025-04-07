export class StringExt {
  public static Empty: string = "";

  public static isNullOrWhiteSpace(value: string): boolean {
    try {
      if (value === null || value === "undefined") {
        return false;
      }

      return value.replace(/\s/g, "").length < 1;
    } catch (e) {
      return false;
    }
  }

  public static Format(value: any, ...args): string {
    try {
      return value.replace(/{(\d+(:.*)?)}/g, function(match, i) {
        var s = match.split(":");
        if (s.length > 1) {
          i = i[0];
          match = s[1].replace("}", "");
        }

        var arg = StringExt.formatPattern(match, args[i]);
        return typeof arg != "undefined" && arg != null ? arg : "";
      });
    } catch (e) {
      return "";
    }
  }

  private static formatPattern(match: any, arg: any): string {
    switch (match) {
      case "L":
        arg = arg.toLowerCase();
        break;
      case "U":
        arg = arg.toUpperCase();
        break;
      default:
        break;
    }

    return arg;
  }

  public static changeSymbolFromRusToEn(value: any): string {
    try {
      var replace = new Array(
        "й",
        "ц",
        "у",
        "к",
        "е",
        "н",
        "г",
        "ш",
        "щ",
        "з",
        "х",
        "ъ",
        "ф",
        "ы",
        "в",
        "а",
        "п",
        "р",
        "о",
        "л",
        "д",
        "ж",
        "э",
        "я",
        "ч",
        "с",
        "м",
        "и",
        "т",
        "ь",
        "б",
        "ю"
      );
      var search = new Array(
        "q",
        "w",
        "e",
        "r",
        "t",
        "y",
        "u",
        "i",
        "o",
        "p",
        "\\[",
        "\\]",
        "a",
        "s",
        "d",
        "f",
        "g",
        "h",
        "j",
        "k",
        "l",
        ";",
        "'",
        "z",
        "x",
        "c",
        "v",
        "b",
        "n",
        "m",
        ",",
        "\\."
      );

      for (var i = 0; i < replace.length; i++) {
        var reg = new RegExp(replace[i], "mig");
        value = value.replace(reg, function(a) {
          return a == a.toLowerCase() ? search[i] : search[i].toUpperCase();
        });
      }
      return value;
    } catch (e) {
      return "";
    }
  }
}
