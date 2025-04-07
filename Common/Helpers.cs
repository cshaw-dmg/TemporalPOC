using Temporalio.Client;
using Temporalio.Converters;

public static class Helpers
{
	public static async Task<TemporalClient> CreateClient()
	{
		var temporalServer = Environment.GetEnvironmentVariable("TEMPORAL_SERVER") ?? "localhost:7233";

		return await TemporalClient.ConnectAsync(new(temporalServer)
		{
			Namespace = "default",
			DataConverter = new DataConverter(new DefaultPayloadConverter(encodingConverters: [new BinaryProtoConverter()]), new DefaultFailureConverter())
		});
	}
}