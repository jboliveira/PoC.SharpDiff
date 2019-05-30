ARG DOCKER_VERSION=latest
FROM docker:${DOCKER_VERSION}

RUN docker build /src/PoC.SharpDiff.WebAPI/Dockerfile .
