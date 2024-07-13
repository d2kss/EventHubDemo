using Azure.Messaging.EventHubs.Producer;
using System.Text;

//Event producer client object


EventHubProducerClient producerClient = new EventHubProducerClient(
    "Endpoint=Copy your endpoint;SharedAccessKeyName=Publisher;SharedAccessKey=<<Copy your shared access key>>",
    "d2ksseventhub1");

// event batch
using EventDataBatch eventDatabatch = await producerClient.CreateBatchAsync();

//we add add bytes to event batch
for (int i = 1; i <= 3; i++)
{
    if (!eventDatabatch.TryAdd(new Azure.Messaging.EventHubs.EventData(Encoding.UTF8.GetBytes($"Event {i}"))))
    {
        throw new Exception($"Event {i} is too large for the batch and cannot be sent.");
        Console.ReadLine();
    }

}
try
{
    await producerClient.SendAsync(eventDatabatch);
    Console.WriteLine("batch data has sent");
    Console.ReadLine();
}
finally
{
    await producerClient.DisposeAsync();
}
//send the bytes to event hub