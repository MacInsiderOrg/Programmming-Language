using System;
using System.Collections.Generic;

namespace ProgrammingLanguage
{
    public class EventArguments : EventArgs
    {
        public string Action
        {
            get { return Interpretator.CurrentAction; }
            set { Interpretator.CurrentAction = value; }
        }

        public string Tokens
        {
            get { return Debugging.TokensList; }
            set { Debugging.TokensList = value; }
        }

        public Dictionary<string, Variable> Variables
        {
            get { return Debugging.VariablesList; }
            set { Debugging.VariablesList = value; }
        }
    }

    public delegate void ChangeLabelDelegate(object sender, EventArguments e);
    public delegate void AddTokensToListDelegate(object sender, EventArguments e);
    public delegate void AddVariablesToListDelegate(object sender, EventArguments e);
}
