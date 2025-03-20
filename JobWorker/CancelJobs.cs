using Temporalio.Common;
using Temporalio.Workflows;

namespace JobWorker;

[Workflow]
public class CancelJobs
{
	[WorkflowRun]
	public async Task RunAsync()
	{
		RetryPolicy retryPolicy = new()
		{
			InitialInterval = TimeSpan.FromSeconds(1),
			MaximumInterval = TimeSpan.FromSeconds(100),
			BackoffCoefficient = 2,
			MaximumAttempts = 3,
		};

		await Workflow.ExecuteActivityAsync(
						 () => Activities.CancelJobs(Guid.NewGuid()),
						 new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5), RetryPolicy = retryPolicy }
					);
	}
}
