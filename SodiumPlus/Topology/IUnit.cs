namespace SodiumPlus.Topology
{
    public interface IUnit
    {
        double ActivationValue { get; set; }

        double NetInput { get; set; }

        string Name { get; set; }

        double SlopeMultiplier { get; set; }
    }
}