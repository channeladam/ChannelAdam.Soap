SET msbuild="C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"

%msbuild% ..\src\ChannelAdam.Soap\ChannelAdam.Soap.csproj /t:Rebuild /p:Configuration=Release;OutDir=bin\Release

..\tools\nuget\nuget.exe pack ..\src\ChannelAdam.Soap\ChannelAdam.Soap.nuspec -Symbols

pause
