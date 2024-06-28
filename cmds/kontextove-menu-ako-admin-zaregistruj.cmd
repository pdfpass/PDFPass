@echo off
net session >nul 2>&1
if %errorlevel% == 0 (
    echo Mate administratorke prava. Registracia pokracuje.
) else (
    echo UPOZORNENIE: Subor nebol spusteny s pravami administratora. Restartujte s pozadovanymi pravami.
    pause
    exit
)
REM !!! ***** ZMENIŤ PRE SPUSTENIM ****** !!!
SET OWNER_PASSWORD=mojeHESLO#2024

SET EXT=pdf
SET ACTION=PDFPass.PDF
SET MENU_TEXT="Otvor v PDFPass"
SET EXE_FILE=%~dp0PDFPass.exe
SET COMMAND=\"%EXE_FILE%\" -i \"%%1\" --owner_pass \"%OWNER_PASSWORD%\"

SET REG_DIR=HKCR\SystemFileAssociations\.%EXT%\shell\%ACTION%
REG ADD %REG_DIR% /f /ve /t REG_EXPAND_SZ /d %MENU_TEXT%
REG ADD "%REG_DIR%" /f /v Icon  /t REG_EXPAND_SZ /d "\"%EXE_FILE%\""
REG ADD "%REG_DIR%\command" /f /ve /t REG_EXPAND_SZ /d "%COMMAND%"
