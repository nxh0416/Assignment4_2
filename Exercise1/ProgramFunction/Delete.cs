using Exercise1.Interfaces;
using Exercise1.Lib;
using Exercise1.Models;



namespace Exercise1.ProgramFunction;

public class Delete
{
    public static string? OneInstance<T>(string objectName, ref List<T> list_instances, string prefixId)
    where T : ICommon
    {
        Console.WriteLine("Enter data in the following form to delete {0}", objectName);

        Console.Write("Enter {0} ID (number): ", objectName);
        var id = Input.ReadId(prefixId);

        if (!list_instances.Exists(instance => instance.Id.Equals(id)))
        {
            Console.WriteLine("Can not find {0} from entered ID: {0}.", id);
            Console.WriteLine("Return to main sub program.");
            return null;
        }
        else
        {
            Console.WriteLine("Displaying {0} detail.", objectName);

            var instance = list_instances.Find(fixedWing => fixedWing.Id.Equals(id))!;
            Console.WriteLine(string.Join("\n", instance.ToString().Split(", ")));
            Console.WriteLine();

            Console.WriteLine("Are you sure to delete this {0}? (yes/no)", objectName);

            while (true)
            {
                var yesNo = Input.ReadString("Enter your answer: ");
                if (yesNo.ToLower().Equals("yes"))
                {
                    list_instances.Remove(instance);
                    Airplane.RemoveId(instance.Id);
                    break;
                }
                else if (yesNo.ToLower().Equals("no"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid answer. Can only understand \"yes\" or \"no\"");
                }
            }
            return id;
        }
    }
}

