@echo off

dotnet.exe publish CreateShortcut.csproj --self-contained -o C:\IVI\Apps\Scripts\General\CreateShortcut -c Release -r win-x64 ^
/p:PublishTrimmed=true /p:TrimMode=Link /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true
