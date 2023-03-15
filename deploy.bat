@echo off

dotnet.exe publish CreateShortcut.csproj --no-self-contained ^
-o C:\IVI\Apps\Scripts\General\CreateShortcut -c Release -r win-x64 /p:PublishSingleFile=true
