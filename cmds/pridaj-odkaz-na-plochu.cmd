@echo off
set TARGET_PATH=%~dp0PDFPass.exe
set SHORTCUT_NAME=PDFPass
set SHORTCUT_PATH=%USERPROFILE%\Desktop\%SHORTCUT_NAME%.lnk

powershell -command "$ws = New-Object -ComObject WScript.Shell; $s = $ws.CreateShortcut('%SHORTCUT_PATH%'); $s.TargetPath = '%TARGET_PATH%'; $s.Save()"

echo Shortcut created on the desktop.
pause
