using System.Collections.Generic;

namespace ProgrammingLanguage
{
    public class Tokens
    {
        /**
         * Description variables, types, methods, operations
         * loops, conditions, struct etc.
         */

        // Data types
        public static List<string> Types = new List<string>
        {
            "int",
            "double",
            "string",
            "bool"
        };

        // Methods
        public static List<string> Methods = new List<string>
        {
            "OpenUrl",
            "ClickOnButton",
            "GetElementById",
            "SetElementById",
            "GetElementByName",
            "SetElementByName",
            "GetElementByClassName",
            "SetElementByClassName"
        };

        // Basic operations
        public static List<string> Operations = new List<string>
        {
            ">=", "<=", ">", "<",
            "==", "!=", "!", "||", "&&",
            "++", "--", "=",
            "+", "-", "*", "/"
        };

        // Conditions
        public static List<string> ConditionOperators = new List<string>
        {
            "if",
            "while",
            "for",
        };

        /**
         * Types of tokens
         * 
         */
        public enum TokenTypes
        {
            Operation,          // +, -, *, /, ++, --, etc..        -> ok
            Dot,                // .                                -> ok
            Coma,               // ,                                -> ok
            Semicolon,          // ;                                -> ok
            Param,              // ( __ )                           -> ok
            Parantheses,        // ()                               -> ok
            Braces,             // { }                              -> ok
            Type,               // variable type                    -> ok
            Word,               // variable name                    -> ok
            Value,              // variable value                   -> ok
            Method,             // ____ () - name method            -> ok
            Comment,            // /**/ and // comments             -> ok
            Conditions          // if, while, for                   -> ok            
        }

        /**
         * Token structure
         *
         */
        public struct Token
        {
            private dynamic value;
            private TokenTypes type;

            public dynamic Value
            {
                get { return value; }
                set { this.value = value; }
            }

            public TokenTypes Type
            {
                get { return type; }
                set { type = value; }
            }

            /* Constructor with two parameters to create new Token */
            public Token(dynamic value, TokenTypes type)
            {
                this.value = value;
                this.type = type;
            }

            /* Present Token - type [current value] */
            public override string ToString()
            {
                return type + " [" + value.ToString() + "]";
            }
        }

        /* All variables are saved in dictionary */
        public static Dictionary<string, Variable> Variables = new Dictionary<string, Variable>();
    }
}
