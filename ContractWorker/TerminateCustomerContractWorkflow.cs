using DMG.Common;
using Temporalio.Common;
using Temporalio.Workflows;

namespace ConractWorker;

[Workflow]
public class TerminateCustomerContractWorkflow
{
	[WorkflowRun]
	public async Task<WorkflowResult> RunAsync(WorkflowPolicy policy)
	{
		// Retry policy
		RetryPolicy retryPolicy = new()
		{
			InitialInterval = TimeSpan.FromSeconds(1),
			MaximumInterval = TimeSpan.FromSeconds(100),
			BackoffCoefficient = 2,
			MaximumAttempts = 3,
		};

		var child = Workflow.StartChildWorkflowAsync("CancelJobs", [policy], new ChildWorkflowOptions
		{
			ParentClosePolicy = ParentClosePolicy.RequestCancel,
			TaskQueue = "FULFILMENT_TASK_QUEUE",
			Id = Guid.NewGuid().ToString(),
			ExecutionTimeout = TimeSpan.MaxValue,
		});

		await Workflow.ExecuteActivityAsync(
						 () => Activities.TerminateCustomerContractAsync(policy),
						 new ActivityOptions { StartToCloseTimeout = TimeSpan.MaxValue, RetryPolicy = retryPolicy }
					);
		await Workflow.ExecuteActivityAsync(
						 () => Activities.TerminateProviderAgreementAsync(policy),
						 new ActivityOptions { StartToCloseTimeout = TimeSpan.MaxValue, RetryPolicy = retryPolicy }
					);

		var done = await child;
		//await child.GetResultAsync();

		return new WorkflowResult { Successful = true };
	}
}