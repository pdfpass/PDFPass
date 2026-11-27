@echo off
setlocal DisableDelayedExpansion

SET EXT=pdf
SET ACTION=PDFPass.PDF
SET MENU_TEXT="Open with PDFPass"
SET "EXE_FILE=%~dp0PDFPass.exe"
setlocal EnableDelayedExpansion

SET "REG_DIR=HKCU\Software\Classes\SystemFileAssociations\.%EXT%\shell\%ACTION%"
REG ADD "%REG_DIR%" /f /ve /d %MENU_TEXT% || exit /b 1
REG ADD "%REG_DIR%" /f /v Icon /d "\"!EXE_FILE!\"" || exit /b 1
REG ADD "%REG_DIR%\command" /f /ve /d "\"!EXE_FILE!\" -i \"%%1\""
