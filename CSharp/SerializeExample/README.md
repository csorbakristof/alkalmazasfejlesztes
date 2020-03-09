# Szerializálás példák

Könyv osztály szerializálása string és int property-kkel és egy beágyazott listával, ami a fejezeteket tartalmazza.

## Többféle szerializáló könyvtár bemutatása:
* XML
  * System.Xml.Serialization \
  https://docs.microsoft.com/en-us/dotnet/standard/serialization/examples-of-xml-serialization

* JSON
  * System.Text.Json \
https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to

  * Newtonsoft Json \
https://www.newtonsoft.com/json

## Setup
Ahhoz, hogy ne dobjon file beolvasásnál hibát a TextFiles mappában lévő txt-ket másold be a Projectmappa/bin/Debug/netcoreapp3.1 mappába!

## Opcionális plusz funkciók bemutatása:
* Attribútumban megadható, hogy pontosan mik szerializálódjanak ki: \
Newtonsoft: ```[JsonObject(MemberSerialization.OptIn)]``` az osztályra és ```[JsonProperty]``` azokra, amiket akarunk szerializálni
* Attribútumban megadható, hogy milyen névvel szerializálódjanak az egyes property-ki: \
System.Text.Json: ```[JsonPropertyNameAttribute("nev")]```