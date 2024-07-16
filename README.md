![ref-card.jpg](screenshots%2Fref-card.jpg)

# PDFPass 🇸🇰
PDFPass je **bezplatný** offline nástroj s otvoreným zdrojovým kódom v jazyku C# (.NET 8) na rýchle a jednoduché šifrovanie/heslovanie súborov PDF ako aj odstraňovanie hesiel. 

Toto je oficiálna verzia PDFPass založená na projekte _PDFEncrypt.net/Ryan Griggs_. Upozorňujeme, že všetky ostatné verzie sú klony a nemusia rešpektovať alebo dodržiavať základné zásady ochrany súkromia a slobody, ktoré zastáva autor projektu.

Kontakt: **pdfpass@outlook.com**

## Súbor na stiahnutie 💾
Aktuálny ```PDFPass-portable.zip``` najdete na: https://github.com/pdfpass/PDFPass/releases/latest

## Kedy použiť "heslo na čítanie" (user password) 🔐?

* Ochrana citlivých informácií: Ak PDF dokument obsahuje citlivé alebo dôverné informácie, je vhodné ho chrániť heslom, aby ste zamedzili neoprávnenému prístupu.

* Distribúcia medzi obmedzený počet osôb: Ak je PDF určený na distribúciu len medzi určitých jednotlivcov alebo skupinu osôb, heslo zabezpečí, že dokument môže byť otvorený len tými, ktorí poznajú heslo.

* Právne alebo regulačné požiadavky: V niektorých prípadoch môžu byť požiadavky na ochranu dokumentov heslom dané právnymi predpismi alebo internými reguláciami organizácie.

## Kedy použiť "heslo vlastníka" (owner password) 🔐? 
Toto heslo poskytuje rôzne možnosti ochrany, ktoré umožňujú kontrolovať, čo môžu používatelia s dokumentom robiť. Tu sú hlavné možnosti ochrany, ktoré môžete nastaviť pomocou "hesla vlastníka":
* Zabránenie úpravám dokumentu:
  * Zabránenie všetkým úpravám: Môžete nastaviť heslo, aby nikto nemohol dokument upravovať.
  * Obmedzené úpravy: Môžete povoliť len určité typy úprav, ako je napríklad vyplňovanie formulárov alebo pridávanie komentárov.
* Zabránenie tlače dokumentu:
  * Zabránenie všetkej tlače: Môžete nastaviť heslo, aby nikto nemohol dokument tlačiť.
  * Zabránenie kvalitnej tlače: Môžete povoliť len tlač v nízkej kvalite, čím sa zabezpečí, že dokument nebude možné vytlačiť vo vysokej kvalite.
* Zabránenie kopírovaniu textu a obrázkov: Heslom môžete zabrániť používateľom, aby kopírovali text alebo obrázky z dokumentu do iných aplikácií.
* Zabránenie extrahovaniu stránok: Môžete zabrániť používateľom v extrahovaní jednotlivých stránok z dokumentu a ich uložení ako samostatné PDF súbory.
* Zabránenie asistenčným technológiám: Môžete obmedziť používanie asistenčných technológií, ako sú obrazovkové čítačky, ktoré by inak mohli čítať obsah dokumentu pre zrakovo postihnutých používateľov.
* Zabránenie pridávaniu anotácií: Môžete obmedziť používateľov, aby nemohli pridávať komentáre, poznámky alebo anotácie k dokumentu.

Tieto možnosti ochrany zabezpečujú, že dokument zostane chránený pred neoprávnenými úpravami, tlačou, kopírovaním alebo extrahovaním obsahu, čím zvyšujú bezpečnosť a kontrolu nad distribúciou a používaním PDF dokumentov.

## Podpora projektu 💶
Projekt môžete finančne podporiť:
* platbou prevodom - [payme](https://payme.sk/?V=1&IBAN=SK1611000000002615114396&AM=5.00&CC=EUR&DT=&PI=&MSG=PDFPass&CN=) (po kliknutí na link by malo nastať automatické presmerovanie do bankovej Android/iOS aplikácie (ČSOB, Tatra banka, SLSP, VÚB) - údaje budú predvyplnené, cenu je možné upraviť. V ostatných pripadoch sa zobrazi stránka [payme.sk](https://payme.sk) s QR kódom, ktorý stačí zosnímať…
* platbou cez systém [PayPal](https://www.paypal.com/donate/?hosted_button_id=5G336LA7YBMXQ&locale.x=sk_SK) (cenu je potrebné zadať)

Váš príspevok pomôže zaplatiť **bezpečnostné aktualizácie** a rozvoj programu, ktorý zostane navždy **zadarmo**. 

## "Inštalácia" a kontextové menu (vyžaduje práva administrátora)

Súbor ```PDFPass-portable.zip``` stačí rozbaliť a umiestniť kdekoľvek, nie je potrebné umiestňovať do ```c:\Program Files``` alebo ```c:\Program Files (x86)```. Aktivácia kontextového menu je jednoduchá, postačuje spustiť súbor ```kontextove-menu-ako-admin-zaregistruj.cmd``` s právami administrátora (viď screenshot)

![register-menu.png](screenshots%2Fregister-menu.png)

Ak registrácia prebehne úspešne, po kliknutí pravým tlačidlom myši na súbor PDF sa zobrazí kontextová ponuka **Otvor v PDFPass** (viď screenshot)

![context-menu.png](screenshots%2Fcontext-menu.png)


## "Inštalácia" a odkaz na ploche (bez práv administrátora)

Súbor ```PDFPass-portable.zip``` stačí rozbaliť a umiestniť kdekoľvek. Následne pre vytvorenie odkazu na PDFPass na Ploche postačuje spustiť súbor ```pridaj-odkaz-na-plochu.cmd```
## Parametre príkazového riadku

```
-i [cesta vstupného súboru] alebo --input [cesta vstupného súboru]

-o [cesta výstupného súboru] alebo --output [cesta výstupného súboru]

--user_pass [heslo uzmknutia čítania]

--owner_pass [heslo vlastníka]

--run - okamžite vykonať funkciu "Zahesluj" po spustení (nečakať, kým používateľ klikne na tlačidlo)
```

## Hlavná obrazovka pre nastavenia hesla
![App Screenshot](screenshots%2Fencrypt.png)

## Hlavná obrazovka pre odstránenia hesla
![App Screenshot](screenshots%2Fdecrypt.png)

## Obrazovka nastavení
![Settings](screenshots%2Fsettings.png)


## Zmeny oproti pôvodnej verzii

* prechod na aktuálnu platformu z .NET 4.7 na .NET 8
* preklad do slovenčiny 🇸🇰
* upgrade knižnice na prácu s PDF (iText 8.x). Pôvodná verzia (iText 7) obsahovala zraniteľnosť. Viac na: https://devhub.checkmarx.com/cve-details/CVE-2023-6299/
* vizuálne vylepšenia


## Plánované zmeny
* **funkcionalita odstránenia hesla z PDF ✅**
* podpora viacerých jazykov ⏳
* možnosť inštalácie aplikácie z Microsoft Store ⏳
* vlastná web stránka ⏳


# License

The PDFPass application and source code are licensed under the AGPL.  You may download, install, use, and distribute the PDFPass application freely under the AGPL.  You may download, modify, fork, and distribute the PDFPass source code under the AGPL.

Restrictions: you may not use the term "Official" or "Original" to designate forks of this project, and forks must credit Gabriel Boss (Java Guru) and PDFPass.net both within the application and on any documentation and/or related websites.

**The "PDFPass" brand is a trademark of PDFPass/Gabriel Boss and is not licensed under the AGPL.  If you wish to release a forked version of PDFPass, you must rename it to avoid confusion with the official branch.**

**The PDFPass Logo is a trademark of, and copyrighted by, PDFPass/Gabriel Boss and is not licensed under the AGPL. If you fork this project, you must create your own logo which is materially different from the official logo, to avoid confusion between yours and the official/original version.**



