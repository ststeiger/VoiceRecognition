using System.Runtime.InteropServices;

namespace Microsoft.JScript
{
    /// <summary>Gibt die JScript-Fehlermeldungen an.Diese Klasse gehört zur Kategorie für Kompilierungs- und Laufzeitzustände.</summary>
    [Guid("268CA962-2FEF-3152-BA46-E18658B7FA4F")]
    [ComVisible(true)]
    public enum JSError
    {
        /// <summary>Kein Fehler.Dies ist der Standardwert (0) für die Enumeration.</summary>
        NoError = 0,
        /// <summary>Ungültiger Prozeduraufruf oder ungültiges Argument.</summary>
        InvalidCall = 5,
        /// <summary>Nicht genügend Arbeitsspeicher.</summary>
        OutOfMemory = 7,
        /// <summary>Typenkonflikt.</summary>
        TypeMismatch = 13,
        /// <summary>Nicht genügend Stapelspeicher.</summary>
        OutOfStack = 28,
        /// <summary>Ein interner Fehler wurde ausgelöst.</summary>
        InternalError = 51,
        /// <summary>Datei nicht gefunden.</summary>
        FileNotFound = 53,
        /// <summary>Ein Objekt wird benötigt.</summary>
        NeedObject = 424,
        /// <summary>Objekt kann nicht erstellt werden.</summary>
        CantCreateObject = 429,
        /// <summary>Das Objekt unterstützt diese Eigenschaft oder die Methode nicht.</summary>
        OLENoPropOrMethod = 438,
        /// <summary>Das Objekt unterstützt diese Aktion nicht.</summary>
        ActionNotSupported = 445,
        /// <summary>Dieses Objekt ist keine Auflistung.</summary>
        NotCollection = 451,
        /// <summary>Syntaxfehler ermittelt.</summary>
        SyntaxError = 1002,
        /// <summary>":" erwartet.</summary>
        NoColon = 1003,
        /// <summary>";" erwartet.</summary>
        NoSemicolon = 1004,
        /// <summary>"(" erwartet.</summary>
        NoLeftParen = 1005,
        /// <summary>")" erwartet.</summary>
        NoRightParen = 1006,
        /// <summary>"]" erwartet.</summary>
        NoRightBracket = 1007,
        /// <summary>"{" erwartet.</summary>
        NoLeftCurly = 1008,
        /// <summary>"}" erwartet.</summary>
        NoRightCurly = 1009,
        /// <summary>Bezeichner erwartet.</summary>
        NoIdentifier = 1010,
        /// <summary>"=" erwartet.</summary>
        NoEqual = 1011,
        /// <summary>Ungültiges Zeichen.</summary>
        IllegalChar = 1014,
        /// <summary>Die Zeichenfolgenkonstante wurde nicht beendet.</summary>
        UnterminatedString = 1015,
        /// <summary>Kommentar nicht beendet.</summary>
        NoCommentEnd = 1016,
        /// <summary>Die Rückgabeanweisung kann nicht außerhalb der Funktion stehen.</summary>
        BadReturn = 1018,
        /// <summary>Außerhalb der Schleife darf break nicht vorkommen.</summary>
        BadBreak = 1019,
        /// <summary>Außerhalb der Schleife darf continue nicht vorkommen.</summary>
        BadContinue = 1020,
        /// <summary>Hexadezimalziffer erwartet.</summary>
        BadHexDigit = 0x3FF,
        /// <summary>while erwartet.</summary>
        NoWhile = 0x400,
        /// <summary>Es ist bereits eine Bezeichnung mit diesem Namen vorhanden.</summary>
        BadLabel = 1025,
        /// <summary>Bezeichnung nicht gefunden.</summary>
        NoLabel = 1026,
        /// <summary>default darf in einer switch-Anweisung nur einmal angegeben werden.</summary>
        DupDefault = 1027,
        /// <summary>Bezeichner oder string erwartet.</summary>
        NoMemberIdentifier = 1028,
        /// <summary>@end erwartet.</summary>
        NoCcEnd = 1029,
        /// <summary>Die bedingte Kompilierung ist ausgeschaltet.</summary>
        CcOff = 1030,
        /// <summary>Konstante erwartet.</summary>
        NotConst = 1031,
        /// <summary>@ erwartet.</summary>
        NoAt = 1032,
        /// <summary>catch erwartet.</summary>
        NoCatch = 1033,
        /// <summary>else ohne Entsprechung, da kein if definiert ist.</summary>
        InvalidElse = 1034,
        /// <summary>"," erwartet.</summary>
        NoComma = 1100,
        /// <summary>Der Sichtbarkeitsmodifizierer ist bereits definiert.</summary>
        DupVisibility = 1101,
        /// <summary>Ungültiger Sichtbarkeitsmodifizierer.</summary>
        IllegalVisibility = 1102,
        /// <summary>Fehlende case-Anweisung oder fehlende default-Anweisung.</summary>
        BadSwitch = 1103,
        /// <summary>@end ohne Entsprechung, da kein @if definiert ist.</summary>
        CcInvalidEnd = 1104,
        /// <summary>@else ohne Entsprechung, da kein @if definiert ist.</summary>
        CcInvalidElse = 1105,
        /// <summary>@elif ohne Entsprechung, da kein @if definiert ist.</summary>
        CcInvalidElif = 1106,
        /// <summary>Weitere Quellzeichen erwartet.</summary>
        ErrEOF = 1107,
        /// <summary>Nicht kompatibler Sichtbarkeitsmodifizierer.</summary>
        IncompatibleVisibility = 1108,
        /// <summary>In diesem Kontext nicht zugelassene Klassendefinition.</summary>
        ClassNotAllowed = 1109,
        /// <summary>Ein Ausdruck muss eine Kompilierzeitkonstante sein.</summary>
        NeedCompileTimeConstant = 1110,
        /// <summary>Bezeichner wird bereits verwendet.</summary>
        DuplicateName = 1111,
        /// <summary>Ein Typname wird erwartet.</summary>
        NeedType = 1112,
        /// <summary>Nur innerhalb einer Klassendefinition gültig.</summary>
        NotInsideClass = 1113,
        /// <summary>Eine unbekannte Positionsdirektive wurde ermittelt.</summary>
        InvalidPositionDirective = 1114,
        /// <summary>Nach der Direktive darf auf derselben Zeile kein Code stehen.</summary>
        MustBeEOL = 1115,
        /// <summary>Falsche Debuggerdirektive oder falsche Position für die Direktive.</summary>
        WrongDirective = 1118,
        /// <summary>Die Positionsdirektive muss beendet werden, bevor eine neue gestartet werden kann.</summary>
        CannotNestPositionDirective = 1119,
        /// <summary>Zirkuläre Definition.</summary>
        CircularDefinition = 1120,
        /// <summary>Der angegebene Typ ist veraltet.</summary>
        Deprecated = 1121,
        /// <summary>Die Verwendung von this im aktuellen Kontext ist ungültig.</summary>
        IllegalUseOfThis = 1122,
        /// <summary>Auf das Objekt oder den Member kann von diesem Bereich aus nicht zugegriffen werden.</summary>
        NotAccessible = 1123,
        /// <summary>Nur Konstruktorfunktionen können den gleichen Namen wie die Klasse besitzen, der sie entstammen.</summary>
        CannotUseNameOfClass = 1124,
        /// <summary>Die Klasse muss eine Implementierung der Methode bereitstellen.</summary>
        MustImplementMethod = 1128,
        /// <summary>Ein Schnittstellenname wurde erwartet.</summary>
        NeedInterface = 1129,
        /// <summary>Die Catch-Klausel ist nicht erreichbar.</summary>
        UnreachableCatch = 1133,
        /// <summary>Der Typ kann nicht erweitert werden.</summary>
        TypeCannotBeExtended = 1134,
        /// <summary>Eine Variable wurde nicht deklariert.</summary>
        UndeclaredVariable = 1135,
        /// <summary>Nicht initialisierte Variablen sind gefährlich und in der Verwendung sehr langsam.Wollten Sie die Variable nicht initialisieren?</summary>
        VariableLeftUninitialized = 1136,
        /// <summary>Sie können kein reserviertes Wort als Bezeichner verwenden.</summary>
        KeywordUsedAsIdentifier = 1137,
        /// <summary>Objekt oder Member ist in einem Aufruf des Basisklassenkonstruktors nicht zulässig.</summary>
        NotAllowedInSuperConstructorCall = 1140,
        /// <summary>Sie können diese Methode nicht direkt aufrufen.Verwenden Sie stattdessen nach Möglichkeit Eigenschaftenaccessoren.</summary>
        NotMeantToBeCalledDirectly = 1141,
        /// <summary>Die get-Methode und die set-Methode dieser Eigenschaft stimmen nicht überein.</summary>
        GetAndSetAreInconsistent = 1142,
        /// <summary>Eine benutzerdefinierte Attributklasse muss von <see cref="T:System.Attribute" /> abgeleitet werden.</summary>
        InvalidCustomAttribute = 1143,
        /// <summary>Nur primitive Typen werden in einer benutzerdefinierten Attributkonstruktorargumente-Liste zugelassen.</summary>
        InvalidCustomAttributeArgument = 1144,
        /// <summary>Unbekannte benutzerdefinierte Attributklasse oder unbekannter Konstruktor.</summary>
        InvalidCustomAttributeClassOrCtor = 1146,
        /// <summary>Es sind zu viele tatsächliche Parameter vorhanden.Die überzähligen Parameter werden ignoriert.</summary>
        TooManyParameters = 1148,
        /// <summary>Durch die <see cref="T:Microsoft.JScript.With" />-Anweisung wurde die Verwendung dieses Namens mehrdeutig.</summary>
        AmbiguousBindingBecauseOfWith = 1149,
        /// <summary>Durch das Vorhandensein von <see cref="T:Microsoft.JScript.Eval" /> wurde die Verwendung dieses Namens mehrdeutig.</summary>
        AmbiguousBindingBecauseOfEval = 1150,
        /// <summary>Objekte dieses Typs haben keinen solchen Member.</summary>
        NoSuchMember = 1151,
        /// <summary>Das Eigenschaftenelement kann nicht für eine <see cref="T:Microsoft.JScript.Expando" />-Klasse definiert werden.Dieses Element ist für die <see cref="T:Microsoft.JScript.Expando" />-Felder reserviert.</summary>
        ItemNotAllowedOnExpandoClass = 1152,
        /// <summary>Die Eigenschaft mit dem Namen Item kann für eine <see cref="T:Microsoft.JScript.Expando" />-Klasse nicht definiert werden.</summary>
        MethodNotAllowedOnExpandoClass = 1153,
        /// <summary>Die <see cref="T:Microsoft.JScript.Expando" />-Klasse kann nicht erstellt werden, da in der Klassenhierarchie bereits eine Eigenschaft mit dem Namen Item definiert ist.</summary>
        MethodClashOnExpandoSuperClass = 1155,
        /// <summary>Eine Basisklasse ist bereits als <see cref="T:Microsoft.JScript.Expando" /> markiert. Die aktuelle Angabe wird ignoriert.</summary>
        BaseClassIsExpandoAlready = 1156,
        /// <summary>Eine abstrakte Methode kann nicht privat sein.</summary>
        AbstractCannotBePrivate = 1157,
        /// <summary>Objekte dieses Typs sind nicht indizierbar.</summary>
        NotIndexable = 1158,
        /// <summary>Statische Initialisierer müssen das static-Schlüsselwort angeben.</summary>
        StaticMissingInStaticInit = 1159,
        /// <summary>Die Liste der Attribute gilt nicht für den aktuellen Kontext.</summary>
        MissingConstructForAttributes = 1160,
        /// <summary>In einem package sind nur Klassen zulässig.</summary>
        OnlyClassesAllowed = 1161,
        /// <summary>
        ///   <see cref="T:Microsoft.JScript.Expando" />-Klassen dürfen <see cref="T:System.Collections.IEnumerable" /> nicht implementieren.Die Schnittstelle ist implizit für <see cref="T:Microsoft.JScript.Expando" />-Klassen definiert.</summary>
        ExpandoClassShouldNotImpleEnumerable = 1162,
        /// <summary>Der angegebene Member ist nicht CLS-kompatibel.</summary>
        NonCLSCompliantMember = 1163,
        /// <summary>Das Objekt oder der Member kann nicht gelöscht werden.</summary>
        NotDeletable = 1164,
        /// <summary>Paketname erwartet.</summary>
        PackageExpected = 1165,
        /// <summary>Der Ausdruck hat keine Auswirkungen.</summary>
        UselessExpression = 1169,
        /// <summary>Die base-Klasse enthält bereits einen Member dieses Namens.</summary>
        HidesParentMember = 1170,
        /// <summary>Die Sichtbarkeitsangabe einer Basismethode kann nicht geändert werden.</summary>
        CannotChangeVisibility = 1171,
        /// <summary>Diese Methode blendet abstract in einer base-Klasse aus.</summary>
        HidesAbstractInBase = 1172,
        /// <summary>Eine Methode stimmt mit einer Methode in einer Basisklasse überein.Muss override oder hide angeben.</summary>
        NewNotSpecifiedInMethodDeclaration = 1173,
        /// <summary>Eine Methode in einer Basisklasse, die final oder nicht virtualoverride ist, wird ignoriert.Geben Sie hide an.</summary>
        MethodInBaseIsNotVirtual = 1174,
        /// <summary>In einer Basisklasse gibt es keine Member, auf die hide angewendet werden kann.</summary>
        NoMethodInBaseToNew = 1175,
        /// <summary>Diese Methode in einer Basisklasse hat einen anderen Rückgabetyp.</summary>
        DifferentReturnTypeFromBase = 1176,
        /// <summary>Der Name des Felds steht in Konflikt mit dem Namen der Eigenschaft.</summary>
        ClashWithProperty = 1177,
        /// <summary>Die Schlüsselwörter override und hide können nicht zusammen in einer Memberdeklaration verwendet werden.</summary>
        OverrideAndHideUsedTogether = 1178,
        /// <summary>Es muss entweder die Sprachoption "fast" oder "versionSafe" angegeben werden.</summary>
        InvalidLanguageOption = 1179,
        /// <summary>In einer Basisklasse gibt es keine Member, auf die override angewendet werden kann.</summary>
        NoMethodInBaseToOverride = 1180,
        /// <summary>Nicht zulässig für einen Konstruktor.</summary>
        NotValidForConstructor = 1181,
        /// <summary>Von einem Konstruktor oder einer Void-Funktion kann kein Wert zurückgegeben werden.</summary>
        CannotReturnValueFromVoidFunction = 1182,
        /// <summary>Mit dieser Parameterliste stimmen mehrere Methoden oder Eigenschaften überein.</summary>
        AmbiguousMatch = 1183,
        /// <summary>Mit dieser Parameterliste stimmen mehrere Konstruktoren überein.</summary>
        AmbiguousConstructorCall = 1184,
        /// <summary>Aus diesem Bereich kann nicht auf den Basisklassenkonstruktor zugegriffen werden.</summary>
        SuperClassConstructorNotAccessible = 1185,
        /// <summary>Die oktalen Literale sind veraltet.</summary>
        OctalLiteralsAreDeprecated = 1186,
        /// <summary>Die Variable ist eventuell nicht initialisiert.</summary>
        VariableMightBeUnitialized = 1187,
        /// <summary>An diesem Speicherort darf kein Basisklassenkonstruktor aufgerufen werden.</summary>
        NotOKToCallSuper = 1188,
        /// <summary>Diese Verwendung der Basisklasse ist ungültig.</summary>
        IllegalUseOfSuper = 1189,
        /// <summary>finally-Blöcke in dieser Form sind langsam und problematisch.</summary>
        BadWayToLeaveFinally = 1190,
        /// <summary>"," erwartet, oder ungültige Typdeklaration; schreiben Sie &lt;Bezeichner&gt; : &lt;Typ&gt;", nicht "&lt;Typ&gt; &lt;Bezeichner&gt;."</summary>
        NoCommaOrTypeDefinitionError = 1191,
        /// <summary>Abstrakte Funktionen können keinen Text enthalten.</summary>
        AbstractWithBody = 1192,
        /// <summary>"," oder ")" erwartet.</summary>
        NoRightParenOrComma = 1193,
        /// <summary>"," oder "] " erwartet.</summary>
        NoRightBracketOrComma = 1194,
        /// <summary>Ausdruck erwartet.</summary>
        ExpressionExpected = 1195,
        /// <summary>Unerwartetes Zeichen ";".</summary>
        UnexpectedSemicolon = 1196,
        /// <summary>Zu viele Fehler.Die Datei ist möglicherweise keine JScript-Datei.</summary>
        TooManyTokensSkipped = 1197,
        /// <summary>Mögliche ungültige Variablendeklaration, var fehlt oder nicht erkannter Syntaxfehler.</summary>
        BadVariableDeclaration = 1198,
        /// <summary>Mögliche ungültige Funktionsdeklaration, fehlende Funktion oder nicht erkannter Syntaxfehler.</summary>
        BadFunctionDeclaration = 1199,
        /// <summary>Ungültige Eigenschaftendeklaration.Der get-Accessor darf keine Argumente besitzen, und der set-Accessor muss ein einzelnes Argument besitzen.</summary>
        BadPropertyDeclaration = 1200,
        /// <summary>Der Ausdruck hat keine Adresse.</summary>
        DoesNotHaveAnAddress = 1203,
        /// <summary>Es wurden nicht alle erforderlichen Parameter angegeben.</summary>
        TooFewParameters = 1204,
        /// <summary>Eine Zuweisung erstellt eine <see cref="T:Microsoft.JScript.Expando" />-Eigenschaft, die sofort verworfen wird.</summary>
        UselessAssignment = 1205,
        /// <summary>Die If-Bedingung kann keine Zuweisung enthalten.</summary>
        SuspectAssignment = 1206,
        /// <summary>Leere Anweisung in if-Anweisung gefunden.</summary>
        SuspectSemicolon = 1207,
        /// <summary>Die angegebene Konvertierung oder Koersion ist nicht möglich.</summary>
        ImpossibleConversion = 1208,
        /// <summary>final und abstract dürfen nicht zusammen verwendet werden.</summary>
        FinalPrecludesAbstract = 1209,
        /// <summary>Es wird eine Instanz erwartet.</summary>
        NeedInstance = 1210,
        /// <summary>Kann nur abstrakt sein, wenn die Klasse als abstrakt gekennzeichnet ist.</summary>
        CannotBeAbstract = 1212,
        /// <summary>Der enum-Basistyp muss ein primitiver ganzzahliger Typ sein.</summary>
        InvalidBaseTypeForEnum = 1213,
        /// <summary>Es ist nicht möglich, eine Instanz einer abstrakten Klasse zu erstellen.</summary>
        CannotInstantiateAbstractClass = 1214,
        /// <summary>Wenn einem <see cref="T:System.Array" /> ein JScript-Array zugewiesen wird, wird das Array eventuell kopiert.</summary>
        ArrayMayBeCopied = 1215,
        /// <summary>Statische Methoden können nicht abstrakt sein.</summary>
        AbstractCannotBeStatic = 1216,
        /// <summary>Statische Methoden können final sein.</summary>
        StaticIsAlreadyFinal = 1217,
        /// <summary>Statische Methoden können Basisklassenmethoden nicht überschreiben.</summary>
        StaticMethodsCannotOverride = 1218,
        /// <summary>Statische Methoden können Basisklassenmethoden nicht ausblenden.</summary>
        StaticMethodsCannotHide = 1219,
        /// <summary>
        ///   <see cref="T:Microsoft.JScript.Expando" />-Methoden dürfen keine Basisklassenmethoden überschreiben.</summary>
        ExpandoPrecludesOverride = 1220,
        /// <summary>Eine Variablenargumenteliste muss einen Arraytyp haben.</summary>
        IllegalParamArrayAttribute = 1221,
        /// <summary>
        ///   <see cref="T:Microsoft.JScript.Expando" />-Methoden dürfen nicht abstrakt sein.</summary>
        ExpandoPrecludesAbstract = 1222,
        /// <summary>Eine Funktion ohne Text sollte abstract sein.</summary>
        ShouldBeAbstract = 1223,
        /// <summary>Dieser Modifizierer kann nicht für einen Schnittstellenmember verwendet werden.</summary>
        BadModifierInInterface = 1224,
        /// <summary>Variablen können nicht in einer Schnittstelle deklariert werden.</summary>
        VarIllegalInInterface = 1226,
        /// <summary>Schnittstellen können nicht in einem interface deklariert werden.</summary>
        InterfaceIllegalInInterface = 1227,
        /// <summary>Das var-Schlüsselwort sollte in Deklarationen von enum-Membern nicht verwendet werden.</summary>
        NoVarInEnum = 1228,
        /// <summary>Die import-Anweisung ist in diesem Kontext nicht gültig.</summary>
        InvalidImport = 1229,
        /// <summary>In diesem Kontext nicht zugelassene enum-Definition.</summary>
        EnumNotAllowed = 1230,
        /// <summary>Dieses Attribut ist für diesen Deklarationstyp nicht gültig.</summary>
        InvalidCustomAttributeTarget = 1231,
        /// <summary>Paketdefinition in diesem Kontext nicht zulässig.</summary>
        PackageInWrongContext = 1232,
        /// <summary>Ein Konstruktor kann keinen Rückgabetyp besitzen.</summary>
        ConstructorMayNotHaveReturnType = 1233,
        /// <summary>Nur Klassen und Pakete sind in einer Bibliothek zulässig.</summary>
        OnlyClassesAndPackagesAllowed = 1234,
        /// <summary>Ungültige Debugdirektive.</summary>
        InvalidDebugDirective = 1235,
        /// <summary>Dieser Attributtyp muss eindeutig sein.</summary>
        CustomAttributeUsedMoreThanOnce = 1236,
        /// <summary>Ein nicht statischer geschachtelter Typ kann nur mit einem nicht statischen Typ erweitert werden, der in derselben Klasse geschachtelt ist.</summary>
        NestedInstanceTypeCannotBeExtendedByStatic = 1237,
        /// <summary>Ein Attribut für die Eigenschaft muss im get-Accessor angegeben werden, wenn der get-Accessor vorhanden ist.</summary>
        PropertyLevelAttributesMustBeOnGetter = 1238,
        /// <summary>Eine throw-Anweisung muss über ein Argument verfügen, sofern sie sich nicht im catch-Block einer try-Anweisung befindet.</summary>
        BadThrow = 1239,
        /// <summary>Eine Variablenargumentliste muss das letzte Argument sein.</summary>
        ParamListNotLast = 1240,
        /// <summary>Der Typ konnte nicht gefunden werden.Möglicherweise fehlt ein Assemblyverweis.</summary>
        NoSuchType = 1241,
        /// <summary>Falsch formatiertes Oktalliteral. Wird als Dezimalliteral behandelt.</summary>
        BadOctalLiteral = 1242,
        /// <summary>Auf einen nicht statischen Member kann nicht aus einem static-Bereich zugegriffen werden.</summary>
        InstanceNotAccessibleFromStatic = 1243,
        /// <summary>Auf einen statischen Member muss über den Klassennamen zugegriffen werden.</summary>
        StaticRequiresTypeName = 1244,
        /// <summary>Auf einen nicht statischen Member kann nicht mit dem Klassennamen zugegriffen werden.</summary>
        NonStaticWithTypeName = 1245,
        /// <summary>Der Typ hat keinen solchen static-Member.</summary>
        NoSuchStaticMember = 1246,
        /// <summary>Die Schleifenbedingung kann keinen Funktionsaufruf enthalten.</summary>
        SuspectLoopCondition = 1247,
        /// <summary>Assembly erwartet.</summary>
        ExpectedAssembly = 1248,
        /// <summary>Benutzerdefinierte Attribute von Assemblys dürfen nicht Teil eines anderen Konstrukts sein.</summary>
        AssemblyAttributesMustBeGlobal = 1249,
        /// <summary>
        ///   <see cref="T:Microsoft.JScript.Expando" />-Methoden können nicht statisch sein.</summary>
        ExpandoPrecludesStatic = 1250,
        /// <summary>Diese Methode hat den gleichen Namen, die gleichen Parametertypen und den gleichen Rückgabetyp wie eine andere Methode in dieser Klasse.</summary>
        DuplicateMethod = 1251,
        /// <summary>Klassenmember, die als Konstruktoren verwendet werden, sollten als <see cref="T:Microsoft.JScript.Expando" />-Funktionen markiert sein.</summary>
        NotAnExpandoFunction = 1252,
        /// <summary>Keine gültige Versionszeichenfolge.</summary>
        NotValidVersionString = 1253,
        /// <summary>Ausführbare Dateien können nicht lokalisiert werden. <see cref="P:System.Reflection.AssemblyCultureAttribute.Culture" /> muss immer leer sein.</summary>
        ExecutablesCannotBeLocalized = 1254,
        /// <summary>Der Operator Plus ist zur Verkettung von Zeichenfolgen sehr langsam.Verwenden Sie stattdessen <see cref="T:System.Text.StringBuilder" />.</summary>
        StringConcatIsSlow = 1255,
        /// <summary>Bedingte Kompilierungsdirektiven und Variablen können nicht im Debugger verwendet werden.</summary>
        CcInvalidInDebugger = 1256,
        /// <summary>
        ///   <see cref="T:Microsoft.JScript.Expando" />-Methoden müssen öffentlich sein.</summary>
        ExpandoMustBePublic = 1257,
        /// <summary>Delegate sollten nicht explizit konstruiert werden. Verwenden Sie einfach den Methodennamen.</summary>
        DelegatesShouldNotBeExplicitlyConstructed = 1258,
        /// <summary>Eine Assembly, auf die verwiesen wird, hängt von einer anderen Assembly ab, auf die nicht verwiesen wird oder die nicht gefunden werden konnte.</summary>
        ImplicitlyReferencedAssemblyNotFound = 1259,
        /// <summary>Diese Konvertierung schlägt möglicherweise zur Laufzeit fehl.</summary>
        PossibleBadConversion = 1260,
        /// <summary>Das Konvertieren einer Zeichenfolge in eine Zahl oder einen booleschen Wert ist langsam und kann zur Laufzeit Fehler verursachen.</summary>
        PossibleBadConversionFromString = 1261,
        /// <summary>Dies ist keine gültige RESOURCES-Datei.</summary>
        InvalidResource = 1262,
        /// <summary>Die Adresse des Operators kann nur in einer Liste von Argumenten verwendet werden.</summary>
        WrongUseOfAddressOf = 1263,
        /// <summary>Der angegebene Typ ist nicht CLS-kompatibel.</summary>
        NonCLSCompliantType = 1264,
        /// <summary>Der Klassenmember kann nicht als CLS (Common Language Specification)-kompatibel markiert werden, da die Klasse nicht als CLS-kompatibel markiert ist.</summary>
        MemberTypeCLSCompliantMismatch = 1265,
        /// <summary>Der Typ kann nicht als CLS-kompatibel markiert werden, da die Assembly nicht als CLS-kompatibel markiert ist.</summary>
        TypeAssemblyCLSCompliantMismatch = 1266,
        /// <summary>Die Assembly, auf die verwiesen wird, ist nicht kompatibel.</summary>
        IncompatibleAssemblyReference = 1267,
        /// <summary>Eine ungültige Assemblyschlüsseldatei wurde verwendet.</summary>
        InvalidAssemblyKeyFile = 1268,
        /// <summary>Der vollqualifizierte Typname ist zu lang.Er muss eine Länge von weniger als 1.024 Zeichen besitzen.</summary>
        TypeNameTooLong = 1269,
        /// <summary>Eine Memberinitialisierer kann keinen Funktionsausdruck enthalten.</summary>
        MemberInitializerCannotContainFuncExpr = 1270,
        /// <summary>Die Zuweisung zu this ist nicht möglich.</summary>
        CantAssignThis = 5000,
        /// <summary>Es wurde eine Zahl erwartet.</summary>
        NumberExpected = 5001,
        /// <summary>Eine Funktion wurde erwartet.</summary>
        FunctionExpected = 5002,
        /// <summary>Die Zuweisung zu einem Funktionsergebnis ist nicht möglich.</summary>
        CannotAssignToFunctionResult = 5003,
        /// <summary>Zeichenfolge erwartet.</summary>
        StringExpected = 5005,
        /// <summary>Datenobjekt erwartet.</summary>
        DateExpected = 5006,
        /// <summary>Es wurde ein Objekt erwartet.</summary>
        ObjectExpected = 5007,
        /// <summary>Ungültige Zuweisung.</summary>
        IllegalAssignment = 5008,
        /// <summary>Nicht definierter Bezeichner.</summary>
        UndefinedIdentifier = 5009,
        /// <summary>Kein boolescher Wert gefunden.</summary>
        BooleanExpected = 5010,
        /// <summary>Es wird ein VBArray erwartet.</summary>
        VBArrayExpected = 5013,
        /// <summary>Enumeratorobjekt erwartet.</summary>
        EnumeratorExpected = 5015,
        /// <summary>
        ///   <see cref="T:Microsoft.JScript.RegExpObject" />-Objekt erwartet.</summary>
        RegExpExpected = 5016,
        /// <summary>Syntaxfehler in regulärem Ausdruck.</summary>
        RegExpSyntax = 5017,
        /// <summary>Eine Ausnahme wurde ausgelöst, aber nicht abgefangen.</summary>
        UncaughtException = 5022,
        /// <summary>Diese Funktion hat kein gültiges Prototypobjekt.</summary>
        InvalidPrototype = 5023,
        /// <summary>Der zu decodierende URI enthält ein ungültiges Zeichen.</summary>
        URIEncodeError = 5024,
        /// <summary>Der zu decodierende URI ist keine gültige Codierung.</summary>
        URIDecodeError = 5025,
        /// <summary>Die Anzahl der Dezimalstellen liegt außerhalb des definierten Bereichs.</summary>
        FractionOutOfRange = 5026,
        /// <summary>Die Genauigkeit liegt außerhalb des definierten Bereichs.</summary>
        PrecisionOutOfRange = 5027,
        /// <summary>Die Arraylänge muss eine endliche positive Ganzzahl sein.</summary>
        ArrayLengthConstructIncorrect = 5029,
        /// <summary>Der Arraylänge muss eine endliche positive Zahl zugewiesen werden.</summary>
        ArrayLengthAssignIncorrect = 5030,
        /// <summary>Ein "|" ist kein Arrayobjekt.Arrayobjekt erwartet.</summary>
        NeedArrayObject = 5031,
        /// <summary>Dieser Konstruktor ist nicht vorhanden.</summary>
        NoConstructor = 5032,
        /// <summary>Ein <see cref="T:Microsoft.JScript.Eval" /> kann nicht von einem Alias aufgerufen werden.</summary>
        IllegalEval = 5033,
        /// <summary>Noch nicht implementiert.</summary>
        NotYetImplemented = 5034,
        /// <summary>Ein Parametername darf nicht NULL oder leer sein.</summary>
        MustProvideNameForNamedParameter = 5035,
        /// <summary>Doppelter Parametername.</summary>
        DuplicateNamedParameter = 5036,
        /// <summary>Der angegebene Parametername gehört nicht zu den formalen Parametern.</summary>
        MissingNameParameter = 5037,
        /// <summary>Es wurden zu wenige Argumente angegeben.Die Anzahl der Parameternamen darf nicht die Anzahl der übergebenen Argumente übersteigen.</summary>
        MoreNamedParametersThanArguments = 5038,
        /// <summary>Der Ausdruck kann nicht im Debugger ausgewertet werden.</summary>
        NonSupportedInDebugger = 5039,
        /// <summary>Die Zuweisung zu einem schreibgeschützten Feld oder einer schreibgeschützten Eigenschaft ist nicht möglich.</summary>
        AssignmentToReadOnly = 5040,
        /// <summary>Die Eigenschaft ist lesegeschützt.</summary>
        WriteOnlyProperty = 5041,
        /// <summary>Die Anzahl der Indizes stimmt nicht mit der Arraydimension überein.</summary>
        IncorrectNumberOfIndices = 5042,
        /// <summary>Methoden mit ref-Parametern können nicht im Debugger aufgerufen werden.</summary>
        RefParamsNonSupportedInDebugger = 5043,
        /// <summary>Die Sicherheitsmethoden <see cref="M:System.Security.CodeAccessPermission.Deny" />, <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> und <see cref="M:System.Security.CodeAccessPermission.Assert" /> können nicht beim späten Binden aufgerufen werden.</summary>
        CannotCallSecurityMethodLateBound = 5044,
        /// <summary>JScript unterstützt keine statischen Sicherheitsattribute.</summary>
        CannotUseStaticSecurityAttribute = 5045,
        /// <summary>Ein Ziel hat eine nicht mit CLS kompatible Ausnahme ausgelöst.</summary>
        NonClsException = 5046,
        /// <summary>Die Funktionsauswertung wurde abgebrochen.</summary>
        FuncEvalAborted = 6000,
        /// <summary>Das Zeitlimit für die Funktionsauswertung wurde überschritten.</summary>
        FuncEvalTimedout = 6001,
        /// <summary>Fehler bei der Funktionsauswertung.Der Thread wurde angehalten.</summary>
        FuncEvalThreadSuspended = 6002,
        /// <summary>Fehler bei der Funktionsauswertung.Der Thread wurde deaktiviert, wartet auf ein Objekt oder wartet auf das Ende eines anderen Threads.</summary>
        FuncEvalThreadSleepWaitJoin = 6003,
        /// <summary>Fehler bei der Funktionsauswertung.Der Thread kann falsche Daten enthalten.</summary>
        FuncEvalBadThreadState = 6004,
        /// <summary>Die Funktionsauswertung schlug fehl, und der Thread wurde nicht gestartet.</summary>
        FuncEvalBadThreadNotStarted = 6005,
        /// <summary>Funktionsauswertung abgebrochen.Zum Aktivieren der Eigenschaftsauswertung klicken Sie im Menü Extras auf Optionen, und wählen Sie dann in der Strukturansicht Debuggen aus.</summary>
        NoFuncEvalAllowed = 6006,
        /// <summary>Die Funktionsauswertung kann nicht ausgeführt werden, wenn das Programm an dieser Stelle beendet wird.</summary>
        FuncEvalBadLocation = 6007,
        /// <summary>Eine Webmethode kann nicht im Debugger aufgerufen werden.</summary>
        FuncEvalWebMethod = 6008,
        /// <summary>Eine statische Variable ist nicht verfügbar.</summary>
        StaticVarNotAvailable = 6009,
        /// <summary>Das Typobjekt für diesen Typ ist nicht verfügbar.</summary>
        TypeObjectNotAvailable = 6010,
        /// <summary>Ausnahme von HRESULT.</summary>
        ExceptionFromHResult = 6011,
        /// <summary>Der Ausdruck verursacht Nebeneffekte und wird nicht ausgewertet.</summary>
        SideEffectsDisallowed = 6012
    }
}
