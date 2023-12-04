namespace TuberTreats.Models;

public class TuberDriver 
{
    public int id { get; set; }
    public string Name { get; set; }
    public List<TuberOrder> Deliveries { get; set; }
}