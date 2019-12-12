var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
function BetterFetch(url, options) {
    return __awaiter(this, void 0, void 0, function () {
        return __generator(this, function (_a) {
            if (options == null)
                options = {};
            if (options.credentials == null)
                options.credentials = 'same-origin';
            return [2, new Promise(function (resolve, reject) {
                    return __awaiter(this, void 0, void 0, function () {
                        var response, e_1;
                        return __generator(this, function (_a) {
                            switch (_a.label) {
                                case 0:
                                    _a.trys.push([0, 2, , 3]);
                                    return [4, fetch(url, options)];
                                case 1:
                                    response = _a.sent();
                                    if (response.status >= 200 && response.status < 300) {
                                        resolve(response);
                                    }
                                    else
                                        reject(new Error("HTTP " + response.status));
                                    return [3, 3];
                                case 2:
                                    e_1 = _a.sent();
                                    reject(e_1);
                                    return [3, 3];
                                case 3: return [2];
                            }
                        });
                    });
                })];
        });
    });
}
function test() {
    return __awaiter(this, void 0, void 0, function () {
        return __generator(this, function (_a) {
            console.log("Main load !");
            console.log("Main load success !");
            return [2];
        });
    });
}
function main() {
    return __awaiter(this, void 0, void 0, function () {
        var foo, bar, ex_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("Main !");
                    return [4, ScriptLoader.loadPolyfill("/ts/date.js")];
                case 1:
                    _a.sent();
                    console.log("date loaded !");
                    console.log("date loaded2 !");
                    console.log("foo", $);
                    console.log($.datepicker.formatDate("ddMMyyyy", new Date()));
                    console.log("picked !");
                    return [4, test()];
                case 2:
                    _a.sent();
                    _a.label = 3;
                case 3:
                    _a.trys.push([3, 6, , 7]);
                    console.log("before exists");
                    return [4, BetterFetch("index.htm")];
                case 4:
                    foo = _a.sent();
                    console.log("after exists");
                    console.log(foo);
                    console.log("before not exists");
                    return [4, BetterFetch("index1.htm")];
                case 5:
                    bar = _a.sent();
                    console.log("after not exists");
                    console.log(bar);
                    return [3, 7];
                case 6:
                    ex_1 = _a.sent();
                    console.log("ex", ex_1.message);
                    return [3, 7];
                case 7: return [2];
            }
        });
    });
}
main();
