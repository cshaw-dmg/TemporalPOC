using Temporalio.Common;
using Temporalio.Workflows;

namespace ConractWorker;

[Workflow]
public class TerminateCustomerContractWorkflow
{
	[WorkflowRun]
	public async Task RunAsync(Guid contractId)
	{
		// Retry policy
		RetryPolicy retryPolicy = new()
		{
			InitialInterval = TimeSpan.FromSeconds(1),
			MaximumInterval = TimeSpan.FromSeconds(100),
			BackoffCoefficient = 2,
			MaximumAttempts = 3,
		};

		await Workflow.ExecuteActivityAsync(
						 () => Activities.TerminateCustomerContractAsync(contractId),
						 new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5), RetryPolicy = retryPolicy }
					);
		await Workflow.ExecuteActivityAsync(
						 () => Activities.TerminateProviderAgreementAsync(Guid.NewGuid()),
						 new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5), RetryPolicy = retryPolicy }
					);

		var child = await Workflow.StartChildWorkflowAsync("CancelJobs", [], new ChildWorkflowOptions
		{
			ParentClosePolicy = ParentClosePolicy.RequestCancel,
			TaskQueue = "FULFILMENT_TASK_QUEUE",
			Id = Guid.NewGuid().ToString()
		});

		await child.GetResultAsync();
	}
}