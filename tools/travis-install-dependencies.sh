#!/bin/sh

# Install SonarQube Scanner for MSBuild
wget -O sonar.zip https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/4.7.1.2311/sonar-scanner-msbuild-4.7.1.2311-netcoreapp2.0.zip
unzip -qq sonar.zip -d tools/sonar
ls -l tools/sonar
chmod +x tools/sonar/sonar-scanner-4.1.0.1829/bin/sonar-scanner

# Install Codacy Coverage Reporter
wget -O ${TRAVIS_BUILD_DIR}/tools/codacy-coverage-reporter.jar https://github.com/codacy/codacy-coverage-reporter/releases/download/6.0.6/codacy-coverage-reporter-6.0.6-assembly.jar
chmod +x ${TRAVIS_BUILD_DIR}/tools/codacy-coverage-reporter.jar
