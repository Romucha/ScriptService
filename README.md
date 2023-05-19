# ScriptService - a set services for storing and managing scripts
**ScriptService** is a Blazor WebAssembly and ASP.NET Core applications and Postgres database. It serves the purpose of managing repository of scripts.

With the help of **ScriptService** user can:
- create database repository;
- add new scripts;
- edit existing scripts;
- delete scripts.

Additionally, it can also be deployed in docker containers.

**ScriptService** requires .NET 6 to run.

# Components

## ScriptService.API

**ScriptService.API** is ASP.NET Core web application. 

It accepts http requests using REST API, communicates with database and returns the result in form of json objects.

**ScriptService.API** provides authorization in form of JWT tokens, and anonymous users can only perform GET requests.

## ScriptService.App

**ScriptService.App** is a Blazor WebAssembly application. It provides graphical interface for user to interact with.

The Docker container of **ScriptService.App** is made from nginx image with files of the application copied into it.

## Docker-compose

**docker-compose** is an orchestrator project. It allows user to launch the whole system inside of Docker environment.

## Component Library

**ScriptService.ComponentLibrary** is a Blazor component project. It contains script management interface and services that connect to **ScriptService.API**.
This project is separated from WebAssembly application and can be used in any other kind of project that is capable of displaying web pages.