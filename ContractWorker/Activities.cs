using Temporalio.Activities;

namespace ConractWorker;
public class Activities
{
	[Activity]
	public static async Task TerminateCustomerContractAsync(Guid contractId)
	{
		await Task.Delay(10000);
	}

	[Activity]
	public static async Task TerminateProviderAgreementAsync(Guid agreementId)
	{
		await Task.Delay(15000);
	}
}
