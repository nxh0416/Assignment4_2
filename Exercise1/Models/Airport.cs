using Exercise1.Interfaces;

namespace Exercise1.Models;

public class Airport : ICommon
{
    // Fields
    private static HashSet<string> _airportsID = [];
    private string _id = null!;
    private string _name = null!;
    private double _runwaySize;
    private int _maxFixedwingParkingPlace;
    private List<string> _fixedWingIDs = [];
    private int _maxRotatedwingParkingPlace;
    private List<string> _helicopterIDs = [];

    // Properties
    public string Id
    {
        get => _id;
        set => _id = value;
    }
    public string Name
    {
        get => _name;
        set => _name = value;
    }
    public double RunwaySize
    {
        get => _runwaySize;
        set => _runwaySize = value;
    }
    public int MaxFixedwingParkingPlace
    {
        get => _maxFixedwingParkingPlace;
        set => _maxFixedwingParkingPlace = value;
    }
    public List<string> FixedWingIDs
    {
        get => _fixedWingIDs;
        set => _fixedWingIDs = value;
    }
    public int MaxRotatedwingParkingPlace
    {
        get => _maxRotatedwingParkingPlace;
        set => _maxRotatedwingParkingPlace = value;
    }
    public List<string> HelicopterIDs
    {
        get => _helicopterIDs; set =>
    _helicopterIDs = value;
    }
    // Constructors
    public Airport(string id, string name, double runwaySize, int maxFixedwingParkingPlace, int maxRotatedwingParkingPlace)
    {
        _airportsID.Add(id);

        Id = id;
        Name = name;
        RunwaySize = runwaySize;
        MaxFixedwingParkingPlace = maxFixedwingParkingPlace;
        MaxRotatedwingParkingPlace = maxRotatedwingParkingPlace;
    }
    // Methods
    public static bool CheckAirportId(string AirportId)
    {
        return _airportsID.Contains(AirportId);
    }
    public static bool RemoveId(string id)
    {
        return _airportsID.Remove(id);
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, RunwaySize: {RunwaySize}, Max Fixed wing capacity: {MaxFixedwingParkingPlace}, Max Helicopter capacity: {MaxRotatedwingParkingPlace}";
    }
}
