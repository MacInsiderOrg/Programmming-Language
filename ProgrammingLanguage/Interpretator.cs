using System.Collections.Generic;
using System.Windows.Forms;

namespace ProgrammingLanguage
{
    public class Interpretator : IInterpretator
    {
        public static event ChangeLabelDelegate ChangeLabel;
        public static string CurrentAction;
        
        private readonly List<string> buildList;

        private WebBrowser webBrowser;
        
        public Interpretator(WebBrowser webBrowser)
        {
            buildList = new List<string>
            {
                "Building",
                "Build succeeded",
                "Interpretator find some errors"
            };

            CurrentAction = buildList[0];
            this.webBrowser = webBrowser;
        }

        public object[] Execute(string programText, object[] args)
        {
            // Create lexical analyzer object, which contains all data about tokens
            var lexicalAnalyzer = new LexicalAnalyzer(CurrentAction);

            // Parse tokens from program text
            var tokens = lexicalAnalyzer.ParseText(programText);

            var syntaxAnalyzer = new SyntaxAnalyzer(tokens, Tokens.Methods, Tokens.Variables, webBrowser);
            if (tokens != null)
            {
                Debugging.WriteTokensToList(tokens);

                int zero = 0;
                syntaxAnalyzer.CreateTree(ref zero);
                CurrentAction = buildList[1];

                Debugging.WriteVariablesToDict(syntaxAnalyzer.Variables);
            }
            else
            {
                CurrentAction = buildList[2];
            }

            return null;
        }

        public static void ChangeCurrentAction()
        {
            ChangeLabel?.Invoke(null, new EventArguments() { Action = CurrentAction });
        }
    }
}
