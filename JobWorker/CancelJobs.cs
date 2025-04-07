using DMG.Common;
using Temporalio.Common;
using Temporalio.Workflows;

namespace JobWorker;

[Workflow]
public class CancelJobs
{
	[WorkflowRun]
	public async Task<WorkflowResult> RunAsync(WorkflowPolicy policy)
	{
		RetryPolicy retryPolicy = new()
		{
			InitialInterval = TimeSpan.FromSeconds(1),
			MaximumInterval = TimeSpan.FromSeconds(100),
			BackoffCoefficient = 2,
			MaximumAttempts = 3,
		};

		await Workflow.ExecuteActivityAsync(
			() => Activities.CancelJobs(policy),
			new ActivityOptions { StartToCloseTimeout = TimeSpan.MaxValue, RetryPolicy = retryPolicy }
		);

		return new WorkflowResult { Successful = true };
	}
}
