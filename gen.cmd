@echo off

setlocal

cd /d %~dp0

dotnet tool run ClangSharpPInvokeGenerator -f include\biscuit_auth.h ^
    --namespace us.awise.biscuits.generated ^
    -o biscuit-csharp\generated\generated.cs ^
    -x c -l biscuit_auth ^
    --with-access-specifier *=Internal
