SET DEST_DIR="c:\Users\admin\Desktop\PDFPass-portable" 
REM ************************

rmdir /s /q %DEST_DIR%

call dotnet publish -r win-x86 -c Release -p:DebugType=none --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true -o "c:\Users\admin\Desktop\PDFPass-portable"

cd c:\Users\admin\Desktop
copy "c:\java\utils\myUtils\PDFPass-portable\kontextove-menu-ako-admin-registruj.cmd" "c:\Users\admin\Desktop\PDFPass-portable"
copy "c:\java\utils\myUtils\PDFPass-portable\kontextove-menu-ako-admin-odregistruj.cmd" "c:\Users\admin\Desktop\PDFPass-portable"

7z a PDFPass-portable.zip PDFPass-portable

copy   ""
robocopy "%USERPROFILE%\Desktop" "H:/box/Downloads" PDFPass-portable.zip /eta /v /r:0
robocopy "%USERPROFILE%\Desktop\PDFPass-portable" "%MY_UTILS%/PDFPass-portable" /eta /v /r:0

rmdir /s /q %DEST_DIR%

del "C:\Users\Admin\Desktop\PDFPass-portable.zip"