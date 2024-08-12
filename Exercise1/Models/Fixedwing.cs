namespace Exercise1.Models;

public class Fixedwing : Airplane
{
    // Fields
    private string _planeType = "";
    private double _minNeededRunawaySize;

    // Properties
    public string PlaneType
    {
        get => _planeType;
        set => _planeType = value;
    }
    public double MinNeededRunawaySize
    {
        get => _minNeededRunawaySize;
        set => _minNeededRunawaySize = value;
    }
    // Constructors
    public Fixedwing(string id, string model, double cruiseSpeed, double emptyWeight, double maxTakeoffWeight, string planeType, double minNeededRunawaySize, string? belongToAirport = null) : base(id, model, cruiseSpeed, emptyWeight, maxTakeoffWeight, belongToAirport)
    {
        PlaneType = planeType;
        MinNeededRunawaySize = minNeededRunawaySize;
    }
    // Methods
    public override string FlyMethod() => "fixed wing";

    public override string ToString()
    {
        return base.ToString() + $", Fly method: {FlyMethod()}, Type: {PlaneType}, Min Needed Runway Size: {MinNeededRunawaySize}";
    }
}
