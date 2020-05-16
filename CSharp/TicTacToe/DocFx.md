# Tic-tac-toe játék - Dokumentáció generálás
A mintaprogramhoz készült DocFx-el generált dokumentáció, amely a program működését dokumentálja.
## Kommentek
.NET környezetben egyfajta konvenció az XML tag-ekkel való dokumentálás. Három egymás után leírt '/' jel után automatikusan generálódik egy nyitó és záró ```<summary>``` tag.

Más tag-ek is használatosak, ezek közül néhány: ```<summary>```, ```<remarks>```, ```<returns>```,```<exception>```. További infó ezekről a kommentekről: [ezen a linken](https://docs.microsoft.com/en-us/dotnet/csharp/codedoc)

Az ilyen kommentekkel ellátott kódból lehetséges generáló tool-ok segítségével dokumentációt generáltatni.

Az egyik ajánlás a [DocFX](https://dotnet.github.io/docfx/index.html).

## DocFX telepítése
Ahogy a telepítési útmutató is írja: GitHub repoból letölthető a legújabb verzió zip-ben, ezt valahova kicsomagoljuk és hozzáadjuk a környezeti változókhoz, hogy használható legyen.

Környezeti változóhoz hozzáadás:
1. Windows gomb: "környezeti változók"-ra keresés
2. Rendszer környezeti változóinak módosítása
3. Környezeti változók
4. Rendszerváltozók alatt Path kiválasztása és szereksztés
5. Adjuk hozzá a kicsomagolt docfx mappát (pl. C:\Tools\docfx az Új gombbal
6. Mindent okézzunk le

Ezzel készen vagyunk.

## Struktúra létrehozása
Alapértelmezésként egy docfx project-en belülre kellene létrehozni a Visual Studio project-et/solution-t.

Elegánsabb megoldás, ha a doksi project és a VS project egymás mellett külön vannak.

Ehhez nyissuk meg intézőben a project mappát. Abban a mappában legyünk, amelyben az sln file van. Windows Intézőben az elérési útba kattintsunk, majd a sor elejére egy ```cmd ``` szöveget írjunk, majd enter. Így elindítottunk egy command line ablakot az aktuális mappában.

Adjuk ki a DocFx inicializáló parancsát: ```docfx init -q```

*Ha sikerült beállítani környezeti változónak a tool-t, akkor ez így működik, nem kapunk rá hibaüzenetet.*

Létrejött egy ```docfx_project``` mappa az sln file és a project-ek mappái mellett.
Ezen belül vannak a szükséges konfigurációs fájlok és ide rakhatjuk a kommenteken kívüli további "dokumentációinkat" (pl képek, feladatleírás)

Konfigurációkat tartalmazó fájl: ```docfx.json```

Ezen belül az alábbi módosítást végezzük el:
Az eddigi "src"-t, ezt:
```json
"src": [
        {
          "files": [
            "src/**.csproj"
          ]
        }
      ],
```
cseréljük le erre:
```json
"src": [
        {
		"files": [
            "**/TicTacToe.csproj"
          ],
		"src":".."
        }
      ],
```
Ezzel mit is csináltunk? Megmondtuk a tool-nak, hogy ne az src mappában keressen project-et, amihez dokumentációt akarunk generáltatni, hanem egy mappával feljebb (```"src":".."```) és ezen belül bárhol létező TicTacToe C# project-hez (```"../TicTacToe.csproj"```)

## Dokumentáció generáltatás

Az előzőleg megnyitot command line ablakban adjuk ki a következő parancsokat:

```docfx docfx_project/docfx.json```

Ezzel a szükséges html és markdown fájlokat elkészítette a tool.
```docfx serve docfx_project/_site```

Ezzel az előzőleg elkészített fájlokból egy böngészőben megtekinthető dokumentáció készült. A parancs futtatása után a tool közli, hogy milyen címen érhetjük el (pl. localhost:8080)

## Formázás, finomítások
Ahogy láthattuk, a tool nem csak a kommentezet kódunkból, hanem mellékelhető .md fájlokból is hozzátesz a doksihoz. Ez használható arra, hogy egyéb információkat is hozzáfűzzünk a doksihoz a kommentekből generáltakon kívül. Ehhez a docfx_project mappán belüli index.md, articles mappán belüli intro.md, api mappán belüli index.md fájlokat módosíthatjuk. (Például index.md módosítható azért, hogy a megjelenő főoldalon más tartalom legyen, mondjuk feladatleírás, api/index.md módosítható azért, hogy  project-ekhez tartozó osztálydiagrammok és egy átfogó ismertetőt tartalmazzon az elkészített programról.)