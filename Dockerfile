FROM docker/compose:1.24.0 AS base

RUN apt-get update && apt-get install -y curl
RUN sudo curl -L "https://github.com/docker/compose/releases/download/1.24.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
RUN sudo chmod +x /usr/local/bin/docker-compose
RUN docker-compose build
