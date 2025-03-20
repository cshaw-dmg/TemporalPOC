# Job Worker

The **JobWorker** project demonstrates the ability to execute a child workflow in a different service than where it was initiated. 
This is an important use case for contract termination becasue the goal is to have the contract team write a service which will orchestrate calling all required workflows depending on the options a user selected when terminating the contract. 
In a real-world scenario, **JobWorker** would be a service that the **fulfillment team** implements, the contract team would just need to call the workflow when needed.

## Purpose

- Demonstrates how a workflow in one service can start a child workflow in another service.
- Simulates a fulfillment process that executes independently from the main workflow.
- Ensures separation of concerns by allowing different teams to manage their own workflow logic.

## Prerequisites

Ensure that all required services are running by following the setup instructions in the main [README](../README.md).

## Running the Worker

This worker will automatically start as part of the docker-compose.yml.
It can also be started manually with the following command:

```sh
cd JobWorker
dotnet run
```