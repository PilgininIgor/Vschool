@echo off
:: The available verbosity levels are q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic]
set verbosity=minimal
set buildfile=deploy.proj
set configuration=Photon

:start
cls
echo.
echo            ************************************************************
echo            *                     Build Prompt                         *
echo            ************************************************************
echo.
echo            Build and Copy Server Binaries To Deploy Folder
echo            Configuration: %configuration%
echo            Buildfile:     %buildfile%
echo.
echo            1.  Start
echo.
echo            0.  Exit
echo.

:begin
IF NOT EXIST .\log\ MD .\log
set eof=
set choice=
set /p choice=Enter option 
if not '%choice%'=='' set choice=%choice:~0,1%
if '%choice%'=='1' call :loadbalancing
if '%choice%'=='0' goto eof
if '%eof%'=='' ECHO "%choice%" is not valid please try again
if '%eof%'=='' goto begin
pause
goto start

:loadbalancing
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe %buildfile% /verbosity:%verbosity% /l:FileLogger,Microsoft.Build.Engine;logfile=log\Vschool.log;verbosity=%verbosity%;performancesummary /property:Configuration="%configuration%" /t:BuildVschool
goto done

:done
:eof
set eof=1
