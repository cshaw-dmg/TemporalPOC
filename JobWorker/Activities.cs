using DMG.Common;
using Temporalio.Activities;

namespace JobWorker;
public class Activities
{
	[Activity]
	public static async Task CancelJobs(WorkflowPolicy policy)
	{
		await Task.Delay(1000);
	}
}
