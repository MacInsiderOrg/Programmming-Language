using System.Collections.Generic;

namespace ProgrammingLanguage
{
    internal static class Debugging
    {
        public static event AddTokensToListDelegate AddTokensToList;
        public static string TokensList;

        public static event AddVariablesToListDelegate AddVariablesToList;
        public static Dictionary<string, Variable> VariablesList;

        public static void WriteTokensToList(List<Tokens.Token> tokens)
        {
            TokensList = "Tokens in current program:\r\n";
            for (int index = 0; index < tokens.Count; index++)
            {
                TokensList += $"{index}: {tokens[index]}\n";
            }
        }

        public static void WriteVariablesToDict(Dictionary<string, Variable> variables)
        {
            VariablesList = variables;
        }

        public static void OnAddTokensToList()
        {
            AddTokensToList?.Invoke(null, new EventArguments() { Tokens = TokensList });
        }

        public static void OnAddVariablesToList()
        {
            AddVariablesToList?.Invoke(null, new EventArguments() { Variables = VariablesList });
        }
        
        /*
        //private static StreamWriter debugFile;

        public static void StartDebugging()
        {
            debugFile = new StreamWriter("debug.txt");
            WriteToDebug("This is debug file, which contain all information about your compilation file\r\n");
        }

        public static void EndDebugging()
        {
            debugFile.Close();
        }

        public static void WriteToDebug(string message)
        {
            debugFile.WriteLine(message);
        }

        public static void WriteTokensToDebug(List<Tokens.Token> tokens)
        {
            WriteToDebug("Tokens in current program:\r\n");
            for (int index = 0; index < tokens.Count; index++)
            {
                debugFile.WriteLine("{0}: {1}", index, tokens[index].ToString());
            }

            //WriteTokensToList(tokens);
        }

        public static void WriteVariablesToDebug(Dictionary<string, Variable> variables)
        {
            WriteToDebug("\r\nVariables with values in current program:\r\n");

            foreach (var variable in variables)
            {
                debugFile.WriteLine(string.Format("[{0}] {1} = {2}", variable.Value.Type, variable.Key, variable.Value.Value));
            }

            //VariablesList = variables;
        }*/
    }
}
