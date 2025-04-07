using JobWorker;
using Temporalio.Client;
using Temporalio.Worker;

//This service is a placeholder for what fulfilment will write.
//This service was created to verify that it's possible to start a child workflow in another service proving that it will be possible for the contract team to orchistrate all this stuff

var temporalServer = Environment.GetEnvironmentVariable("TEMPORAL_SERVER") ?? "localhost:7233";

TemporalClient client;

while (true)
{
	try
	{
		client = await Helpers.CreateClient();
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

// Create a worker with the activity and workflow registered
using TemporalWorker worker = new(
	 client, // client
	 new TemporalWorkerOptions(taskQueue: "FULFILMENT_TASK_QUEUE") { Identity = "JobWorker" }
			.AddAllActivities(new Activities())
		  .AddWorkflow<CancelJobs>() // Register workflow
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
// @@@SNIPEND