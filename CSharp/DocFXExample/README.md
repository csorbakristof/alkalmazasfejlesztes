# Dokumentáció készítés kód alapján
EViP kollekciós labor Storage-hez dokumentáció generáltalás kód és kommentek alapján DocFX-el

## MS ajánlás
XML kommenttekkel "dokumentálás" majd ebből saját kezűleg vagy valamilyen tool-al tényleges dokumentáció generálás.
Ajánlott tool: DocFX vagy Sandcastle

https://docs.microsoft.com/hu-hu/dotnet/csharp/programming-guide/xmldoc/

## Dokumentációhoz használatos tag-ek
https://docs.microsoft.com/hu-hu/dotnet/csharp/programming-guide/xmldoc/recommended-tags-for-documentation-comments \
Kommentezésnél 3.-4. ```\``` jelnél auto-generálódik egy ```<summary>``` tag-es váz, függvényeknél visszatérési értékkel és paraméterekkel együtt.

## DocFX
https://dotnet.github.io/docfx/ \
HTML és PDF dokumentációt generál. Konfigurálható. \
Ennél a példánál a DocFX munkakönyvtára és a tényleges VS solution két egymás melletti mappában van, így nem keveredik a kód a dokumentációval
### Setup
* Letöltés és környezeti változókhoz path-ként hozzáadás (így cmd-ből használható)
* PDF generáláshoz még egy plusz tool letöltése és konfigurálása ugyanígy
* (VS-hez is hozzáadható NuGet package-ként, de így a build-elés közben generál doksit és a project struktúrán is változtat)
* Tool futtatása cmd-ből: ```docfx init -q``` paranccsal
* ```docfx_project\docfx.json``` file-ban konfigurációk módosítása
  * Ennél a példánál egyetlen módosítás az src-nél van, hogy a kód és a doksi külön könyvtárba szervezve legyenek és ne mosódjon össze az egész
* ```docfx docfx_project\docfx.json --serve``` paranccsal a dokumentáció elkészítése, ami a ```http://localhost:8080```-on megtekinthető
* PDF generáláshoz a ```docfx_project\docfx.json``` file-ban plusz konfigurációk szükségesek