@echo off
net session >nul 2>&1
if %errorlevel% == 0 (
    echo Mate administratorke prava. Registracia pokracuje.
) else (
    echo UPOZORNENIE: Subor nebol spusteny s pravami administratora. Restartujte s pozadovanymi pravami.
    pause
    exit
)

REG DELETE HKEY_CLASSES_ROOT\SystemFileAssociations\.pdf\shell\PDFPass.PDF /f
