@echo off
setlocal

SET "REG_DIR=HKCU\Software\Classes\SystemFileAssociations\.pdf\shell\PDFPass.PDF"
REG QUERY "%REG_DIR%" >nul 2>&1 || exit /b 0
REG DELETE "%REG_DIR%" /f
