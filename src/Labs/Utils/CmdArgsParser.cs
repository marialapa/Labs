namespace Labs.Utils
{
    public class CmdArgsParser
    {
        public static Dictionary<string, string?> Parse(string[] args)
        {
            Dictionary<string, string?> values = new Dictionary<string, string?>();
            if (args.Length == 0)
            {
                return values;
            }
            for(int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-"))
                {
                    if (args.Length > i + 1)
                    {
                        values[args[i]] = args[i + 1];
                    }
                    else
                    {
                        values[args[i]] = null;
                    }
                }
            }
            return values;
        }
    }
}
