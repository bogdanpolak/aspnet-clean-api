rem (ExecuteOnce): dotnet tool install --global dotnet-reportgenerator-globaltool

rmdir "UnitTests\TestResults" /s /q
rmdir "CodeCoverage\Html" /s /q
dotnet test --collect:"XPlat Code Coverage" --settings "CodeCoverage\coverlet.runsettings"
reportgenerator -reports:UnitTests/TestResults/**/coverage.cobertura.xml -targetdir:"CodeCoverage\Html" -reporttypes:Html
rmdir "UnitTests\TestResults" /s /q
pause