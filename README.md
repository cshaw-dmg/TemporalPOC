# Temporal POC

This is a **Proof of Concept (POC)** project demonstrating the use of [Temporal](https://temporal.io/) for orchestrating workflows in **C#/.NET**. It includes a client which initiates workflows and workers which will complete the workflow execution.  

This POC demonstrates executing workflows on demand, orchestrating workflows across multiple services, and passing data using protocol buffers.

## Project Structure


TemporalPOC/ \
├── [Client/](Client) &nbsp;&nbsp;&nbsp;&nbsp;# Temporal Client that starts workflows\
├── [ContractWorker/](ContractWorker) &nbsp;&nbsp;&nbsp;&nbsp;# Worker that handles the contract workflows\
├── [JobWorker/](JobWorkder) &nbsp;&nbsp;&nbsp;&nbsp;# Additional worker for executing job related workflows

## Setup and Running the Project

### 1. Start Required Services

All required services are configured in the `docker-compose.yml` file. You can bring them up using Docker Compose or by starting the Docker Compose project in Visual Studio.

```sh
docker-compose up -d
```

This will start Temporal services and required workers.

### 2. Run the Client

Running the client will queue a workflow for execution. The workflow execution will be handled by the worker, which listens for tasks and completes them asynchronously. You can verify and watch the workflow execution at:

[http://localhost:8080](http://localhost:8080)

## Concepts Demonstrated

- Easily run everything locally
- **Client starts and waits on a workflow to complete**
- Child workflows allow a workflow to start workflows in other services controlled by other teams
- **Activity retries and timeout handling**
- **Workflow versioning and updates**
- When using the BinaryProtoConverter protocol buffer data will be preserved to downstream calls even if the service has an older proto definition

## Example Execution Flow

1. [Client](Client) starts the [TerminateCustomerContract](ContractWorker/TerminateCustomerContractWorkflow.cs) workflow using the latest version of the WorkflowPolicy proto
1. [ContractWorker](ContractWorker) is notified by temporal that it needs to run the [TerminateCustomerContract](ContractWorker/TerminateCustomerContractWorkflow.cs) workflow
1. [ContractWorker](ContractWorker) executes the [activities](ContractWorker/Activities.cs) related to terminating the customer contract
1. [ContractWorker](ContractWorker) starts the child workflow [CancelJobs](JobWorker/CancelJobs.cs) and waits for it complete using the old version of the WorkflowPolicy proto which is missing some fields
1. [JobWorker](JobWorker) is notified by temporal that it needs to run the [CancelJobs](JobWorker/CancelJobs.cs) workflow
1. [JobWorker](JobWorker) executes the [activities](JobWorker/Activities.cs) related to cancelling jobs (this would be fulfilment logic) and since it's using the latest version of WorkflowPolicy proto all fields populated by the Client are visible at this stage
1. The result is returned to the main workflow and then back to the client.

## Monitoring Temporal UI

After starting the Temporal server, open the **Temporal Web UI** at:

[http://localhost:8080](http://localhost:8080)

This UI allows you to monitor running and completed workflows, inspect execution history, and debug failed workflows.

