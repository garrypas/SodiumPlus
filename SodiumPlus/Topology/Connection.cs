namespace SodiumPlus.Topology
{

    public class Connection : IConnection
    {
        public string Name { get; set; }
        public virtual double Weight { get; set; }
    }
}