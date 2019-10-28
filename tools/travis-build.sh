#!/bin/sh

# Project Variables
version="1.0"
netConfiguration="Release"

# Tooling Path Variables
runnerSonarScanner="${TRAVIS_BUILD_DIR}/tools/sonar/SonarScanner.MSBuild.dll"
runnerCodacy="${TRAVIS_BUILD_DIR}/tools/codacy-coverage-reporter.jar"

# csprojs Path Variables
pathResourcesTests="${TRAVIS_BUILD_DIR}/tests/PoC.SharpDiff.Resources.Tests"
pathWebAPITests="${TRAVIS_BUILD_DIR}/tests/PoC.SharpDiff.WebAPI.Tests"
pathIntegrationTests="${TRAVIS_BUILD_DIR}/tests/PoC.SharpDiff.Tests"

# Start SonarScanner
if [ "$TRAVIS_PULL_REQUEST" = "false" ]; then
	dotnet $runnerSonarScanner begin /n:"${SONAR_PROJ_NAME}" /k:"${SONAR_PROJ_KEY}" /v:"$version.${TRAVIS_BUILD_NUMBER}" /d:sonar.login="${SONAR_LOGIN}" /o:"${SONAR_ORG}" /d:sonar.host.url="${SONAR_HOST}" /d:sonar.pullrequest.provider=github /d:sonar.pullrequest.github.repository=jboliveira/PoC.SharpDiff /d:sonar.cs.vstest.reportsPaths="${TRAVIS_BUILD_DIR}/tests/*.Tests/BuildReports/UnitTests/*.trx" /d:sonar.cs.xunit.reportsPaths="${TRAVIS_BUILD_DIR}/tests/*.Tests/BuildReports/UnitTests/*.xml" /d:sonar.cs.opencover.reportsPaths="$pathResourcesTests/BuildReports/Coverage/coverage.opencover.xml,$pathWebAPITests/BuildReports/Coverage/coverage.opencover.xml" /d:sonar.cs.opencover.it.reportsPaths="$pathIntegrationTests/BuildReports/Coverage/coverage.opencover.xml" /d:sonar.links.ci="${LINKS_CI}" /d:sonar.links.issue="${LINKS_ISSUE}" /d:sonar.links.scm="${LINKS_SCM}"
else
  dotnet $runnerSonarScanner begin /n:"${SONAR_PROJ_NAME}" /k:"${SONAR_PROJ_KEY}" /v:"$version.${TRAVIS_BUILD_NUMBER}" /d:sonar.login="${SONAR_LOGIN}" /o:"${SONAR_ORG}" /d:sonar.host.url="${SONAR_HOST}" /d:sonar.pullrequest.provider=github /d:sonar.pullrequest.github.repository=jboliveira/PoC.SharpDiff /d:sonar.pullrequest.branch="${TRAVIS_PULL_REQUEST_BRANCH}" /d:sonar.pullrequest.key="${TRAVIS_PULL_REQUEST}" /d:sonar.pullrequest.base="master" /d:sonar.cs.vstest.reportsPaths="${TRAVIS_BUILD_DIR}/tests/*.Tests/BuildReports/UnitTests/*.trx" /d:sonar.cs.xunit.reportsPaths="${TRAVIS_BUILD_DIR}/tests/*.Tests/BuildReports/UnitTests/*.xml" /d:sonar.cs.opencover.reportsPaths="$pathResourcesTests/BuildReports/Coverage/coverage.opencover.xml,$pathWebAPITests/BuildReports/Coverage/coverage.opencover.xml" /d:sonar.cs.opencover.it.reportsPaths="$pathIntegrationTests/BuildReports/Coverage/coverage.opencover.xml" /d:sonar.links.ci="${LINKS_CI}" /d:sonar.links.issue="${LINKS_ISSUE}" /d:sonar.links.scm="${LINKS_SCM}"
fi

# Build
dotnet build -c $netConfiguration -p:Version=$version.${TRAVIS_BUILD_NUMBER} --force

# Test & Code Coverage
dotnet test $pathResourcesTests/PoC.SharpDiff.Resources.Tests.csproj -c $netConfiguration --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory $pathResourcesTests/BuildReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=$pathResourcesTests/BuildReports/Coverage/ "/p:CoverletOutputFormat=\"cobertura,opencover\"" /p:Exclude="[xunit.*]*"
dotnet test $pathWebAPITests/PoC.SharpDiff.WebAPI.Tests.csproj -c $netConfiguration --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory $pathWebAPITests/BuildReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=$pathWebAPITests/BuildReports/Coverage/ "/p:CoverletOutputFormat=\"cobertura,opencover\"" /p:Exclude="[xunit.*]*"
dotnet test $pathIntegrationTests/PoC.SharpDiff.Tests.csproj -c $netConfiguratio --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory $pathIntegrationTests/BuildReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=$pathIntegrationTests/BuildReports/Coverage/ "/p:CoverletOutputFormat=\"cobertura,opencover\"" /p:Exclude="[xunit.*]*"

# Send Coverage to Codacy
java -jar $runnerCodacy report --language CSharp --force-language -r $pathResourcesTests/BuildReports/Coverage/coverage.cobertura.xml --partial
java -jar $runnerCodacy report --language CSharp --force-language -r $pathWebAPITests/BuildReports/Coverage/coverage.cobertura.xml --partial
java -jar $runnerCodacy report --language CSharp --force-language -r $pathIntegrationTests/BuildReports/Coverage/coverage.cobertura.xml --partial
java -jar $runnerCodacy final

# End SonarScanner
dotnet $runnerSonarScanner end /d:sonar.login="${SONAR_LOGIN}"