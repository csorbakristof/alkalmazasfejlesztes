# MVVM keretrendszer
A mintaalkalmaás teendők "mentésére", lekérdezésére és "törlésére" használható UWP app.

Megadhatjuk a teendő nevét, egy leírást hozzá és a határidejét. Ehhez van egy felületünk, ahol felvehetünk új teendőt, ahol ki tudjuk listáztatni a teendőket és ahol törölni is tudunk.

Az első alkalamzás (TodoUWP) az MVVM minta használatához az ```ICommand``` és az ```INotifyPropertyChanged``` interface-ek implementálásával egy saját "MVVM vázat" mutatnak be ehhez.

A második alkalamzás (TodoUWP.Template10) egy már meglévő MVVM keretrendszer, a Template10 felhasználásával szemlélteti ugyanezt.

## Közös részletek
Mindkét alkalmazás ugyanazokat a model osztályokat és "adatelérési" kódot használja, de most a minta könnyebb átlátása érdekében ezek nem lettek külön project-ekbe/package-ekbe szervezve.

Model osztály egy ```TodoItem``` névvel, leírással, határidővel.

Adateléréshez jelenleg egy memóriában lévő placeholder van (```DbService```). Vegyük észre, hogy mivel a hívó felek nem a konkrét működést megvalósító osztályt kérik és tárolják, hanem csak egy funkciókat leíró interface-t várnak (```IDbService```) ezért egyszerűen lehetséges lenne fájlba mentő és onnan olvasó adatelérési réteg vagy akár tényleges adatbázist használó réteg megvalósítása is, amíg az implementálja ezt az interface-t (inversion of control).

Kis eltérés van a Viewk között, a saját megvalósításnál explicit be van hivatkozva a ViewModel namespace, míg erre a Template10-es project-nél a használt dependency injection rendszer miatt nincs szükség.

Megjegyzendő, hogy a jelenlegi view középpontjában nem felhasználói "élmény" van, csupán arra jó ez, hogy megjelenjenek valahogyan az adatok, látni lehessen, hogy mi történik, lehessen propertykhez és commandokhoz adatkötni!

Ezzel "letudva" az MVVM modelje és view-ja. Nézzük az eltérő részeket, melyek a viewmodelhez kapcsolódnak.

## Saját ICommand és INPC
Ahhoz, hogy ne kelljen minden egyes bind-oldandó propertynél az INPC megvalósítással sok többletkódot írni illetve több viewmodel esetén ne kelljen minden egyes viewmodelben az INPC-ből leszármazni és azt megvalósítani gyakori minta, hogy egy ViewModelBase vagy egy BindableBase ősbe szervezik ki az INPC-t.

A saját megvalósításnál ez a ```BindableBase``` osztályba került.

Ahhoz, hogy például a view gombjainak eseménykezelése ne a code-behindba kerüljön a command mintát szoktuk használni. Ekkor a gombon a command propertyt egy ICommand interfacet megvalósító objektumhoz adatkötjük.

Ez az ICommand megvalósítás megnézi, hogy ez adott művelet végrehajtható-e és ha igen, akkor végrehajtja. Jelen esetben az egyszerűség kedvéért mindig igazat kapunk a végrehajthatóság kérdésére.

Ehhez a megvalósítás a ```CommandBase``` osztályba került.

Nézzük ezek használatát a ViewModelben!
- Először is a ```BindableBase```-ből kell leszármaznunk.
- Másodszor az adatkötni kívánt propertyhez kellenek privát változók, mert így lehetséges a ```BindableBase``` ```Set``` metódusát hívni. Mivel listát is meg szeretnénk jeleníteni, ne feledjük, hogy ehhez az ```ObservableCollection``` használatos!
- Most a ```DbService``` a konstruktorban van létrehozva. MVVM keretrendszerek lehetőséget biztosítanak arra, hogy ezeket egy DI keretrendszer vagy container segítségével kívülről adjuk át a viewmodelnek.
- A ```CommandBase```-t használó commandjainkat is a viewmodel konstruktorban kell inicializálni. Ezek egyszerű műveleteket látnak el melyek egy-egy segédfüggvényben vannak.

## Template10 használata
Ahhoz, hogy egy külső MVVM keretrendszert tudjunk használni (legyen az Template10, Prism, MVVMLight,...) azt először NuGet packageként hozzá kell adnunk a projecthez. Ha az adott keretrendszer önmagában nem valósít meg vagy használ más DI könyvtárat, akkor azt is magunknak kell a projecthez adni!

A mostani példánál az MVVM keretrendszer a Template10, a használt DI rendszer pedig az Autofac.

- Template10 használatánál a viewmodelnek a ```ViewModelBase``` ősosztályból kell leszármaznia.
Az INPC implementáció hasonló itt is ahhoz, ahogyan az a saját példában látható.
Különbség, hogy a használt ```IDbService```-t most konstuktorparaméterben kapja a viewmodel, tehát csak egy olyan objektumot vár, amin az interface függvényei meghívhatóak, léteznek.
- Megjegyzendő, hogy a Template10 ```ViewModelBase``` osztálya nemcsak az INPC-t valósítja meg. Ez biztosít egy navigáviós servicet is. MVVM mintában a navigáció az, ami kicsit nehezen kezelhető, mert view-on történő esemény commandként jut el a viewmodelhez, aminek a viewt kell változtatnia, de nem a rendelkezésre álló adatkötésekkel.
- Az előző ```CommandBase``` helyett most ```DelegateCommand``` van. Ez tudja megfelően kezelni a művelet végrehajthatóságát, illetve paraméterezett command is végrehajtható vele. (Paraméterezett commandot használunk, amikor például egy lista elemét szeretnénk törölni és az abban a sorban megjelenő törlés gombra kattintunk. Ilyenkor a parancs paraméterként megkapja, hogy melyik elemet kell törölnie.)
- Ezeken kívül az ```App.xaml``` és ```App.xaml.cs``` fájlt is módosítanunk kell. Ez az alkalamzás belépési pontja.
- Az appnak a Template10-es ```BootStrapper``` osztályból kell leszármaznia. Ezt az ```App.xaml```-ben is be kell állítani: a gyökérelem legyen a megfelelő Template10-es ```BootStrapper```.
- ```OnLaunched``` helyett ```OnStartAsync``` függvény van, ebben történik a megfelelő kezdő view kiválasztása és a DI konfigurálása.
- DI konfigurálás a ```ConfigureDependencies``` segédfüggvényben van. Itt megadjuk, hogy ha valamikor szükség van egy ```IDbService``` típusú objektumra, akkor egy ```DbService``` típusú objektum legyen az, ami ezt megvalósítja. (Hasonló a konfiguráció a viewmodelhez is)
- Vegyük észre, hogy az egyes beregisztrált elemek scopeja/élettartama eltérő: a ```DbService``` singleton, azaz csak egy objektum létezik az egész alkalamzás futása során, míg a viewmodelnél a default beállítás az érvényes.
- Ahhoz, hogy a view és a viewmodel megfelelően összerendelődjön, megfelelő legyen a view DataContextje, a ```ResolveForPage``` felüldefiniálásával megadhatjuk, hogy melyik viewhoz melyik viewmodel tartozik.
