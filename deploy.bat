@echo off

dotnet publish CreateShortcut.csproj --self-contained ^
-o C:\IVI\Apps\Scripts\General\CreateShortcut -c Release -r win-x64 /p:PublishSingleFile=true
