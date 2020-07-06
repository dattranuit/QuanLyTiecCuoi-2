@echo off

set "file1=%a%\Database.mdf"
set "file2=%a%\Database_log.ldf"
if not "%1"=="am_admin" (powershell start -verb runas '%0' am_admin & exit /b)
icacls "%file1%" /grant:r %USERNAME%:(ga)
echo %file1%
icacls "%file2%" /grant:r %USERNAME%:(ga)