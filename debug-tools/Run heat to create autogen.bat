@echo OFF
del "%~dp0autogenOutput.wxs"
echo ## Heat.exe from WiX Toolset does not support x64 dlls.
echo ## But its output from a x86 dll will work fine for both x64/x86 installers
echo.
echo ## Make sure that you've built the x86/AnyCPU debug binary :)
echo.
"C:\Program Files (x86)\WiX Toolset v3.8\bin\heat.exe" file "%~dp0..\WiimoteAddin\bin\Debug\WiimoteAddin.dll" -o "%~dp0autogenOutput.wxs"
echo.
echo ## Wrote to "%~dp0autogenOutput.wxs"
notepad "%~dp0autogenOutput.wxs"