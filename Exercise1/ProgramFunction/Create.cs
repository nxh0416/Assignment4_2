using Exercise1.Models;
using Exercise1.Lib;

namespace Exercise1.ProgramFunction;

public class Create
{
    public static Fixedwing CFixedwing()
    {
        Input.ReadAirplane<Fixedwing>(out var id, out var model, out var cruiseSpeed, out var emptyWeight, out var maxTakeoffWeight, prefix: "FW");

        Console.Write("Enter plane type (\"CAG\", \"LGR\", \"PRV\"): ");
        var planeType = Input.ReadPlaneType();

        var minNeededRunawaySize = Input.ReadPositiveNumber<int>("Enter min needed runwaway size (km): ");

        return new Fixedwing(id, model, cruiseSpeed, emptyWeight, maxTakeoffWeight, planeType, minNeededRunawaySize);
    }

    public static Helicopter CHelicopter()
    {
        Input.ReadAirplane<Helicopter>(out var id, out var model, out var cruiseSpeed, out var emptyWeight, out var maxTakeoffWeight, prefix: "RW");

        var range = Input.ReadPositiveNumber<double>("Enter range: ");

        return new Helicopter(id, model, cruiseSpeed, emptyWeight, maxTakeoffWeight, range);

    }

    public static Airport CAirport()
    {
        Input.ReadAirport(out var id, out var name, out var runwaySize, out var maxFixedwingParkingPlace, out var maxRotatedwingParkingPlace, "AP");

        return new Airport(
            id: id,
            name: name,
            runwaySize: runwaySize,
            maxRotatedwingParkingPlace: maxFixedwingParkingPlace,
            maxFixedwingParkingPlace: maxRotatedwingParkingPlace
        );
    }
}
