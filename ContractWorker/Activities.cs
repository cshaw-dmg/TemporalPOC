using DMG.Common;
using Temporalio.Activities;

namespace ConractWorker;
public class Activities
{
	[Activity]
	public static async Task<ActionResult> TerminateCustomerContractAsync(WorkflowPolicy policy)
	{
		await Task.Delay(10000);

		return new ActionResult { Successful = true };
	}

	[Activity]
	public static async Task<ActionResult> TerminateProviderAgreementAsync(DMG.Common.WorkflowPolicy policy)
	{
		await Task.Delay(15000);

		return new ActionResult { Successful = true };
	}
}
