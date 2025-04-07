using System.Text.Json;
using Temporalio.Client;
using Temporalio.Converters;

public static class Helpers
{
	public static async Task<TemporalClient> CreateClient()
	{
		var temporalServer = Environment.GetEnvironmentVariable("TEMPORAL_SERVER") ?? "localhost:7233";
		DefaultPayloadConverter payloadConverter = new(
			new BinaryNullConverter(),
			new BinaryPlainConverter(),
			new BinaryProtoConverter(),
			new JsonPlainConverter(new JsonSerializerOptions())
		);

		return await TemporalClient.ConnectAsync(new(temporalServer)
		{
			Namespace = "default",
			DataConverter = new DataConverter(payloadConverter, new DefaultFailureConverter())
		});
	}
}