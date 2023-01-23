using System.Text.Json.Serialization;

namespace LcN.Articles.Keda.Api.Services;

public class ProducerData
{
    [JsonIgnore]
    public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public int NumOfItems { get; }

    public ProducerData(string name, int numOfItems)
    {
        Name = name;
        NumOfItems = numOfItems;
    }
}
