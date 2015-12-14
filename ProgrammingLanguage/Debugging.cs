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
    }
}
