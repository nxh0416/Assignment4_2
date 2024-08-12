using Exercise1.Models;
using Exercise1.ProgramFunction;
using Exercise1.Lib;

namespace Exercise1;

public static class AirportManagement
{
    const string PROGRAM_HEADER_MAIN = $"Airport MANAGEMENT APP";
    const string PROGRAM_HEADER_1 = "Airport management";
    const string PROGRAM_HEADER_1_1 = "Create an Airport";
    const string PROGRAM_HEADER_1_2 = "Delete an Airport, selected by airport ID";
    const string PROGRAM_HEADER_1_3 = "Add one or more fixed wing(s) to airport";
    const string PROGRAM_HEADER_1_4 = "Add one or more helicopter(s) to airport";
    const string PROGRAM_HEADER_1_5 = "Remove one or more fixed wing(s) from airport";
    const string PROGRAM_HEADER_1_6 = "Remove one or more helicopter(s) from airport";
    const string PROGRAM_HEADER_1_7 = "Display the status of one airport, selected by airport ID";
    const string PROGRAM_HEADER_1_8 = "Display list of all airport information, sorted by airport ID";
    const string PROGRAM_HEADER_2 = "Fixed wing airplane management";
    const string PROGRAM_HEADER_2_1 = "Create fixed wing airplane";
    const string PROGRAM_HEADER_2_2 = "Delete fixed wing airplane, selected by ID";
    const string PROGRAM_HEADER_2_3 = "Change fixed wing airplane type and min needed runway size, selected by ID";
    const string PROGRAM_HEADER_2_4 = "Display list of all fixed wing airplanes with its parking airport ID and name";
    const string PROGRAM_HEADER_3 = "Helicopter management";
    const string PROGRAM_HEADER_3_1 = "Create helicopter";
    const string PROGRAM_HEADER_3_2 = "Delete helicopter, selected by ID";
    const string PROGRAM_HEADER_3_3 = "Display list of all helicopters with its parking airport ID and name";
    const string CLOSE_OPERATION = "Close Program";
    const string NAVIGATE_TO_SUB_PROGRAM = "Return to sub program";
    const string NAVIGATE_TO_MAIN_PROGRAM = "Return to main program";
    const int MAIN_PROGRAM_OP = 4;
    const int AP_PROGRAM_OP = 9;
    const int FW_PROGRAM_OP = 5;
    const int H_PROGRAM_OP = 4;
    public static void Main()
    {
        // bao gồm tất cả các loại airplane
        List<Airplane> list_airplanes = [];
        // bao gồm tất cả các Airport
        List<Airport> list_airports = [];

        bool exitMainProgram = false;
        while (!exitMainProgram)
        {
            ProgramOutput.Header(PROGRAM_HEADER_MAIN);
            ProgramOutput.Operations([PROGRAM_HEADER_1, PROGRAM_HEADER_2, PROGRAM_HEADER_3, CLOSE_OPERATION]);
            var userInput = Input.ReadOperation(upBound: MAIN_PROGRAM_OP);

            switch (userInput)
            {
                // AIRPORT MANAGEMENT
                case 1:
                    {
                        bool exitSubProgram = false;
                        while (!exitSubProgram)
                        {
                            ProgramOutput.Header(PROGRAM_HEADER_1);
                            ProgramOutput.Operations([PROGRAM_HEADER_1_1, PROGRAM_HEADER_1_2, PROGRAM_HEADER_1_3, PROGRAM_HEADER_1_4, PROGRAM_HEADER_1_5, PROGRAM_HEADER_1_6, PROGRAM_HEADER_1_7, PROGRAM_HEADER_1_8, NAVIGATE_TO_MAIN_PROGRAM]);
                            var subOperation = Input.ReadOperation(upBound: AP_PROGRAM_OP);
                            switch (subOperation)
                            {
                                // CREATE AIRPORT
                                case 1:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_1_1);

                                        var airport = Create.CAirport();
                                        list_airports.Add(airport);

                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // DELETE AIRPORT
                                case 2:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_1_2);

                                        string? DeletedId = Delete.OneInstance("Airport", ref list_airports, "AP");
                                        if (DeletedId != null)
                                        {
                                            Airport.RemoveId(DeletedId);

                                            list_airplanes.ForEach(x =>
                                            {
                                                if (x.BelongToAirport != null && x.BelongToAirport.Equals(DeletedId))
                                                {
                                                    x.BelongToAirport = null;
                                                }
                                            });
                                        }

                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // ADD ONE OR MORE FIXED WINGS
                                case 3:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_1_3);
                                        Console.Write("Enter airport ID: ");
                                        string apID = Input.ReadId("AP");
                                        var airport = list_airports.Find(airport => airport.Id.Equals(apID));
                                        if (airport != null)
                                        {
                                            ProgramOutput.Header($"Add fixed wing(s) to airport {airport.Name}");
                                            if (airport.MaxFixedwingParkingPlace == airport.FixedWingIDs.Count)
                                            {
                                                Console.WriteLine("Parking placed for fixed wing in airport {0} is full", airport.Name);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Enter \"exit\" to exit operation");
                                                Console.WriteLine();
                                                while (true)
                                                {
                                                    int airportStorageRemain = airport.MaxFixedwingParkingPlace - airport.FixedWingIDs.Count;
                                                    if (airportStorageRemain == 0)
                                                    {
                                                        Console.WriteLine("Airport capacity for fixed wing is full");
                                                        break;
                                                    }
                                                    string userOP = Input.ReadString("Enter fixed wing ID (number): ");
                                                    if (userOP.ToLower().Equals("exit"))
                                                    {
                                                        Console.WriteLine("Exit adding");
                                                        break;
                                                    }
                                                    else if (Validate.Id(userOP))
                                                    {
                                                        string id = $"FW{int.Parse(userOP):00000}";
                                                        Fixedwing? plane = list_airplanes.OfType<Fixedwing>().ToList().Find(plane => plane.Id.Equals(id));
                                                        if (plane == null)
                                                        {
                                                            Console.WriteLine("Can not find fixed wing with ID {0}", id);
                                                        }
                                                        else
                                                        {
                                                            if (plane.MinNeededRunawaySize > airport.RunwaySize)
                                                            {
                                                                Console.WriteLine("Airport runway size does not sastify fixed wing min runway size");
                                                            }
                                                            else if (airport.FixedWingIDs.Contains(plane.Id))
                                                            {
                                                                Console.WriteLine("Fixed wing with ID {0} is already added to this airport", plane.Id);
                                                            }
                                                            else if (plane.BelongToAirport != null)
                                                            {
                                                                Console.WriteLine("Fixed wing is already added to another airport with ID {0}", plane.BelongToAirport);
                                                            }
                                                            else
                                                            {
                                                                plane.BelongToAirport = airport.Id;
                                                                airport.FixedWingIDs.Add(id);
                                                                Console.WriteLine("Successfully add fixed wing to the airport");
                                                                Console.WriteLine("Airport can hold {0} more fixed wing(s)", airportStorageRemain - 1);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid fixed wing ID");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Can not find airport by entered ID {0}", apID);

                                        }
                                        Console.WriteLine();
                                        Console.WriteLine(NAVIGATE_TO_SUB_PROGRAM);
                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // ADD ONE OR MORE HELICOPTER
                                case 4:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_1_4);
                                        Console.Write("Enter airport ID: ");
                                        string apID = Input.ReadId("AP");
                                        var airport = list_airports.Find(airport => airport.Id.Equals(apID));
                                        if (airport != null)
                                        {
                                            ProgramOutput.Header($"Add helicopter(s) to airport {airport.Name}");
                                            Console.WriteLine("Enter \"exit\" to exit operation");
                                            Console.WriteLine();
                                            while (true)
                                            {
                                                int airportStorageRemain = airport.MaxRotatedwingParkingPlace - airport.HelicopterIDs.Count;
                                                if (airportStorageRemain == 0)
                                                {
                                                    Console.WriteLine("Airport capacity for helicopter is full");
                                                    break;
                                                }
                                                Console.WriteLine("Airport can hold {0} more helicopter(s)", airportStorageRemain);
                                                string userOP = Input.ReadString("Enter helicopter ID (number): ");
                                                if (userOP.ToLower().Equals("exit"))
                                                {
                                                    Console.WriteLine("Exit adding");
                                                    break;
                                                }
                                                else if (Validate.Id(userOP))
                                                {
                                                    string id = $"RW{int.Parse(userOP):00000}";
                                                    Helicopter? plane = list_airplanes.OfType<Helicopter>().ToList().Find(plane => plane.Id.Equals(id));
                                                    if (plane == null)
                                                    {
                                                        Console.WriteLine("Can not find helicopter with ID {0}", id);
                                                    }
                                                    else
                                                    {
                                                        if (airport.HelicopterIDs.Contains(plane.Id))
                                                        {
                                                            Console.WriteLine("helicopter with ID {0} is already added to this airport", plane.Id);
                                                        }
                                                        else if (plane.BelongToAirport != null)
                                                        {
                                                            Console.WriteLine("helicopter is already added to another airport with ID {0}", plane.BelongToAirport);
                                                        }
                                                        else
                                                        {
                                                            plane.BelongToAirport = airport.Id;
                                                            airport.HelicopterIDs.Add(id);
                                                            Console.WriteLine("Successfully add helicopter to the airport");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid ID");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Can not find airport by entered ID {0}", apID);
                                            Console.WriteLine(NAVIGATE_TO_SUB_PROGRAM);
                                            ProgramOutput.Pause();
                                        }
                                        break;
                                    }
                                // REMOVE ONE OR MORE FIXED WING
                                case 5:
                                    {

                                        ProgramOutput.Header(PROGRAM_HEADER_1_5);
                                        Console.Write("Enter airport ID: ");
                                        string apID = Input.ReadId("AP");
                                        var airport = list_airports.Find(airport => airport.Id.Equals(apID));
                                        if (airport != null)
                                        {
                                            ProgramOutput.Header($"Remove fixed wing(s) to airport {airport.Name}");
                                            if (airport.FixedWingIDs.Count == 0)
                                            {
                                                Console.WriteLine("Currently there is no fixed wings to remove.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Enter \"exit\" to exit operation");
                                                Console.WriteLine();
                                                while (true)
                                                {
                                                    if (airport.FixedWingIDs.Count == 0)
                                                    {
                                                        Console.WriteLine("No more fixed wing left to remove.");
                                                        break;
                                                    }
                                                    string userOP = Input.ReadString("Enter fixed wing ID (number): ");
                                                    if (userOP.ToLower().Equals("exit"))
                                                    {
                                                        Console.WriteLine("Exit adding");
                                                        break;
                                                    }
                                                    else if (Validate.Id(userOP))
                                                    {
                                                        string id = $"FW{int.Parse(userOP):00000}";
                                                        Fixedwing? plane = list_airplanes.OfType<Fixedwing>().ToList().Find(plane => plane.Id.Equals(id));
                                                        if (plane == null)
                                                        {
                                                            Console.WriteLine("Can not find fixed wing with ID {0}", id);
                                                        }
                                                        else
                                                        {
                                                            if (!airport.FixedWingIDs.Contains(plane.Id))
                                                            {
                                                                Console.WriteLine("Fixed wing is not belong to this airport.");
                                                            }
                                                            else
                                                            {
                                                                plane.BelongToAirport = null;
                                                                airport.FixedWingIDs.Remove(id);
                                                                Console.WriteLine("Successfully add fixed wing to the airport");
                                                                Console.WriteLine("There is only {0} fixed wing(s) left", airport.FixedWingIDs.Count);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid fixed wing ID");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Can not find airport by entered ID {0}", apID);
                                        }
                                        Console.WriteLine();
                                        Console.WriteLine(NAVIGATE_TO_SUB_PROGRAM);
                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // REMOVE ONE OR MORE HELICOPTERS
                                case 6:
                                    {

                                        ProgramOutput.Header(PROGRAM_HEADER_1_5);
                                        Console.Write("Enter airport ID: ");
                                        string apID = Input.ReadId("AP");
                                        var airport = list_airports.Find(airport => airport.Id.Equals(apID));
                                        if (airport != null)
                                        {
                                            ProgramOutput.Header($"Remove helicopter(s) to airport {airport.Name}");
                                            if (airport.HelicopterIDs.Count == 0)
                                            {
                                                Console.WriteLine("Currently there is no helicopters to remove.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Enter \"exit\" to exit operation");
                                                Console.WriteLine();
                                                while (true)
                                                {
                                                    if (airport.HelicopterIDs.Count == 0)
                                                    {
                                                        Console.WriteLine("No more helicopter left to remove.");
                                                        break;
                                                    }
                                                    string userOP = Input.ReadString("Enter helicopter ID (number): ");
                                                    if (userOP.ToLower().Equals("exit"))
                                                    {
                                                        Console.WriteLine("Exit adding");
                                                        break;
                                                    }
                                                    else if (Validate.Id(userOP))
                                                    {
                                                        string id = $"RW{int.Parse(userOP):00000}";
                                                        Fixedwing? plane = list_airplanes.OfType<Fixedwing>().ToList().Find(plane => plane.Id.Equals(id));
                                                        if (plane == null)
                                                        {
                                                            Console.WriteLine("Can not find helicopter with ID {0}", id);
                                                        }
                                                        else
                                                        {
                                                            if (!airport.HelicopterIDs.Contains(plane.Id))
                                                            {
                                                                Console.WriteLine("helicopter is not belong to this airport.");
                                                            }
                                                            else
                                                            {
                                                                plane.BelongToAirport = null;
                                                                airport.HelicopterIDs.Remove(id);
                                                                Console.WriteLine("Successfully add helicopter to the airport");
                                                                Console.WriteLine("There is only {0} helicopter(s) left", airport.HelicopterIDs.Count);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid helicopter ID");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Can not find airport by entered ID {0}", apID);
                                        }
                                        Console.WriteLine();
                                        Console.WriteLine(NAVIGATE_TO_SUB_PROGRAM);
                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // DISPLAY ALL AIRPORT INFORMATION
                                case 7:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_1_7);
                                        if (list_airports.Count == 0)
                                        {
                                            Console.WriteLine("There is no airport avaiable to display.");
                                        }
                                        else
                                        {
                                            list_airports.Sort((pre, cur) => pre.Id.CompareTo(cur.Id));

                                            list_airports.ForEach(airport =>
                                            {
                                                List<string> ap_H = airport.HelicopterIDs;
                                                List<string> ap_FW = airport.FixedWingIDs;
                                                Console.WriteLine(airport.ToString() + $", Current Fixed Wings in airport: {ap_FW.Count}, Current Helicopters in airport: {ap_H.Count}.");
                                                Console.WriteLine();
                                            });
                                        }

                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // DISPLAY AIRPORT STATUS
                                case 8:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_1_8);
                                        if (list_airports.Count == 0)
                                        {
                                            Console.WriteLine("There is no airport avaiable to display.");
                                        }
                                        else
                                        {
                                            Console.Write("Enter the ID of aiport (number): ");
                                            var apID = Input.ReadId("AP");

                                            if (list_airports.Exists(airport => airport.Id.Equals(apID)))
                                            {
                                                Airport airport = list_airports.Find(airport => airport.Id.Equals(apID))!;
                                                Console.WriteLine("Airport {0} current parking place status: ", airport.Name);
                                                Console.WriteLine("Airport can hold {0} more fixed wing(s).", airport.MaxFixedwingParkingPlace - airport.FixedWingIDs.Count);
                                                Console.WriteLine("Airport can hold {0} more helicopter(s).", airport.MaxRotatedwingParkingPlace - airport.HelicopterIDs.Count);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Can not found airport base on ID: {0}.", apID);
                                                Console.WriteLine("Return to main sub program.");
                                                ProgramOutput.Pause();
                                                break;
                                            }
                                        }
                                        ProgramOutput.Pause();
                                        break;
                                    }
                                case 9:
                                    {
                                        exitSubProgram = true;
                                        break;
                                    }
                                default:
                                    {
                                        throw new Exception($"Invalid operations in sub program {PROGRAM_HEADER_1}.");
                                    }
                            }
                        }
                        break;
                    }
                // FIXED WING MANAGEMENT
                case 2:
                    {
                        bool exitSubProgram = false;
                        while (!exitSubProgram)
                        {

                            ProgramOutput.Header(PROGRAM_HEADER_2);
                            ProgramOutput.Operations([PROGRAM_HEADER_2_1, PROGRAM_HEADER_2_2, PROGRAM_HEADER_2_3, PROGRAM_HEADER_2_4, NAVIGATE_TO_MAIN_PROGRAM]);
                            var subOperation = Input.ReadOperation(upBound: FW_PROGRAM_OP);
                            switch (subOperation)
                            {
                                // CREATE FIXED WING
                                case 1:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_2_1);

                                        var fixedWing = Create.CFixedwing();
                                        list_airplanes.Add(fixedWing);

                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // DELETE FIXED WING
                                case 2:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_2_2);

                                        string? dID = Delete.OneInstance("fixed wing", ref list_airplanes, "FW");
                                        if (!string.IsNullOrEmpty(dID))
                                        {
                                            Airplane.RemoveId(dID);
                                            list_airports.ForEach(airport =>
                                            {
                                                airport.FixedWingIDs.Remove(dID);
                                            });
                                        }

                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // EDIT FIXED WING
                                case 3:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_2_3);
                                        var list_fixedWings = list_airplanes.OfType<Fixedwing>().ToList();
                                        Console.WriteLine("Enter data in the following form to edit fixed wing");

                                        var id = Input.ReadId("FW");
                                        var instance = list_fixedWings.Find(instance => instance.Id.Equals(id));
                                        if (instance == null)
                                        {
                                            Console.WriteLine("Can not find fixed wing from entered ID: {0}.", id);
                                            Console.WriteLine(NAVIGATE_TO_SUB_PROGRAM);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Current plane type: {0}", instance.PlaneType);
                                            var newType = Input.ReadPlaneType();

                                            Console.WriteLine("Current min needed runway size (km): {0}", instance.MinNeededRunawaySize);
                                            var newMinNeededRunwaySize = Input.ReadPositiveNumber<double>("Enter new min needed runway size (km): ");
                                            if (instance.BelongToAirport != null)
                                            {
                                                var airport = list_airports.Find(airport => airport.Id.Equals(instance.BelongToAirport));
                                                if (airport == null)
                                                {
                                                    throw new Exception("Failed Logic in line 519");
                                                }
                                                else
                                                {
                                                    if (newMinNeededRunwaySize > airport.RunwaySize)
                                                    {
                                                        Console.WriteLine("Fixed wing min runway size is larger than airport runway size");
                                                        Console.Write("Would you like to remove fixed wing from airport? (yes/no): ");
                                                        var readAnswer = Input.ReadString("");
                                                        if (readAnswer.ToLower().Equals("yes"))
                                                        {
                                                            airport.FixedWingIDs.Remove(instance.Id);
                                                            instance.BelongToAirport = null;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Updating operation cancel.");
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Update succeed");
                                                        instance.PlaneType = newType;
                                                        instance.MinNeededRunawaySize = newMinNeededRunwaySize;
                                                    }
                                                }
                                            }

                                        }

                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // DISPLAY ALL FIXED WING 
                                case 4:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_3_3);

                                        List<Fixedwing> fixedwings = list_airplanes.OfType<Fixedwing>().ToList();
                                        if (fixedwings.Count == 0)
                                        {
                                            Console.WriteLine("Currently there is no fixed wings to display");
                                        }
                                        else
                                        {
                                            fixedwings.ForEach(fixedWing =>
                                            {
                                                string airportName = fixedWing.BelongToAirport == null ? "None" : list_airports.Find(airport => airport.Id == fixedWing.BelongToAirport)!.Name;
                                                string airportId = fixedWing.BelongToAirport ?? "None";
                                                Console.WriteLine(fixedWing.ToString() + $", Parking airport ID: {airportId}, Parking airport Name: {airportName}");
                                                Console.WriteLine();
                                            });
                                        }
                                        ProgramOutput.Pause();
                                        break;
                                    }
                                case 5:
                                    {
                                        exitSubProgram = true;
                                        break;
                                    }
                                default:
                                    {
                                        throw new Exception($"Invalid operations in sub program {PROGRAM_HEADER_2}.");
                                    }
                            }

                        }


                        ProgramOutput.Pause();
                        break;
                    }
                // HELICOPTER MANAGEMENT
                case 3:
                    {
                        bool exitSubProgram = false;
                        while (!exitSubProgram)
                        {

                            ProgramOutput.Header(PROGRAM_HEADER_3);
                            ProgramOutput.Operations([PROGRAM_HEADER_3_1, PROGRAM_HEADER_3_2, PROGRAM_HEADER_3_3, NAVIGATE_TO_MAIN_PROGRAM]);
                            var subOperation = Input.ReadOperation(upBound: H_PROGRAM_OP);
                            switch (subOperation)
                            {
                                // CREATE HELICOPTER
                                case 1:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_3_1);

                                        var helicopter = Create.CHelicopter();
                                        list_airplanes.Add(helicopter);

                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // DELTE HELICOPTER
                                case 2:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_3_2);

                                        string? dID = Delete.OneInstance("Helicopter", ref list_airplanes, "RW");

                                        if (!string.IsNullOrEmpty(dID))
                                        {
                                            Airplane.RemoveId(dID);
                                            list_airports.ForEach(airport =>
                                            {
                                                airport.HelicopterIDs.Remove(dID);
                                            });
                                        }

                                        ProgramOutput.Pause();
                                        break;
                                    }
                                // DISPLAY HELICOPTER
                                case 3:
                                    {
                                        ProgramOutput.Header(PROGRAM_HEADER_3_3);

                                        List<Helicopter> helicopters = list_airplanes.OfType<Helicopter>().ToList();
                                        if (helicopters.Count == 0)
                                        {
                                            Console.WriteLine("Currently there is no helicopter to display.");
                                        }
                                        else
                                        {
                                            helicopters.ForEach(helicopter =>
                                            {
                                                string airportName = helicopter.BelongToAirport == null ? "None" : list_airports.Find(airport => airport.Id == helicopter.BelongToAirport)!.Name;
                                                string airportId = helicopter.BelongToAirport ?? "None";
                                                Console.WriteLine(helicopter.ToString() + $", Parking airport ID: {airportId}, Parking airport Name: {airportName}");
                                                Console.WriteLine();
                                            });
                                        }

                                        ProgramOutput.Pause();
                                        break;
                                    }
                                case 4:
                                    {
                                        exitSubProgram = true;
                                        break;
                                    }
                                default:
                                    {
                                        throw new Exception($"Invalid operations in sub program {PROGRAM_HEADER_2}.");
                                    }
                            }

                        }
                        ProgramOutput.Pause();
                        break;
                    }
                case 4:
                    {
                        exitMainProgram = true;
                        break;
                    }
                default:
                    {
                        throw new Exception("Invalid main operation!");
                    }
            }

        }
    }
}
