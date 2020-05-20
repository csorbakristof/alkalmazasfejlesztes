# Tic-tac-toe játék

A mintaprogram az ismert tic-tac-toe játék egy lehetséges implementációca C# nyelven. 3x3-as táblán játszik két játékos, az egyik '+', a másik '-' karakterrel jelöli meg mezőit. Az nyer, akinek hamarabb lesz egyenes vonalban 3 mezője.

A játék jeleg konzolos felületen működik. A felületen megjelenik, hogy melyik játékos jön, és milyen inputokkal tud mezőt megjelölni.
(Megjelenik a játék jelenlegi állása, hanyadik körben járunk és hogy ki következik. Ezután meg kell adnnia a játékosnak, hogy hanyadik sor hanyadik oszlopába szeretne mezőt lerakni. A felületen megjelenő "R" karakter után sorszámot, "C" karakter után oszlopszámot kell megadni.)

A játék hibát dob, ha a játékos nem megfelelő helyre szeretne mezőt tenni. (Pl. már foglalt mezőre, vagy olyanra, ami nem megfelelő sorban és/vagy oszlopban van.)

A játék véget ér, ha 3 egyforma mező kerül le egyenes vonalban (értsd: vízszintesen, függőlegesen, átlósan). Ekkor a legutóbbi soron lévő játékos nyer, hiszen ő rakta le az utolsó, nyerő mezőt.

A játék véget ér, ha minden mező foglalt lesz. Ilyenkor ha az előző pont szerint nincs nyertes, akkor döntetlen az eredmény.

A mintaprogramhoz készültek unit tesztek, amelyek az elvárt működést tesztelik. (Bővebb info: UnitTests.md)
A mintaprogramhoz készült DocFx-el generált dokumentáció, amely a program működését dokumentálja. (Bővebb info: DocFx.md)