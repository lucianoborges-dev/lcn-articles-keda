namespace LcN.Articles.Keda.Model;

public interface IConsumerData
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
}