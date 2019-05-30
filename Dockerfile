FROM docker/compose:1.24.0 AS base

RUN docker-compose build
