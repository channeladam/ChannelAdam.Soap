@echo off

set packageName=ChannelAdam.Soap

set /p version="What is the version you want to push?"

..\tools\nuget\nuget.exe push "%packageName%.%version%.nupkg"

pause
