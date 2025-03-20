# Client

The **Client** project in this repository is responsible for starting workflows in Temporal. It interacts with the Temporal server to initiate workflows.

## Features

- Starts [TerminateCustomerContract](../ContractWorker/TerminateCustomerContractWorkflow.cs) workflow and waits for completion

## Prerequisites

Ensure that all required services are running by following the setup instructions in the main [README](../README.md).

## Running the Client

To start the Client and queue a workflow for execution:

```sh
cd Client
dotnet run
```

The client will send workflow requests to Temporal, and execution will be handled by the worker.

## Example Workflow Execution

When the client starts, it initiates a workflow with predefined input parameters. The Temporal worker will then pick up the task and process it asynchronously. You can monitor the workflow execution in the Temporal Web UI:

[http://localhost:8080](http://localhost:8080)