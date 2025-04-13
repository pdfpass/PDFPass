@echo off
net session >nul 2>&1
if %errorlevel% == 0 (
    echo You have admin rights. The registration will continue...
) else (
    echo WARNING: Subor has not been started with administrator rights. Run with admin permissions.
    pause
    exit
)

REG DELETE HKEY_CLASSES_ROOT\SystemFileAssociations\.pdf\shell\encrypt.2.PDF /f
REG DELETE HKEY_CLASSES_ROOT\SystemFileAssociations\.pdf\shell\PDFPass.PDF /f
