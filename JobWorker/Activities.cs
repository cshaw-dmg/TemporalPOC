using Temporalio.Activities;

namespace JobWorker;
public class Activities
{
	[Activity]
	public static async Task CancelJobs(Guid contractId)
	{
		await Task.Delay(20000);
	}
}
