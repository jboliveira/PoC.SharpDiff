#!/bin/sh

# Install SonarQube Scanner for MSBuild
wget -O sonar.zip https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/4.6.1.2049/sonar-scanner-msbuild-4.6.1.2049-netcoreapp2.0.zip
unzip -qq sonar.zip -d tools/sonar
ls -l tools/sonar
chmod +x tools/sonar/sonar-scanner-3.3.0.1492/bin/sonar-scanner

# Install Codacy Coverage Reporter
wget -O ${TRAVIS_BUILD_DIR}/tools/codacy-coverage-reporter.jar https://github.com/codacy/codacy-coverage-reporter/releases/download/6.0.0/codacy-coverage-reporter-6.0.0-assembly.jar
chmod +x ${TRAVIS_BUILD_DIR}/tools/codacy-coverage-reporter.jar
