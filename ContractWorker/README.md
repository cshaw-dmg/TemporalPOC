# Contract Worker

The **ContractWorker** project is responsible for executing workflow activities within the Temporal system. It listens for workflow execution requests and runs activities as needed.

## Features

- Executes the [TerminateCustomerContract](TerminateCustomerContractWorkflow.cs) workflow
- This workflow will run the following [activities](Activities.cs): TerminateCustomerContractAsync, TerminateProviderAgreementAsync
- This workflow will also start a child workflow to [CancelJobs](../JobWorker/CancelJobs.cs) which will be picked up by [JobWorker](../JobWorker)

## Prerequisites

Ensure that all required services are running by following the setup instructions in the main [README](../README.md).

## Running the Worker

This worker will automatically start as part of the docker-compose.yml.
It can also be started manually with the following command:

```sh
cd ContractWorker
dotnet run
```

## Example Execution Flow

1. A workflow is initiated by the **Client**.
2. The **ContractWorker** picks up and executes the necessary activities and child workflows.
4. Upon completion, the worker updates the workflow state.
5. The workflow result is returned to the client.

## Edge Case Test
This workflow also tests an edge case where a given service has an outdated proto definition but if a child workflow is called with an updated proto definition that child workflow will see all the fields