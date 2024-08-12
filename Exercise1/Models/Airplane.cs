using Exercise1.Interfaces;

namespace Exercise1.Models;

public abstract class Airplane : ICommon
{
    // fields
    private static HashSet<string> _airplaneId = [];
    private string _id = null!;
    private string _model = null!;
    private double _cruiseSpeed;
    private double _emptyWeight;
    private double _maxTakeoffWeight;
    private string? _belongToAirport;
    // properties
    public string Id
    {
        get => _id;
        set => _id = value;
    }
    public string Model
    {
        get => _model;
        set => _model = value;
    }
    public double CruiseSpeed
    {
        get => _cruiseSpeed;
        set => _cruiseSpeed = value;
    }
    public double EmptyWeight
    {
        get => _emptyWeight;
        set => _emptyWeight = value;
    }
    public double MaxTakeoffWeight
    {
        get => _maxTakeoffWeight;
        set => _maxTakeoffWeight = value;
    }
    public string? BelongToAirport { get => _belongToAirport; set => _belongToAirport = value; }

    // Constructors
    protected Airplane(string id, string model, double cruiseSpeed, double emptyWeight, double maxTakeoffWeight, string? belongToAirport = null)
    {
        _airplaneId.Add(id);
        Id = id;
        Model = model;
        CruiseSpeed = cruiseSpeed;
        EmptyWeight = emptyWeight;
        MaxTakeoffWeight = maxTakeoffWeight;
        BelongToAirport = belongToAirport;
    }

    // methods
    public static bool CheckId(string id)
    {
        return _airplaneId.Contains(id);
    }
    public static bool RemoveId(string id)
    {
        return _airplaneId.Remove(id);
    }
    public abstract string FlyMethod();

    public override string ToString()
    {
        return $"ID: {Id}, Model: {Model}, Cruise Speed: {CruiseSpeed}, Empty Weight: {EmptyWeight}, Max Takeoff Weight: {MaxTakeoffWeight}";
    }

}
