# MVVM keretrendszer
A mintaalkalmaás teendők "mentésére", lekérdezésére és "törlésére" használható UWP app.

Megadhatjuk a teendő nevét, egy leírást hozzá és a határidejét. Ehhez van egy felületünk, ahol felvehetünk új teendőt, ahol ki tudjuk listáztatni a teendőket és ahol törölni is tudunk.

Az első alkalamzás (TodoUWP) az MVVM minta használatához az ```ICommand``` és az ```INotifyPropertyChanged``` interface-ek implementálásával egy saját "MVVM vázat" mutatnak be ehhez.

A második alkalamzás (TodoUWP.Template10) egy már meglévő MVVM keretrendszer, a Template10 felhasználásával szemlélteti ugyanezt.

## Közös részletek
Mindkét alkalmazás ugyanazokat a model osztályokat és "adatelérési" kódot használja, de most a minta könnyebb átlátása érdekében ezek nem lettek külön project-ekbe/package-ekbe szervezve.

Model osztály egy ```TodoItem``` névvel, leírással, határidővel.

Adateléréshez jelenleg egy memóriában lévő placeholder van (```DbService```). Vegyük észre, hogy mivel a hívó felek nem a konkrét működést megvalósító osztályt kérik és tárolják, hanem csak egy funkciókat leíró interface-t várnak (```IDbService```) ezért egyszerűen lehetséges lenne fájlba mentő és onnan olvasó adatelérési réteg vagy akár tényleges adatbázist használó réteg megvalósítása is, amíg az implementálja ezt az interface-t (inversion of control).

Kis eltérés van a View-k között, a saját megvalósításnál explicit be van hivatkozva a ViewModel namespace, míg erre a Template10-es project-cél a használt dependency injection rendszer miatt nincs szükség.

Megjegyzendő, hogy a jelenlegi view középpontjában nem felhasználói "élémy" van, csupán arra jó ez, hogy megjelenjenek valahogyan az adatok, látni lehessen, hogy mi történik, lehessen property-khez és command-okhoz adatkötni!

Ezzel "letudva" az MVVM modelje és view-ja. Nézzük az eltérő részeket, melyek a viewmodel-hez kapcsolódnak.

## Saját ICommand és INPC
Ahhoz, hogy ne kelljen minden egyes bind-oldandó property-nél az INPC megvalósítással sok többletkódot írni illetve több viewmodel esetén ne kelljen minden egyes viewmodel-be az INPC-ből leszármazni és azt megvalósítani gyakori minta, hogy egy ViewModelBase vagy egy BindableBase ősbe szervezik ki az INPC-t.

A saját megvalósításnál ez a ```BindableBase``` osztályba került.

Ahhoz, hogy például a view gombjainak eseménykezelése ne a code-behind-ba kerüljön a command mintét szoktuk használni. Ekkor a gombon a command property-t egy ICommand interface-t megvalósító objektumhoz adatkötjük.

Ez az ICommand megvalósítás megnézi, hogy ez adott művelet végrehajtható-e és ha igen, akkor végrehajtja. Jelen esetben az egyszerűség kedvéért mindig igazat kapunk a végrehajthatóság kérdésére.

Ehhez a megvalósítás a ```CommandBase``` osztályba került.

Nézzük ezek használatát a ViewModel-ben!
- Először is a ```BindableBase```-ből kell leszármaznunk.
- Másodszor az adatkötni kívánt property-hez kellenek privát változók, mert így lehetséges a ```BindableBase``` ```Set``` metódusát hívni. Mivel listát is meg szeretnénk jeleníteni, ne feledjük, hogy ehhez az ```ObservableCollection``` használatos!
- Most a ```DbService``` a konstruktorban van létrehozva. MVVM keretrendszerek lehetőséget biztosítanak arra, hogy ezeket egy DI keretrendszer vagy container segítségével kívülről adjuk át a viewmodel-nek.
- A ```CommandBase```-t használó commandjainkat is a viewmodel konstruktorban kell inicializálni. Ezek egyszerű műveleteket látnak el melyek 1-1 segédfüggvényben vannak.

## Template10 használata
Ahhoz, hogy egy külső MVVM keretrendszer tudjunk használni (legyen az Template10, Prism, MVVMLight,...) azt először NuGet package-ként hozzá kell adnunk a project-hez. Ha az adott keretrendszer önmagában nem valósít meg vagy használ más DI könyvtárat, akkor azt is magunknak kell a project-hez adni!

A mostani példánál az MVVM keretrendszer a Template10, a használt DI rendszer pedig az Autofac.

- Template10 használatánál a viewmodel-nek a ```ViewModelBase``` ősosztályból kell leszármaznia.
Az INPC implementáció hasonló itt is ahhoz, ahogyan az a saját példában látható.
Különbség, hogy a használt ```IDbService```-t most konstuktorparaméterben kapja a viewmodel, tehát csak egy olyan objektumot vár, amin az interface függvényei meghívhatóak, léteznek.
- Megjegyzendő, hogy a Template10 ```ViewModelBase``` osztálya nemcsak az INPC-t valósítja meg. Ez biztosít egy navigáviós service-t is. MVVM mintában a navigáció az, ami kicsit nehezen kezelhető, mert view-on történő esemény command-ként jut el a viewmodel-hez, aminek a view-t kell változtatnia de nem a rendelkezésre álló adatkötésekkel.
- Az előző ```CommandBase``` helyett most ```DelegateCommand``` van. Ez tudja megfelően kezelni a művelet végrehajthatóságát, illetve paraméterezett command is végrehajtható vele. (Paraméterezett command-ot használunk, amikor például egy lista elemét szeretnénk törölni és az abban a sorban megjelenő törlésgombra kattintunk.)
- Ezeken kívül az ```App.xaml``` és ```App.xaml.cs``` fájlt is módosítanunk kell. Ez ugye az alkalamzás belépési pontja.
- Az appnak a Template10-es ```BootStrapper``` osztályból kell leszármaznia. Ezt az ```App.xaml```-ben is be kell állítani: a gyökérelem legyen a megfelelő Template10-es ```BootStrapper```.
- ```OnLaunched``` helyett ```OnStartAsync``` függvény van, ebben történik a megfelelő kezdő view kiválasztási és a DI konfigurálása.
- DI konfigurálás a ```ConfigureDependencies``` segédfüggvényben van. Itt megadjuk, hogy ha valamikor szükség van egy ```IDbService``` típusú objektumra, akkor egy ```DbService``` típusú objektum legyen az, ami ezt megvalósítja. (Hasonló konfiguráció a viewmodel-hez is)
- Vegyük észre, hogy az egyes beregisztrált elemek scope-ja/élettartama eltérő: a ```DbService``` singleton, azaz csak egy objektum létezik az egész alkalamzás futása során, míg a viewmodel-nél a default beállítás az érvényes.
- Ahhoz, hogy a view és a viewmodel megfelelően összerendelődjön, megfelelő legyen a view DataContext-je a ```ResolveForPage``` felüldefiniálásával megadhatjuk, hogy melyik view-hoz melyik viewmodel tartozik.