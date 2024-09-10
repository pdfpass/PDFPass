![ref-card.jpg](screenshots%2Fref-card.jpg)

# PDFPass 🇸🇰
**PDFPass** je **bezplatný** offline nástroj s otvoreným zdrojovým kódom v jazyku C# (.NET 8) na rýchle a jednoduché **šifrovanie/heslovanie** súborov PDF, ako aj **odstraňovanie** hesiel. 

Toto je oficiálna verzia PDFPass založená na projekte _PDFEncrypt.net/Ryan Griggs_. Upozorňujeme, že všetky ostatné verzie sú klony a nemusia rešpektovať alebo dodržiavať základné zásady ochrany súkromia a slobody, ktoré zastáva autor projektu.

Kontakt: **pdf.pass@outlook.com**

## Súbor na stiahnutie 💾
Aktuálny ```PDFPass-portable.zip``` alebo ```pdfpass-install.exe``` nájdete tu: [stiahnuť](https://github.com/pdfpass/PDFPass/releases/latest)
<br>
<br>

# PDF - podporuje 2 typy hesiel
Oba typy je možné nastaviť/odstrániť pomocou **PDFPass**

## 1. Heslo pre uzamknutie čítania (user password) 🔑

* Ochrana citlivých informácií: Ak PDF dokument obsahuje **citlivé** alebo **dôverné** informácie, je vhodné ho chrániť heslom, aby ste zamedzili neoprávnenému prístupu.

* Distribúcia medzi obmedzený počet osôb: Ak je PDF určený na distribúciu len medzi určitých jednotlivcov alebo skupinu osôb, heslo zabezpečí, že dokument môže byť otvorený len tými, ktorí poznajú heslo.

* Právne alebo regulačné požiadavky: V niektorých prípadoch môžu byť požiadavky na ochranu dokumentov heslom dané právnymi predpismi alebo internými reguláciami organizácie.

## 2. Heslo vlastníka (owner password) 🔑
Toto heslo poskytuje rôzne možnosti ochrany, ktoré umožňujú kontrolovať, čo môžu používatelia s dokumentom robiť. Tu sú hlavné možnosti ochrany, ktoré môžete nastaviť pomocou "hesla vlastníka" v **Nastaveniach**:
* Zabránenie všetkým úpravám: Môžete nastaviť, aby nikto nemohol dokument upravovať.
* Zabránenie vyplňovania formulárov
* Zabránenie tlače dokumentu:
  * Zabránenie všetkej tlače: Môžete nastaviť heslo, aby nikto nemohol dokument tlačiť.
  * Zabránenie kvalitnej tlače: Môžete povoliť len tlač v nízkej kvalite, čím sa zabezpečí, že dokument nebude možné vytlačiť vo vysokej kvalite.
* Zabránenie kopírovaniu textu a obrázkov: Heslom môžete zabrániť používateľom, aby kopírovali text alebo obrázky z dokumentu do iných aplikácií.
* Zabránenie usporiadaniu stránok: 
  * Pridávanie stránok: Používateľ môže vkladať nové stránky do existujúceho PDF dokumentu.
  * Odstraňovanie stránok: Používateľ môže odstrániť niektoré stránky z PDF dokumentu.
  * Extrahovanie stránok: Používateľ môže vybrať určité stránky a uložiť ich ako nový samostatný PDF dokument.
  * Otočenie stránok: Používateľ môže otáčať jednotlivé stránky v dokumente.
* Zabránenie asistenčným technológiám: Môžete obmedziť používanie asistenčných technológií, ako sú obrazovkové čítačky, ktoré by inak mohli čítať obsah dokumentu pre zrakovo postihnutých používateľov.
* Zabránenie pridávaniu anotácií: Môžete obmedziť používateľov, aby nemohli pridávať komentáre, poznámky alebo anotácie k dokumentu.

Tieto možnosti ochrany zabezpečujú, že dokument zostane **chránený** pred neoprávnenými úpravami, tlačou, kopírovaním alebo extrahovaním obsahu, čím zvyšujú bezpečnosť a kontrolu nad distribúciou a používaním PDF dokumentov.

# Podpora projektu 💶
Projekt je možné finančne podporiť:
* zosnímaním [QR kódu](javascript:void(0);)<!-- This is a Markdown link -->
<script>
  document.querySelector('a').onclick = function() {
    window.open('screenshots%2Fpay-by-square.png', 'Image', 'width=600,height=400');
  };
</script>
* platbou [prevodom](https://payme.sk/?V=1&IBAN=SK1611000000002615114396&AM=5.00&CC=EUR&DT=&PI=&MSG=PDFPass&CN=) (po kliknutí na link by malo nastať automatické presmerovanie do bankovej Android/iOS aplikácie (ČSOB, Tatra banka, SLSP, VÚB) - údaje budú predvyplnené, cenu je možné upraviť (prednastavená je 5 €). V prípade, že nedojde k presmerovaniu do aplikácie, alebo nepoužívate aplikáciu zobrazi sa stranka s platobnými údajmi)
<br><br>
* platbou cez systém [PayPal](https://www.paypal.com/donate/?hosted_button_id=5G336LA7YBMXQ&locale.x=sk_SK) (cenu je potrebné zadať, ostané údaje možu byť aj fiktívne)

Váš príspevok pomôže zaplatiť **bezpečnostné aktualizácie** a rozvoj programu, ktorý zostane navždy **zadarmo**. 

# Ako nainštalovať PDFPass? ℹ️

## A) Inštalácia pomocou ```pdfpass-install.exe``` 🆕 (vyžaduje práva administrátora) 💽
**‼️Dôležité je prečítať si: [inštrukcie pred spustením](https://github.com/pdfpass/PDFPass/releases/download/2024.8.10/Precitat.pred.spustenim.instalatora.pdf)**
<br><br>
**Inštalátor umožní:**
* vybrať adresár pre PDFpass
* vytvoriť odkaz na Plochu
* pridanie odkazov do ponuky Štart
* zaregistrovať kontextové menu

### Ukážka inštalátora
![installer.png](screenshots%2Finstaller.png)

## B) "Inštalácia" + kontextové menu z ```PDFPass-portable.zip``` (vyžaduje práva administrátora) 💽

Súbor ```PDFPass-portable.zip``` stačí rozbaliť a umiestniť kdekoľvek, nie je potrebné umiestňovať do ```c:\Program Files``` alebo ```c:\Program Files (x86)```. Aktivácia kontextového menu je jednoduchá, postačuje spustiť súbor ```kontextove-menu-ako-admin-zaregistruj.cmd``` s právami administrátora (viď screenshot)

![register-menu.png](screenshots%2Fregister-menu.png)

Ak registrácia prebehne úspešne, po kliknutí pravým tlačidlom myši na súbor PDF sa zobrazí kontextová ponuka **Otvor v PDFPass** (viď screenshot)

![context-menu.png](screenshots%2Fcontext-menu.png)


## C) "Inštalácia" a odkaz na ploche pomocou ```PDFPass-portable.zip``` (bez práv administrátora) 🖥️

Súbor ```PDFPass-portable.zip``` stačí rozbaliť a umiestniť kdekoľvek. Následne pre vytvorenie odkazu na PDFPass na Ploche postačuje spustiť súbor ```pridaj-odkaz-na-plochu.cmd```

# Parametre príkazového riadku 🛠️

```
-i [cesta vstupného súboru] alebo --input [cesta vstupného súboru]

-o [cesta výstupného súboru] alebo --output [cesta výstupného súboru]

--user_pass [heslo uzmknutia čítania]

--owner_pass [heslo vlastníka]

--run - okamžite vykonať funkciu "Zahesluj" po spustení (nečakať, kým používateľ klikne na tlačidlo)
```

# Hlavná obrazovka pre nastavenia hesla 🔒
![App Screenshot](screenshots%2Fencrypt.png)

# Hlavná obrazovka pre odstránenia hesla 🔓
![App Screenshot](screenshots%2Fdecrypt.png)

# Obrazovka nastavení ⚙️
![Settings](screenshots%2Fsettings.png)


# Zmeny oproti pôvodnej verzii 🔄

* prechod na aktuálnu platformu z .NET 4.7 na .NET 8
* preklad do slovenčiny 🇸🇰
* upgrade knižnice na prácu s PDF (iText7 v8.x). Pôvodná verzia (iText7 v7.x) obsahovala zraniteľnosť. Viac na: https://devhub.checkmarx.com/cve-details/CVE-2023-6299/
* vizuálne vylepšenia


# Plánované zmeny 📅
* **funkcionalita odstránenia hesla z PDF ✅**
* podpora viacerých jazykov ⏳
* možnosť inštalácie aplikácie z Microsoft Store ⏳
* vlastná web stránka ⏳

# Licencia (sk)
Aplikácia PDFPass a zdrojový kód sú licencované pod licenciou AGPL. Aplikáciu PDFPass môžete sťahovať, inštalovať, používať a šíriť voľne pod licenciou AGPL. Zdrojový kód PDFPass môžete sťahovať, upravovať, vytvárať fork a šíriť pod licenciou AGPL.

Obmedzenia: Na označenie forkov tohto projektu nesmiete používať výraz "Official" alebo "Original" a pri forkoch musíte uviesť Gabriela Bossa (Java Guru) a PDFPass.net v rámci aplikácie a na akejkoľvek dokumentácii a/alebo súvisiacich webových stránkach.

Značka "PDFPass" je ochranná známka spoločnosti PDFPass/Gabriel Boss a nie je licencovaná pod AGPL. Ak chcete vydať rozvetvenú verziu PDFPass, musíte ju premenovať, aby nedošlo k zámene s oficiálnou vetvou.

Logo PDFPass je ochranná známka spoločnosti PDFPass/Gabriel Boss, na ktorú sa vzťahujú autorské práva a ktorá nie je licencovaná pod AGPL. Ak tento projekt forknete, musíte si vytvoriť vlastné logo, ktoré sa podstatne líši od oficiálneho loga, aby nedošlo k zámene vášho loga s oficiálnou/pôvodnou verziou.

# License (en)

The PDFPass application and source code are licensed under the AGPL.  You may download, install, use, and distribute the PDFPass application freely under the AGPL.  You may download, modify, fork, and distribute the PDFPass source code under the AGPL.

Restrictions: you may not use the term "Official" or "Original" to designate forks of this project, and forks must credit Gabriel Boss (Java Guru) and PDFPass.net both within the application and on any documentation and/or related websites.

**The "PDFPass" brand is a trademark of PDFPass/Gabriel Boss and is not licensed under the AGPL.  If you wish to release a forked version of PDFPass, you must rename it to avoid confusion with the official branch.**

**The PDFPass Logo is a trademark of, and copyrighted by, PDFPass/Gabriel Boss and is not licensed under the AGPL. If you fork this project, you must create your own logo which is materially different from the official logo, to avoid confusion between yours and the official/original version.**



