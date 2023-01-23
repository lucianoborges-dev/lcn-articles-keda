namespace LcN.Articles.Keda.Model;

public interface IProducerData
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int NumOfItems { get; set; }
}
