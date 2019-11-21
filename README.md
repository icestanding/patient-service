# PatientService
    A coding test implement a micro service for Patient data

# Installation instruction
    1. Please Clone this Repo to local
    2. In Repo directory 
        run  'dotnet build' to build up service
    3. In CodingChallange.Services.Patient.ConsoleApp.Host directory
        run  'dotnet run' to run the Patient Service

# Run Unit Tests
    In Repo directory 
        Run 'dotnet test' to run all unit test

# Third party libaries 
    1. AutoMapper
        For mapping between ViewModel and Model
    2. Sieve
        A pagination provider to help to service side custmise pagination 
    3. Moq
       For mocking components for unit tests

# Deisgn & Implementation
    Basically set up Controller, Manager and Repository layer to build up the whole service
        Contoller - mainly resposible for handling request validation and model mapping
        Manager layer - a glue layer for connect Controller layer to Repository, complex logic has implemented in this layer
        Repositroy layer - for DB operation
    All the controller level mapping using AutoMapper to keep consistency
    For serice side pagination, using Sieve to set up, implement extension pagination functions to help to make it generic
 
# Future improvement
   Due to time limit, more quality unit tests could be implemented in the future


