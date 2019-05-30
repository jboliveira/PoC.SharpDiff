FROM microsoft/nanoserver
RUN docker build -f src/PoC.SharpDiff.WebAPI/Dockerfile .
