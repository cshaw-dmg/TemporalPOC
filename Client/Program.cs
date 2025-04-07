using Google.Protobuf.WellKnownTypes;

// Connect to the Temporal server

var client = await Helpers.CreateClient();

var workflowId = $"{Guid.NewGuid()}";

try
{
	// Start the workflow
	DMG.Common.WorkflowPolicy arg = new()
	{
		ContractTerminationManualReview = new DMG.Common.ContractTerminationManualReview
		{
			ContractTerminationDetails = new DMG.Common.ContractTerminationDetails
			{
				ContractId = Guid.NewGuid().ToString(),
				EffectiveDate = DateTime.UtcNow.ToTimestamp(),
				TermReason = "Some reason"
			}
		}
	};

	var handle = await client.StartWorkflowAsync("TerminateCustomerContractWorkflow", [arg], new Temporalio.Client.WorkflowOptions(id: workflowId, taskQueue: "CONTRACT_TASK_QUEUE") { ExecutionTimeout = TimeSpan.MaxValue });

	Console.WriteLine($"Started Workflow {workflowId}");

	// Await the result of the workflow
	await handle.GetResultAsync();
}
catch (Exception ex)
{
	Console.Error.WriteLine($"Workflow execution failed: {ex.Message}");
}