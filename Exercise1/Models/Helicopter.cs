namespace Exercise1.Models;

public class Helicopter : Airplane
{
    // Fields
    private double _range;


    // Properties
    public double Range { get => _range; set => _range = value; }
    // Constructors
    public Helicopter(string id, string model, double cruiseSpeed, double emptyWeight, double maxTakeoffWeight, double range, string? belongAirport = null) : base(id, model, cruiseSpeed, emptyWeight, maxTakeoffWeight, belongAirport)
    {
        Range = range;
    }

    // Methods
    public override string FlyMethod() => "rotated wing";

    public override string ToString()
    {
        return base.ToString() + $", Fly method: {FlyMethod()}, Range: {Range}";
    }
}
