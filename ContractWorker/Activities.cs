using DMG.Common;
using Temporalio.Activities;

namespace ConractWorker;
public class Activities
{
	[Activity]
	public static async Task TerminateCustomerContractAsync(WorkflowPolicy policy)
	{
		await Task.Delay(10000);
	}

	[Activity]
	public static async Task TerminateProviderAgreementAsync(DMG.Common.WorkflowPolicy policy)
	{
		await Task.Delay(15000);
	}
}
