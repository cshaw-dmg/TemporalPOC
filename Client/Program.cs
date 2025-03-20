using ConractWorker;
using Temporalio.Client;

// Connect to the Temporal server
var client = await TemporalClient.ConnectAsync(new("localhost:7233") { Namespace = "default" });

var workflowId = $"{Guid.NewGuid()}";

try
{
	// Start the workflow
	var handle = await client.StartWorkflowAsync(
		 (TerminateCustomerContractWorkflow wf) => wf.RunAsync(Guid.NewGuid()),
		 new(id: workflowId, taskQueue: "CONTRACT_TASK_QUEUE"));

	Console.WriteLine($"Started Workflow {workflowId}");

	// Await the result of the workflow
	await handle.GetResultAsync();
}
catch (Exception ex)
{
	Console.Error.WriteLine($"Workflow execution failed: {ex.Message}");
}