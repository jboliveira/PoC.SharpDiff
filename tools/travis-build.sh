#!/bin/sh

# Variables
runnerSonarScanner="${TRAVIS_BUILD_DIR}/tools/sonar/SonarScanner.MSBuild.dll"
runnerCodeCoverage="${TRAVIS_BUILD_DIR}/tools/codecoverage/Microsoft.CodeCoverage.16.1.0/build/netstandard1.0/CodeCoverage/CodeCoverage.exe"
pathResourcesTests="${TRAVIS_BUILD_DIR}/tests/PoC.SharpDiff.Resources.Tests"
pathWebAPITests="${TRAVIS_BUILD_DIR}/tests/PoC.SharpDiff.WebAPI.Tests"
pathIntegrationTests="${TRAVIS_BUILD_DIR}/tests/PoC.SharpDiff.Tests"

# Start SonarScanner
if [ "$TRAVIS_PULL_REQUEST" = "false" ]; then
	dotnet $runnerSonarScanner begin /n:"${SONAR_PROJ_NAME}" /k:"${SONAR_PROJ_KEY}" /v:"1.0.${TRAVIS_BUILD_NUMBER}" /d:sonar.login="${SONAR_LOGIN}" /o:"${SONAR_ORG}" /d:sonar.host.url="${SONAR_HOST}" /d:sonar.branch.name="${TRAVIS_REPO_SLUG}" /d:sonar.cs.vstest.reportsPaths="${TRAVIS_BUILD_DIR}/tests/*.Tests/BuildReports/UnitTests/*.trx" /d:sonar.cs.xunit.reportsPaths="${TRAVIS_BUILD_DIR}/tests/*.Tests/BuildReports/UnitTests/*.xml" /d:sonar.cs.opencover.reportsPaths="$pathResourcesTests/BuildReports/Coverage/coverage.opencover.xml,$pathWebAPITests/BuildReports/Coverage/coverage.opencover.xml" /d:sonar.cs.opencover.it.reportsPaths="$pathIntegrationTests/BuildReports/Coverage/coverage.opencover.xml" /d:sonar.links.ci="${LINKS_CI}" /d:sonar.links.issue="${LINKS_ISSUE}" /d:sonar.links.scm="${LINKS_SCM}" /d:sonar.showProfiling=true /d:sonar.verbose=true
else
    dotnet $runnerSonarScanner begin /n:"${SONAR_PROJ_NAME}" /k:"${SONAR_PROJ_KEY}" /v:"1.0.${TRAVIS_BUILD_NUMBER}" /d:sonar.login="${SONAR_LOGIN}" /o:"${SONAR_ORG}" /d:sonar.host.url="${SONAR_HOST}" /d:sonar.pullrequest.branch="${TRAVIS_PULL_REQUEST_BRANCH}" /d:sonar.pullrequest.key="${TRAVIS_PULL_REQUEST}" /d:sonar.pullrequest.base="${TRAVIS_PULL_REQUEST_SLUG}" /d:sonar.cs.vstest.reportsPaths="${TRAVIS_BUILD_DIR}/tests/*.Tests/BuildReports/UnitTests/*.trx" /d:sonar.cs.xunit.reportsPaths="${TRAVIS_BUILD_DIR}/tests/*.Tests/BuildReports/UnitTests/*.xml" /d:sonar.cs.opencover.reportsPaths="$pathResourcesTests/BuildReports/Coverage/coverage.opencover.xml,$pathWebAPITests/BuildReports/Coverage/coverage.opencover.xml" /d:sonar.cs.opencover.it.reportsPaths="$pathIntegrationTests/BuildReports/Coverage/coverage.opencover.xml" /d:sonar.cs.vscoveragexml.it.reportsPaths="$pathIntegrationTests/BuildReports/Coverage/coverage.cobertura.xml" /d:sonar.links.ci="${LINKS_CI}" /d:sonar.links.issue="${LINKS_ISSUE}" /d:sonar.links.scm="${LINKS_SCM}" /d:sonar.showProfiling=true /d:sonar.verbose=true
fi

# Build
dotnet build --configuration Release -p:Version=1.0.${TRAVIS_BUILD_NUMBER}

# Test & Code Coverage
dotnet test $pathResourcesTests/PoC.SharpDiff.Resources.Tests.csproj --configuration Release --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory $pathResourcesTests/BuildReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=$pathResourcesTests/BuildReports/Coverage/ /p:CoverletOutputFormat=opencover /p:Exclude="[xunit.*]*"

dotnet test $pathWebAPITests/PoC.SharpDiff.WebAPI.Tests.csproj --configuration Release --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory $pathWebAPITests/BuildReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=$pathWebAPITests/BuildReports/Coverage/ /p:CoverletOutputFormat=opencover /p:Exclude="[xunit.*]*"

dotnet test $pathIntegrationTests/PoC.SharpDiff.Tests.csproj --configuration Release --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory $pathIntegrationTests/BuildReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=$pathIntegrationTests/BuildReports/Coverage/ /p:CoverletOutputFormat=opencover /p:Exclude="[xunit.*]*"

# End SonarScanner
dotnet $runnerSonarScanner end /d:sonar.login="${SONAR_LOGIN}"