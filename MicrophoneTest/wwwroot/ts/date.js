var Basic;
(function (Basic) {
    var Waiting;
    (function (Waiting) {
        function Start() {
            Portal.Waiting.Start();
        }
        Waiting.Start = Start;
        function Stop() {
            Portal.Waiting.Stop();
        }
        Waiting.Stop = Stop;
        function StartAndStop() {
            Portal.Waiting.StartAndStop();
        }
        Waiting.StartAndStop = StartAndStop;
    })(Waiting = Basic.Waiting || (Basic.Waiting = {}));
})(Basic || (Basic = {}));
var $;
(function ($) {
    var datepicker;
    (function (datepicker) {
        datepicker.localesz = {
            "de": {
                dayNames: [
                    "So", "Mo", "Di", "Mi", "Do", "Fr", "Sa",
                    "Sonntag", "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag"
                ],
                monthNames: [
                    "Jan", "Feb", "Mär", "Apr", "Mai", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dez",
                    "Januar", "Februar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember"
                ],
                ordinal_suffix_of: function (i, with_num) {
                    return with_num ? (i + ".") : ".";
                }
            },
            "en": {
                dayNames: [
                    "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
                    "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
                ],
                monthNames: [
                    "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
                ],
                ordinal_suffix_of: function (i, with_num) {
                    var j = i % 10, k = i % 100;
                    with_num = with_num || false;
                    if (j == 1 && k != 11) {
                        return with_num ? (i + "st") : "st";
                    }
                    if (j == 2 && k != 12) {
                        return with_num ? (i + "nd") : "nd";
                    }
                    if (j == 3 && k != 13) {
                        return with_num ? (i + "rd") : "rd";
                    }
                    return with_num ? (i + "th") : "th";
                }
            },
            "fr": {
                dayNames: [
                    "Dim.", "Lun.", "Mar.", "Mer.", "Jeu.", "Ven.", "Sam.",
                    "Dimanche", "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi"
                ],
                monthNames: [
                    "Janv.", "Févr.", "Mars", "Avr.", "Mai", "Juin", "Juil.", "Août", "Sept.", "Oct.", "Nov.", "Déc.",
                    "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"
                ],
                ordinal_suffix_of: function (i, with_num) {
                    if (i == 1)
                        return with_num ? (i + "er") : "er";
                    return with_num ? (i + "e") : "e";
                }
            },
            "it": {
                dayNames: [
                    "Dom", "Lun", "Mar", "Mer", "Gio", "Ven", "Sab",
                    "Domenica", "Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato"
                ],
                monthNames: [
                    "Gen", "Feb", "Mar", "Apr", "Mag", "Giu", "Lug", "Ago", "Set", "Ott", "Nov", "Dic",
                    "Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"
                ],
                ordinal_suffix_of: function (i, with_num) {
                    return with_num ? (i + ".") : ".";
                }
            },
            "ru": {
                dayNames: [
                    "Dum", "Gli", "Mar", "Mes", "Gie", "Ven", "Son",
                    "Dumengia", "Glindesdi", "Mardi", "Mesemna", "Gievgia", "Venderdi", "Sonda"
                ],
                monthNames: [
                    "Sch", "Fav", "Mar", "Avr", "Mat", "Zer", "Fan", "Avu", "Set", "Oct", "Nov", "Dec",
                    "Schaner", "Favrer", "Mars", "Avrigl", "Matg", "Zercladur", "Fanadur", "Avust", "Settember", "October", "November", "December"
                ],
                ordinal_suffix_of: function (i, with_num) {
                    return with_num ? (i + ".") : ".";
                }
            }
        };
    })(datepicker = $.datepicker || ($.datepicker = {}));
})($ || ($ = {}));
(function ($) {
    var datepicker;
    (function (datepicker) {
        var version = 1234;
        var getLocale = (function () {
            var static_locales = {};
            function getMonthName(locale, month, short) {
                month = Number(month);
                var day = new Date(2019, month, 1, 0, 0, 0, 0);
                if (short)
                    return day.toLocaleDateString(locale, { month: 'short' });
                return day.toLocaleDateString(locale, { month: 'long' });
            }
            function getWeekDayName(locale, weekday, short) {
                weekday = Number(weekday);
                var day = new Date(2019, 10, 17 + weekday, 0, 0, 0, 0);
                if (short)
                    return day.toLocaleDateString(locale, { weekday: 'short' });
                return day.toLocaleDateString(locale, { weekday: 'long' });
            }
            return function (loc) {
                if (static_locales[loc])
                    return static_locales[loc];
                var days = [];
                var months = [];
                for (var i = 0; i < 7; ++i) {
                    days.push(getWeekDayName(loc, i, true));
                }
                for (var i = 0; i < 7; ++i) {
                    days.push(getWeekDayName(loc, i));
                }
                for (var i = 0; i < 12; ++i) {
                    months.push(getMonthName(loc, i, true));
                }
                for (var i = 0; i < 12; ++i) {
                    months.push(getMonthName(loc, i));
                }
                static_locales[loc] = {
                    dayNames: days,
                    monthNames: months,
                    ordinal_suffix_of: function (i) { return "."; }
                };
                return static_locales[loc];
            };
        })();
        var masks = {
            "default": "ddd mmm dd yyyy HH:MM:ss",
            shortDate: "m/d/yy",
            mediumDate: "mmm d, yyyy",
            longDate: "mmmm d, yyyy",
            fullDate: "dddd, mmmm d, yyyy",
            shortTime: "h:MM TT",
            mediumTime: "h:MM:ss TT",
            longTime: "h:MM:ss TT Z",
            isoDate: "yyyy-mm-dd",
            isoTime: "HH:MM:ss",
            isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
            isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
        };
        function pad(val, len) {
            val = String(val);
            len = len || 2;
            while (val.length < len)
                val = "0" + val;
            return val;
        }
        var token = /d{1,4}|M{1,4}|yy(?:yy)?|f{1,3}|([HhmsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g, timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g, timezoneClip = /[^-+\dA-Z]/g;
        function dateFormat(date, mask, utc) {
            var i18n = getLocale("de");
            if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
                mask = date;
                date = undefined;
            }
            date = date ? new Date(date) : new Date;
            if (isNaN(date))
                throw SyntaxError("invalid date");
            mask = String(masks[mask] || mask || masks["default"]);
            if (mask.slice(0, 4) == "UTC:") {
                mask = mask.slice(4);
                utc = true;
            }
            var _ = utc ? "getUTC" : "get", d = date[_ + "Date"](), D = date[_ + "Day"](), M = date[_ + "Month"](), y = date[_ + "FullYear"](), H = date[_ + "Hours"](), m = date[_ + "Minutes"](), s = date[_ + "Seconds"](), L = date[_ + "Milliseconds"](), o = utc ? 0 : date.getTimezoneOffset(), flags = {
                d: d,
                dd: pad(d),
                ddd: i18n.dayNames[D],
                dddd: i18n.dayNames[D + 7],
                M: M + 1,
                MM: pad(M + 1),
                MMM: i18n.monthNames[M],
                MMMM: i18n.monthNames[M + 12],
                yy: String(y).slice(2),
                yyyy: y,
                h: H % 12 || 12,
                hh: pad(H % 12 || 12),
                H: H,
                HH: pad(H),
                m: m,
                mm: pad(m),
                s: s,
                ss: pad(s),
                f: Math.floor(L / 100),
                ff: pad(Math.floor(L / 10), 2),
                fff: pad(L, 3),
                l: pad(L, 3),
                L: pad(L > 99 ? Math.round(L / 10) : L),
                t: H < 12 ? "a" : "p",
                tt: H < 12 ? "am" : "pm",
                T: H < 12 ? "A" : "P",
                TT: H < 12 ? "AM" : "PM",
                Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                S: i18n.ordinal_suffix_of(d)
            };
            var result = mask.replace(token, function ($0) {
                return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
            });
            return result.split("\b").join("'");
        }
        function formatDate(format, date) {
            return dateFormat(date, format, false);
        }
        datepicker.formatDate = formatDate;
    })(datepicker = $.datepicker || ($.datepicker = {}));
})($ || ($ = {}));
