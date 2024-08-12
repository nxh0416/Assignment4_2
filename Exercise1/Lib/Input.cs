
using Exercise1.Models;

namespace Exercise1.Lib
{
    public class Input
    {
        public const int ZERO = 0;
        public static string ReadString(string write)
        {
            do
            {
                Console.Write(write);
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid Input! Input can not be empty or null.");
                }
                else
                {
                    return input;
                }
            } while (true);
        }

        public static T ReadNumber<T>(string write)
        where T : IComparable
        {
            do
            {
                Console.Write(write);
                var input = Console.ReadLine();
                if (decimal.TryParse(input, out var result))
                {
                    return (T)Convert.ChangeType(result, typeof(T));
                }
                else
                {
                    Console.WriteLine("Invalid Input! Input is not a valid number");
                }
            } while (true);
        }
        public static T ReadPositiveNumber<T>(string write)
        where T : IComparable
        {
            while (true)
            {
                var input = ReadNumber<T>(write);
                if (input.CompareTo((T)Convert.ChangeType(ZERO, typeof(T))) < 0)
                {
                    Console.WriteLine("Invalid Input! Input is not a valid positive number.");
                    Console.Write(write);
                }
                else
                {
                    return input;
                }
            }
        }
        public static void ReadAirport(out string id, out string name, out double runwaySize, out int maxFixedwingParkingPlace, out int maxRotatedwingParkingPlace, string prefix)
        {
            string messageName = "Enter aiport name: ";
            string messageRunSize = "Enter Runway Size (km): ";
            string messageMFWPP = "Enter max fixed wing parking place: ";
            string messageMRWPP = "Enter max helicopter parking place: ";
            while (true)
            {
                id = ReadId(prefix: "AP");
                if (Airport.CheckAirportId(id))
                {
                    Console.WriteLine("Invalid ID! Id already exist.");
                }
                else
                {
                    break;
                }
            }
            name = ReadString(messageName);

            runwaySize = ReadPositiveNumber<double>(messageRunSize);

            maxFixedwingParkingPlace = ReadPositiveNumber<int>(messageMFWPP);

            maxRotatedwingParkingPlace = ReadPositiveNumber<int>(messageMRWPP);
        }
        public static void ReadAirplane<T>(out string id, out string model, out double cruiseSpeed, out double emptyWeight, out double maxTakeoffWeight, string prefix)
        {
            while (true)
            {
                id = ReadId(prefix);
                if (Airplane.CheckId(id))
                {
                    Console.WriteLine("Invalid ID! Id already exist.");
                }
                else
                {
                    break;
                }
            }

            model = ReadPlanneModel();

            cruiseSpeed = ReadPositiveNumber<double>("Enter cruise speed (km/h): ");
            if (typeof(T) == typeof(Helicopter))
            {
                while (true)
                {
                    emptyWeight = ReadPositiveNumber<double>("Enter empty weight (kg): ");

                    maxTakeoffWeight = ReadPositiveNumber<double>("Enter max takeoff weight (kg): ");
                    if (emptyWeight * 1.5 < maxTakeoffWeight)
                    {
                        Console.WriteLine("The max takeoff weight of helicopter does not excess 1.5 times of its empty weight.");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                while (true)
                {
                    emptyWeight = ReadPositiveNumber<double>("Enter empty weight (kg): ");

                    maxTakeoffWeight = ReadPositiveNumber<double>("Enter max takeoff weight (kg): ");
                    if (emptyWeight > maxTakeoffWeight)
                    {
                        Console.WriteLine("The max takeoff weight of an airplane does not smaller than its empty weight.");
                    }
                    else
                    {
                        break;
                    }
                }
            }

        }
        public static string ReadPlaneType()
        {
            string message = "Enter plane type (\"CAG\" or \"LGR\" or \"PRV\"): ";
            string error = "Invalid plane type! Plane type can only be \"CAG\" or \"LGR\" or \"PRV\"";
            do
            {
                var userInput = ReadString(message);
                if (Validate.FixedWingType(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine(error);
                }
            } while (true);
        }
        public static string ReadId(string prefix)
        {
            string message = "Enter Id (at most 5 digits): ";
            string error = "Id number can only be digit and not exceed 5 digits";
            do
            {
                var userInput = ReadString(message);
                if (Validate.Id(userInput))
                {
                    var num = int.Parse(userInput);
                    return $"{prefix}{num:00000}";
                }
                else
                {
                    Console.WriteLine(error);
                }
            } while (true);
        }

        public static string ReadPlanneModel()
        {
            string message = "Enter model (at most 40 characters): ";
            string error = "Invalid model length! Model length can not exceed 40 characters.";
            do
            {
                var model = ReadString(message);
                if (model.Length > 40)
                {
                    Console.WriteLine(error);
                }
                else
                {
                    return model;
                }
            } while (true);
        }
        public static int ReadOperation(int upBound = 4, int downBound = 1)
        {
            string message = "Enter your selection: ";
            string error = "Invalid Operation! Operation out of bound.";
            do
            {
                Console.Write(message);
                var userInput = ReadNumber<int>(message);
                if (userInput > upBound || userInput < downBound)
                {
                    Console.WriteLine(error);
                }
                else
                {
                    return userInput;
                }
            } while (true);
        }
    }
}