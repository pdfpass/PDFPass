![ref-card.jpg](screenshots%2Fref-card.jpg)

# PDFPass 🇸🇰
PDFPass je bezplatný offline nástroj s otvoreným zdrojovým kódom napísaný v jazyku C# (.NET 8) na rýchle a jednoduché šifrovanie/heslovanie súborov PDF ako aj odstraňovanie hesiel. 

Toto je oficiálna verzia PDFPass založená na projekte PDFEncrypt.net/Ryan Griggs. Upozorňujeme, že všetky ostatné verzie sú klony a nemusia rešpektovať alebo dodržiavať základné zásady ochrany súkromia a slobody, ktoré zastáva autor projektu.

Aktuálny ZIP balík PDFPass pre systém Windows: https://github.com/pdfpass/PDFPass/releases/latest

## Parametre prikazoveho riadku

```
-i [cesta vstupného súboru] alebo --input [cesta vstupného súboru]

-o [cesta výstupného súboru] alebo --output [cesta výstupného súboru]

--user_pass [heslo uzmknutia čítania]

--owner_pass [heslo vlastníka]

--run - okamžite vykonať funkciu Zahesluj pri spustení (nečakať, kým používateľ klikne na tlačidlo)
```

## Hlavná obrazovka pre nastavenia hesla
![App Screenshot](screenshots%2Fencrypt.png)

## Hlavná obrazovka pre odstránenia hesla
![App Screenshot](screenshots%2Fdecrypt.png)

## Nastavenia
![Settings](screenshots%2Fsettings.png)

# "Inštalácia" a kontextové menu (vyžaduje práva administrátora)

Súbor ```PDFPass-portable.zip``` stačí rozbaliť a umiestniť kdekoľvek, nie je potrebné umiestňovať do ```c:\Program Files``` alebo ```c:\Program Files (x86)```. Aktivácia kontextového menu je jednoduchá, postačuje spustiť súbor ```kontextove-menu-ako-admin-zaregistruj.cmd``` s právami administrátora (viď screenshot)

![register-menu.png](screenshots%2Fregister-menu.png)

Ak registrácia prebehne úspešne, po kliknutí pravým tlačidlom myši na súbor PDF sa zobrazí kontextová ponuka **Otvor v PDFPass** (viď screenshot)

![context-menu.png](screenshots%2Fcontext-menu.png)


# "Inštalácia" a odkaz na ploche (bez práv administrátora)

Súbor ```PDFPass-portable.zip``` stačí rozbaliť a umiestniť kdekoľvek. Následne pre vytvorenie odkazu na PDFPass na Ploche stači spustiť súbor ```pridaj-odkaz-na-plochu.cmd```


# Zmeny oproti pôvodnej verzii

* prechod na aktuálnu platformu z .NET 4.7 na .NET 8
* preklad do slovenčiny 🇸🇰
* upgrade knižnice na prácu s PDF (iText 8.x). Pôvodná verzia (iText 7) obsahovala zraniteľnosť. Viac na: https://devhub.checkmarx.com/cve-details/CVE-2023-6299/
* vizuálne vylepšenia


# Plánované zmeny
* funkcionalita odstránenia hesla z PDF ✅
* podpora viacerých jazykov ⏳
* možnosť inštalácie aplikácie z Microsoft Store ⏳
* vlastná web stránka ⏳


# License

The PDFPass application and source code are licensed under the AGPL.  You may download, install, use, and distribute the PDFPass application freely under the AGPL.  You may download, modify, fork, and distribute the PDFPass source code under the AGPL.

Restrictions: you may not use the term "Official" or "Original" to designate forks of this project, and forks must credit Gabriel Boss (Java Guru) and PDFPass.net both within the application and on any documentation and/or related websites.

**The "PDFPass" brand is a trademark of PDFPass.net/Gabriel Boss and is not licensed under the AGPL.  If you wish to release a forked version of PDFPass, you must rename it to avoid confusion with the official branch.**

**The PDFPass Logo is a trademark of, and copyrighted by, PDFPass.net/Gabriel Boss and is not licensed under the AGPL. If you fork this project, you must create your own logo which is materially different from the official logo, to avoid confusion between yours and the official/original version.**



