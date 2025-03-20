using ConractWorker;
using Temporalio.Client;
using Temporalio.Worker;

var temporalServer = Environment.GetEnvironmentVariable("TEMPORAL_SERVER") ?? "localhost:7233";

// Create a client to connect to localhost on "default" namespace
TemporalClient client;

while (true)
{
	try
	{
		client = await TemporalClient.ConnectAsync(new(temporalServer));
		break;
	}
	catch (Exception e)
	{
		Console.WriteLine(e.ToString());
	}
}

// Cancellation token to shutdown worker on ctrl+c
using CancellationTokenSource tokenSource = new();
Console.CancelKeyPress += (_, eventArgs) =>
{
	tokenSource.Cancel();
	eventArgs.Cancel = true;
};

// Create an instance of the activities since we have instance activities.
// If we had all static activities, we could just reference those directly.
Activities activities = new();

// Create a worker with the activity and workflow registered
using TemporalWorker worker = new(
	 client, // client
	 new TemporalWorkerOptions(taskQueue: "CONTRACT_TASK_QUEUE") { Identity = "ContractWorker" }
		  .AddAllActivities(activities) // Register activities
		  .AddWorkflow<TerminateCustomerContractWorkflow>() // Register workflow
);

// Run the worker until it's cancelled
Console.WriteLine("Running worker...");
try
{
	await worker.ExecuteAsync(tokenSource.Token);
}
catch (OperationCanceledException)
{
	Console.WriteLine("Worker cancelled");
}