![ref-card.jpg](screenshots%2Fref-card.jpg)

# PDFPass (SK)  [PDFPass (EN)⇩ ](#PDFPass-EN)
**PDFPass** je **bezplatný** offline (nepotrebuje a nevyžaduje internetové pripojenie) nástroj s otvoreným zdrojovým kódom v jazyku C# na rýchle a jednoduché **šifrovanie/heslovanie** súborov PDF, ako aj **odstraňovanie** hesiel a pridanie vodoznaku. 

Toto je oficiálna verzia PDFPass založená na projekte _PDFEncrypt.net/Ryan Griggs_. Upozorňujeme, že všetky ostatné verzie sú klony a nemusia rešpektovať alebo dodržiavať základné zásady ochrany súkromia a slobody, ktoré zastáva autor projektu.

Kontakt: **pdf.pass@outlook.com**

## Súbor na stiahnutie 💾
Aktuálny ```PDFPass-portable.zip``` alebo ```PDFPass-install.exe``` nájdete tu: [stiahnuť](https://github.com/pdfpass/PDFPass/releases/latest)

Nové balíky po migrácii používajú .NET 10 a sú iba pre x64; samostatná inštalácia runtime nie je potrebná. Windows 7/8 nie sú podporované. Používajte podporovaný Windows 11 alebo servisovanú edíciu Windows 10 podporovanú platformou .NET 10. Staré x86 vydania sú historické, nie udržiavaná alternatíva. Migrácia sa zverejní až po manuálnom overení a samostatnom zvýšení verzie.
<br>
<br>

# Hlavná obrazovka pre nastavenia hesla 🔒
![App Screenshot](screenshots%2Fencrypt.png)

# Hlavná obrazovka pre odstránenia hesla 🔓
![App Screenshot](screenshots%2Fdecrypt.png)

# Obrazovka nastavení ⚙️
![Settings](screenshots%2Fsettings.png)


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
Projekt je možné finančne podporiť platbou cez systém [PayPal](https://www.paypal.com/donate/?hosted_button_id=5G336LA7YBMXQ&locale.x=sk_SK)
1. priamo pomocou platby z účtu PayPal 🎯
2. platobnou kartou 💳
     

  * _pri platbe kartou môžu byť z dôvodu anonymity osobné údaje fiktívne_<br>
  ![paypal.jpg](screenshots%2Fpaypal.jpg)


Váš príspevok pomôže zaplatiť **bezpečnostné aktualizácie** a rozvoj programu, ktorý zostane navždy **zadarmo**. 

# Ako nainštalovať PDFPass? ℹ️

## A) Inštalácia pomocou ```PDFPass-install.exe``` 🆕 (vyžaduje práva administrátora) 💽

**‼️Dôležité je prečítať
si:  [inštrukcie pred spustením](others/Pre%C4%8D%C3%ADta%C5%A5%20pred%20spusten%C3%ADm%20in%C5%A1tal%C3%A1tora.pdf)**
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



# Zmeny oproti pôvodnej verzii 🔄

* prechod na aktuálnu platformu z .NET 4.7 ➔ .NET 10
* preklad do slovenčiny 🇸🇰
* upgrade knižnice na prácu s PDF (iText7 v8.x). Pôvodná verzia (iText7 v7.x) obsahovala zraniteľnosť. Viac na: https://devhub.checkmarx.com/cve-details/CVE-2023-6299/
* vizuálne vylepšenia


# Plánované zmeny 📅
* **funkcionalita odstránenia hesla z PDF ✅**
* **funkcionalita vodotlače ✅**
* **podpora "Drag & Drop" pre vstupný súbor ✅**
* **podpora viacerých jazykov ✅**
  ![drag-and-drop.gif](screenshots%2Fdrag-and-drop.gif)
* možnosť inštalácie aplikácie z Microsoft Store ⏳
* vlastná web stránka ⏳

# Licencia (sk)
Aplikácia PDFPass a zdrojový kód sú licencované pod licenciou AGPL. Aplikáciu PDFPass môžete sťahovať, inštalovať, používať a šíriť voľne pod licenciou AGPL. Zdrojový kód PDFPass môžete sťahovať, upravovať, vytvárať fork a šíriť pod licenciou AGPL.

Obmedzenia: Na označenie forkov tohto projektu nesmiete používať výraz "Official" alebo "Original" a pri forkoch musíte uviesť Gabriela (a.k.a. Java Guru) a pdfpass.app v rámci aplikácie a na akejkoľvek dokumentácii a/alebo súvisiacich webových stránkach.

Značka "PDFPass" je ochranná známka spoločnosti PDFPass/Gabriel a nie je licencovaná pod AGPL. Ak chcete vydať rozvetvenú verziu PDFPass, musíte ju premenovať, aby nedošlo k zámene s oficiálnou vetvou.

Logo PDFPass je ochranná známka spoločnosti PDFPass/Gabriel, na ktorú sa vzťahujú autorské práva a ktorá nie je licencovaná pod AGPL. Ak tento projekt forknete, musíte si vytvoriť vlastné logo, ktoré sa podstatne líši od oficiálneho loga, aby nedošlo k zámene vášho loga s oficiálnou/pôvodnou verziou.

<br><br><br>

# PDFPass EN
![us-flag.jpg](screenshots/us-flag.jpg)

**PDFPass** is a **free**, offline (does not need or require an internet connection), open-source tool written in C# for quickly and easily **encrypting/password-protecting** PDF files, as well as **removing** passwords and adding watermarks.

This is the official version of PDFPass based on the _PDFEncrypt.net/Ryan Griggs_ project. Please note that all other versions are clones and may not respect or adhere to the fundamental principles of privacy and freedom upheld by the project's author.

Contact: **pdf.pass@outlook.com**

## Download 💾
You can find the current ```PDFPass-portable.zip``` or ```PDFPass-install.exe``` here: [download](https://github.com/pdfpass/PDFPass/releases/latest)
<br>
<br>

# Main screen for setting passwords 🔒
![App Screenshot](screenshots%2Fencrypt-en.png)

# Main screen for removing passwords 🔓
![App Screenshot](screenshots%2Fdecrypt-en.png)

# Settings screen ⚙️
![Settings](screenshots%2Fsettings-en.png)


# PDF - supports 2 types of passwords
Both types can be set/removed using **PDFPass**

## 1. Password to lock reading (user password) 🔑

* Protection of sensitive information: If a PDF document contains **sensitive** or **confidential** information, it is advisable to protect it with a password to prevent unauthorized access.

* Distribution among a limited number of people: If the PDF is intended for distribution only among certain individuals or a group of people, a password ensures that the document can only be opened by those who know the password.

* Legal or regulatory requirements: In some cases, the requirement to protect documents with a password may be dictated by legal regulations or internal organizational rules.

## 2. Owner password 🔑
This password provides various protection options that allow you to control what users can do with the document. Here are the main protection options you can set using the "owner password" in **Settings**:

* Preventing all editing: You can set it so that no one can edit the document.
* Preventing form filling
* Preventing document printing:
    * Preventing all printing: You can set a password so that no one can print the document.
    * Preventing high-quality printing: You can allow only low-quality printing, ensuring that the document cannot be printed in high resolution.
* Preventing copying of text and images: You can use a password to prevent users from copying text or images from the document to other applications.
* Preventing page arrangement:
    * Adding pages: The user can insert new pages into an existing PDF document.
    * Deleting pages: The user can delete some pages from the PDF document.
    * Extracting pages: The user can select specific pages and save them as a new, separate PDF document.
    * Rotating pages: The user can rotate individual pages in the document.
* Preventing assistive technologies: You can restrict the use of assistive technologies, such as screen readers, which could otherwise read the document's content for visually impaired users.
* Preventing adding annotations: You can restrict users from adding comments, notes, or annotations to the document.

These protection options ensure that the document remains **protected** from unauthorized modifications, printing, copying, or extraction of content, thereby increasing the security and control over the distribution and use of PDF documents.

# Project Support 💶
You can financially support the project with a payment through the [PayPal](https://www.paypal.com/donate/?hosted_button_id=5G336LA7YBMXQ&locale.x=sk_SK) system:

1.  Directly using a PayPal account payment 🎯
2.  By credit card 💳

    * _For credit card payments, personal data can be fictitious for anonymity reasons_<br>
      ![paypal-en.png](screenshots%2Fpaypal-en.png)

Your contribution will help pay for **security updates** and the development of the program, which will remain **free forever**.

# How to install PDFPass? ℹ️

## A) Installation with ```PDFPass-install.exe``` 🆕 (requires administrator privileges) 💽

**‼️ It is important to read: [Instructions before running](others/Read%20before%20running%20Installer.pdf)**
<br><br>
**The installer will allow you to:**

* Select a directory for PDFPass
* Create a desktop shortcut
* Add shortcuts to the Start Menu
* Register the context menu

### Installer preview
![installer.png](screenshots%2Finstaller.png)

## B) "Installation" + context menu from ```PDFPass-portable.zip``` (requires administrator privileges) 💽

You only need to unzip the ```PDFPass-portable.zip``` file and place it anywhere; it does not need to be placed in ```c:\Program Files``` or ```c:\Program Files (x86)```. Activating the context menu is simple; just run the ```kontextove-menu-ako-admin-zaregistruj.cmd``` file with administrator privileges (see screenshot).

![register-menu.png](screenshots%2Fregister-menu-en.png)

If the registration is successful, after right-clicking on a PDF file, the context menu **Open in PDFPass** will appear (see screenshot).

![context-menu.png](screenshots%2Fcontext-menu-en.png)

## C) "Installation" and desktop shortcut using ```PDFPass-portable.zip``` (without administrator privileges) 🖥️

You only need to unzip the ```PDFPass-portable.zip``` file and place it anywhere. Then, to create a PDFPass shortcut on the desktop, simply run the ```pridaj-odkaz-na-plochu.cmd``` file.

# Command Line Parameters 🛠️

```
-i [path to input file] or --input [path to input file]

-o [path to output file] or --output [path to output file]

--user_pass [reading lock password]

--owner_pass [owner password]

--run - immediately execute the "Encrypt" function after startup (do not wait for the user to click the button)
```

# Changes from the original version 🔄

* Transition to the current platform from .NET 4.7 ➔ .NET 10
* Translation to Slovak 🇸🇰
* Upgrade of the PDF processing library (iText7 v8.x). The original version (iText7 v7.x) contained a vulnerability. More at: https://devhub.checkmarx.com/cve-details/CVE-2023-6299/
* Visual improvements

# Planned Changes 📅

* **PDF password removal functionality ✅**
* **Watermark functionality ✅**
* **"Drag & Drop" support for input file ✅**
* **Support for multiple languages ✅**
    ![drag-and-drop.gif](screenshots%2Fdrag-and-drop-en.gif)
* Option to install the application from the Microsoft Store ⏳
* Dedicated website ⏳


## Development And Releases

The application targets .NET 10 LTS (`net10.0-windows`). Install the exact SDK pinned in `global.json` (currently `10.0.404`); preview SDKs and automatic roll-forward are disabled. New portable and installer packages are **x64-only**, self-contained, and do not require a separately installed .NET runtime. Use supported Windows 11 or a serviced Windows 10 edition supported by .NET 10. The installer's Windows 10 build 14393 minimum is a runtime floor, not a guarantee that every Windows 10 edition remains supported. Windows 7/8 are unsupported. Historical x86 releases remain downloadable but are not a maintained fallback. Native ARM64 packages are not provided.

```powershell
dotnet restore PDFPass.sln --locked-mode
dotnet build PDFPass.sln -c Debug --no-restore
dotnet run --project src/PDFPass/PDFPass.csproj
```

For CI-equivalent self-contained publishing, restore with the same properties before publishing without a second restore:

```powershell
dotnet restore src/PDFPass/PDFPass.csproj -r win-x64 --locked-mode -p:SelfContained=true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
dotnet publish src/PDFPass/PDFPass.csproj -r win-x64 -c Release --self-contained true -p:DebugType=none -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true --no-restore -o bin/Release
```

Keep `src/PDFPass/packages.lock.json` committed. Regenerate it intentionally with these restore properties and `--force-evaluate` when dependencies change. There is no executable application unit-test project; package checks do not demonstrate PDF/GUI coverage.

The application workflow runs only on pushes to `master`; pull requests, including fork PRs, do not trigger builds. Master builds validate the full publish, portable ZIP contents/bytes, apphost architecture/version, and Inno installer. The complete `release-assets` artifact is retained for 14 days and includes `SHA256SUMS.txt`. After a successful `master` push, the version in `src/PDFPass/Properties/AssemblyInfo.cs` alone controls automatic publication. Already published versions are skipped unchanged. New releases are created as drafts, verified, and only then published as latest. A failed draft requires maintainer review and explicitly authorized cleanup before retrying; published assets and tags must never be replaced or moved. Checksums verify integrity, not signing or publisher identity.

Migration rollout must not bump the existing `2026.13.0` version. Download the artifact from the successful master build and manually validate portable/installed launch on clean Windows VMs without a separate runtime, PDF operations, drag/drop, password paste, language/settings persistence, Unicode/spaced paths, shortcuts/context menu, upgrade and uninstall. Preserve existing HKCU preferences and owner password; do not run registration/uninstall scripts on the developer's main machine. Only after acceptance should a separate AssemblyVersion bump trigger the first .NET 10/x64 public release, with migration information in its release notes. Untested platforms must be reported as untested.

Dependabot proposes weekly grouped Actions and NuGet updates without auto-merge; SDK and Inno pins need explicit maintenance review. Master runs do not cancel in progress. Concurrency is not a durable queue for every intermediate push, so complete one master version-bump run before the next. Maintainers should ensure branch rules do not require a PR build check that will never run, and review default token permissions without silently adding a new approval policy.

Before rollout, revoke/rotate any PAT previously embedded in a local remote URL, use credential-free HTTPS with an approved credential manager, and restrict/redact retained output. Workflow publication uses only the short-lived `GITHUB_TOKEN`, never a PAT.

# License (en)

The PDFPass application and source code are licensed under the AGPL.  You may download, install, use, and distribute the PDFPass application freely under the AGPL.  You may download, modify, fork, and distribute the PDFPass source code under the AGPL.

Restrictions: you may not use the term "Official" or "Original" to designate forks of this project, and forks must credit Gabriel (Java Guru) and pdfpass.app both within the application and on any documentation and/or related websites.

**The "PDFPass" brand is a trademark of PDFPass/Gabriel and is not licensed under the AGPL.  If you wish to release a forked version of PDFPass, you must rename it to avoid confusion with the official branch.**

**The PDFPass Logo is a trademark of, and copyrighted by, PDFPass/Gabriel and is not licensed under the AGPL. If you fork this project, you must create your own logo which is materially different from the official logo, to avoid confusion between yours and the official/original version.**
```
