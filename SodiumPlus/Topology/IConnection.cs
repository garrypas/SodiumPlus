namespace SodiumPlus.Topology
{
    public interface IConnection
    {
        double Weight { get; set; }

        string Name { get; set; }
    }
}