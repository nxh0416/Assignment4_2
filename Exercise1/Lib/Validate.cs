using System.Text.RegularExpressions;

namespace Exercise1.Lib
{
    public class Validate
    {
        private static readonly string[] FIXEDWING_TYPE = ["CAG", "LGR", "PRV"];
        public static bool Id(string id)
        {
            string pattern = @"^[0-9]{1,5}$";
            return Regex.IsMatch(id, pattern);
        }
        public static bool FixedWingType(string type)
        {
            if (FIXEDWING_TYPE.Contains(type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}