@echo off
net session >nul 2>&1
if %errorlevel% == 0 (
    echo Mate administratorke prava. Registracia pokracuje.
) else (
    echo UPOZORNENIE: Subor nebol spusteny s pravami administratora. Restartujte s pozadovanymi pravami.
    pause
    exit
)

SET EXT=pdf
SET ACTION=encrypt.2.PDF
SET MENU_TEXT="Otvor v PDFPass"
SET EXE_FILE=%~dp0PDFPass.exe
SET COMMAND=%EXE_FILE%\" -i \"%%1\"

SET REG_DIR=HKCR\SystemFileAssociations\.%EXT%\shell\%ACTION%
REG ADD %REG_DIR% /f /ve /t REG_EXPAND_SZ /d %MENU_TEXT%
REG ADD "%REG_DIR%" /f /v Icon  /t REG_EXPAND_SZ /d "\"%EXE_FILE%\""
REG ADD "%REG_DIR%\command" /f /ve /t REG_EXPAND_SZ /d "\"%COMMAND%\""
