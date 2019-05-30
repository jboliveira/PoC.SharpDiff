ARG DOCKER_VERSION=latest
FROM docker:${DOCKER_VERSION}

RUN docker-compose build
