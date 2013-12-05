echo off
cls
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727
set DOTNETFX4=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX4%;%DOTNETFX2%
echo Installing Service ... 
echo ---------------------------------------------------
InstallUtil /i Fanex.Affiliate.Services.WinService.exe
echo ---------------------------------------------------
echo Done.
pause