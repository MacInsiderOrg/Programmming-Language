using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgrammingLanguage
{
    class LexicalAnalyzer
    {
        private List<Tokens.Token> tokens;
        private string programText;
        private string currentAction;

        /* Constructor, when new lexical analyzer is created */
        public LexicalAnalyzer(string currentAction)
        {
            this.currentAction = currentAction;

            Tokens.Variables = new Dictionary<string, Variable>();
        }

        /**
         * Parse code from form and create new tokens
         *
         */
        public List<Tokens.Token> ParseText(string text)
        {
            programText = text;

            // List of tokens, which contain all codes divided into different blocks
            tokens = new List<Tokens.Token>();

            // Index position
            int index = 0;

            /* Continue, while text is not successful */
            while (programText.Length > 0)
            {
                //int programTextLength = programText.Length;
                var currentChar = programText[index];

                /* Remove space if finded */
                if (currentChar.Equals(' '))
                {
                    programText = programText.Remove(0, 1);
                    continue;
                }

                /* Add comments to tokens */
                if (currentChar.Equals('/') && (programText[index + 1].Equals('/') || programText[index + 1].Equals('*')))
                {
                    ParseComments();
                    continue;
                }

                /* Add dots to tokens */
                if (currentChar.Equals('.'))
                {
                    AddOneDimensionalValueToTokens(Tokens.TokenTypes.Dot);
                    continue;
                }

                /* Add semicolons to tokens */
                if (currentChar.Equals(';'))
                {
                    AddOneDimensionalValueToTokens(Tokens.TokenTypes.Semicolon);
                    continue;
                }

                /* Delete transition to newline */
                if (currentChar.ToString().Equals("\n"))
                {
                    programText = programText.Remove(0, 1);
                    continue;
                }

                /* Add braces to tokens */
                if (currentChar.Equals('{') || currentChar.Equals('}'))
                {
                    AddOneDimensionalValueToTokens(Tokens.TokenTypes.Braces);
                    continue;
                }

                /* Add operations to tokens */
                if (AddOperationToTokens())
                {
                    continue;
                }

                /* Add methods to tokens */
                if (AddMethodToTokens())
                {
                    continue;
                }

                /* Add params to tokens */
                if (currentChar.Equals('('))
                {
                    int endParamsIndex = programText.IndexOf(')');
                    string parameters;

                    if (tokens[tokens.Count - 1].Value.Equals("for"))
                    {
                        parameters = programText.Substring(1, endParamsIndex - 1);
                        if (AddForParamsToTokens(parameters, endParamsIndex))
                            continue;
                    }

                    if (tokens[tokens.Count - 1].Value.Equals("if") || tokens[tokens.Count - 1].Value.Equals("while"))
                    {
                        int closeParanthesesIndex = GetCloseParanthesesIndex(index);
                        parameters = programText.Substring(0, closeParanthesesIndex + 1);

                        if (AddConditionParamsToTokens(parameters, closeParanthesesIndex))
                            continue;
                    }
                    
                    parameters = programText.Substring(1, endParamsIndex - 1);
                    AddMethodParamsToTokens(parameters, endParamsIndex);

                    continue;
                }
                
                /* Add conditions to tokens */
                if (AddConditionOperatorToTokens())
                {
                    continue;
                }

                /* Add variables types to tokens */
                if (AddVariableTypeToTokens())
                {
                    continue;
                }

                /* Add variables name to tokens */
                if (AddVariableNameToTokens())
                {
                    continue;
                }

                /* Add variables value to tokens */
                AddVariableValueToTokens();
            }

            return tokens;
        }

        /**
         * Parse methods:
         * - ParseComments                  ( )
         * - AddOneDimensionalValueToTokens (tokenType)
         * - AddValueToTokens               (currentText, tokenType, endIndex)
         * - AddOperationToTokens           ( )
         * - AddMethodToTokens              ( )
         * - AddMethodParamsToTokens        (parameters, endParamsIndex)
         * - AddForParamsToTokens           (parameters, endParamsIndex)
         * - AddConditionParamsToTokens     (parameters, endParamsIndex)
         * - AddConditionOperatorToTokens   ( )
         * - AddVariableTypeToTokens        ( )
         * - AddVariableNameToTokens        ( )
         * - AddVariableValueToTokens       ( )
         */

        /**
         * Parse all comments from code and
         * add them to tokens list 
         * Comments can be started with // or /*
         */
        private void ParseComments()
        {
            int index = 0;
            int programTextLength = programText.Length;

            if (programText[index + 1].Equals('*'))
            {
                int endCommentIndex = programText.IndexOf("*/", StringComparison.Ordinal);
                var comment = programText.Substring(0, endCommentIndex + 2);

                AddValueToTokens(comment, Tokens.TokenTypes.Comment, endCommentIndex + 2);
            }
            else if (programText[index + 1].Equals('/'))
            {
                int endCommentIndex = programText.IndexOf("\n", StringComparison.Ordinal);

                if (endCommentIndex >= 0)
                {
                    var comment = programText.Substring(0, endCommentIndex);

                    AddValueToTokens(comment, Tokens.TokenTypes.Comment, endCommentIndex);
                }
                else
                {
                    programText = programText.Remove(0, programTextLength);
                }
            }
        }

        /* Add one dimensional value to tokens */
        private void AddOneDimensionalValueToTokens(Tokens.TokenTypes tokenType)
        {
            tokens.Add(
                new Tokens.Token(
                    programText[0].ToString(),
                    tokenType
                )
            );

            programText = programText.Remove(0, 1);
        }

        /* Add more than one dimensional value to tokens */
        private void AddValueToTokens(string currentText, Tokens.TokenTypes tokenType, int endIndex)
        {
            tokens.Add(
                new Tokens.Token(
                    currentText,
                    tokenType
                )
            );

            programText = programText.Remove(0, endIndex);
        }

        /* Add operation to tokens */
        private bool AddOperationToTokens()
        {
            foreach (var operation in Tokens.Operations)
            {
                if (programText.IndexOf(operation, StringComparison.Ordinal) == 0)
                {
                    AddValueToTokens(operation, Tokens.TokenTypes.Operation, operation.Length);
                    return true;
                }
            }

            return false;
        }

        /* Add method to tokens */
        private bool AddMethodToTokens()
        {
            foreach (var method in Tokens.Methods)
            {
                if (programText.IndexOf(method, StringComparison.Ordinal) == 0)
                {
                    AddValueToTokens(method, Tokens.TokenTypes.Method, method.Length);
                    return true;
                }
            }

            return false;
        }

        /* Add parameters of functions to tokens */
        private void AddMethodParamsToTokens(string parameters, int endParamsIndex)
        {
            const int index = 0;
            parameters = parameters.Trim();//Trim('"').Replace('\\', ' ');

            tokens.Add(
                new Tokens.Token(
                    programText[index].ToString(),
                    Tokens.TokenTypes.Parantheses
                )
            );

            while (parameters.Length > 0)
            {
                parameters = parameters.Trim();//Trim('"').Replace(@"\"[0], ' ');
                char currentChar = parameters[index];

                if (currentChar.Equals(' '))
                {
                    parameters = parameters.Remove(index, 1);
                    continue;
                }

                if (currentChar.Equals(','))
                {
                    tokens.Add(
                        new Tokens.Token(
                            currentChar.ToString(),
                            Tokens.TokenTypes.Coma
                        )
                    );

                    parameters = parameters.Remove(index, 1);
                    continue;
                }

                var positions = new[]
                {
                   // parameters.IndexOf(" ", StringComparison.Ordinal),
                    parameters.IndexOf(",", StringComparison.Ordinal)
                };

                string param;

                if (!positions.Where(value => value > 0).Any())
                    param = parameters.Substring(0, parameters.Length).Trim();
                else
                    param = parameters.Substring(0, positions.Where(value => value > 0).Min()).Trim();

                tokens.Add(
                    new Tokens.Token(
                        param.ToString().Trim('"'),
                        Tokens.TokenTypes.Value
                    )
                );

                parameters = parameters.Remove(index, param.Length);

            }

            tokens.Add(
                new Tokens.Token(
                    programText[endParamsIndex].ToString(),
                    Tokens.TokenTypes.Parantheses
                )
            );

            programText = programText.Remove(index, endParamsIndex + 1);
        }

        /* Add for params to tokens */
        private bool AddForParamsToTokens(string parameters, int endParamsIndex)
        {
            int semicolonIndex = parameters.IndexOf(";", StringComparison.Ordinal);
            var initializeParam = parameters.Substring(0, semicolonIndex);
            parameters = parameters.Remove(0, semicolonIndex + 1);

            semicolonIndex = parameters.IndexOf(";", StringComparison.Ordinal);
            var conditionParam = parameters.Substring(0, semicolonIndex);
            parameters = parameters.Remove(0, semicolonIndex + 1);

            var iterationParam = parameters.Substring(0, parameters.Length);

            var forParams = new[] { initializeParam.Trim(' '), conditionParam.Trim(' '), iterationParam.Trim(' ') };

            tokens.Add(
                new Tokens.Token("(", Tokens.TokenTypes.Parantheses)
            );

            for (int index = 0; index < 3; index++)
            {
                foreach (var operation in Tokens.Operations)
                {
                    var operationIndex = forParams[index].IndexOf(operation, StringComparison.Ordinal);

                    if (operationIndex >= 0)
                    {
                        var firstOperand = forParams[index].Substring(0, operationIndex).Trim();
                        forParams[index] = forParams[index].Remove(0, operationIndex).Trim().Remove(0, operation.Length).Trim();

                        var secondOperand = forParams[index].Substring(0, forParams[index].Length).Trim();

                        if (index != 2)
                        {
                            tokens.Add(
                                new Tokens.Token(firstOperand, Tokens.TokenTypes.Value)
                            );
                        }

                        tokens.Add(
                            new Tokens.Token(operation, Tokens.TokenTypes.Operation)
                        );

                        tokens.Add(
                            new Tokens.Token(secondOperand, Tokens.TokenTypes.Value)
                        );
                        
                        if (index < 2)
                        {
                            tokens.Add(
                                new Tokens.Token(";", Tokens.TokenTypes.Semicolon)
                            );
                        }

                        if (index == 2)
                        {
                            tokens.Add(
                                new Tokens.Token(")", Tokens.TokenTypes.Parantheses)
                            );

                            programText = programText.Remove(0, endParamsIndex + 1);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /* Add conditional params to tokens */
        private bool AddConditionParamsToTokens(string parameters, int endParamsIndex)
        {
            const int index = 0;

            while (parameters.Length > 0)
            {
                char currentChar = parameters[index];

                if (currentChar.Equals(' '))
                {
                    parameters = parameters.Remove(index, 1);
                    continue;
                }

                if ((currentChar.Equals('&') && parameters[index + 1].Equals('&')) ||
                     (currentChar.Equals('|') && parameters[index + 1].Equals('|')))
                {
                    tokens.Add(
                        new Tokens.Token(
                            parameters.Substring(index, 2),
                            Tokens.TokenTypes.Operation
                        )
                    );

                    parameters = parameters.Remove(index, 2);
                    continue;
                }

                if (currentChar.Equals('(') || currentChar.Equals(')'))
                {
                    tokens.Add(
                        new Tokens.Token(
                            currentChar,
                            Tokens.TokenTypes.Parantheses
                        )
                    );

                    parameters = parameters.Remove(index, 1);

                    if (!(parameters.Length > 0))
                    {
                        programText = programText.Remove(0, endParamsIndex + 1);
                        return true;
                    }

                    continue;
                }

                var operations = new List<Tuple<int, int>>();
                int operationIndex = 0;
                int operationNumber = 0;
                string currentOperation = "";

                foreach (var operation in Tokens.Operations)
                {
                    if (operation.Equals("++") || operation.Equals("--") || operation.Equals("=") ||
                        operation.Equals("+") || operation.Equals("-") || operation.Equals("*") || operation.Equals("/") ||
                        operation.Equals("&&") || operation.Equals("||"))
                        continue;

                    operationIndex = parameters.IndexOf(operation, StringComparison.Ordinal);

                    if (operationIndex >= 0)
                    {
                        operations.Add(
                            new Tuple<int, int>(operationIndex, operation.Length)
                        );
                    }
                }

                if (operations.Count > 0)
                {
                    operationIndex = 777;
                    for (int i = 0; i < operations.Count; i++)
                    {
                        if (operationIndex > operations[i].Item1)
                        {
                            operationIndex = operations[i].Item1;
                            operationNumber = i;
                        }
                    }

                    currentOperation = parameters.Substring(operationIndex, operations[operationNumber].Item2);
                }

                if (operationIndex >= 0)
                {
                    var firstOperand = parameters.Substring(0, operationIndex).Trim();
                    parameters = parameters.Remove(0, operationIndex + operations[operationNumber].Item2);

                    var positions = new[]
                    {
                            parameters.IndexOf(" ", StringComparison.Ordinal),
                            parameters.IndexOf(")", StringComparison.Ordinal),
                            parameters.IndexOf("&", StringComparison.Ordinal),
                            parameters.IndexOf("|", StringComparison.Ordinal),
                    };

                    var secondOperand = parameters.Substring(0, positions.Where(value => value > 0).Min()).Trim();

                    tokens.Add(
                        new Tokens.Token(firstOperand, Tokens.TokenTypes.Value)
                    );

                    tokens.Add(
                        new Tokens.Token(currentOperation, Tokens.TokenTypes.Operation)
                    );

                    tokens.Add(
                        new Tokens.Token(secondOperand, Tokens.TokenTypes.Value)
                    );

                    parameters = parameters.Remove(index, secondOperand.Length + 1);
                }
            }

            return false;
        }

        /* Add condition operator to tokens */
        private bool AddConditionOperatorToTokens()
        {
            foreach (var conditionOperator in Tokens.ConditionOperators)
            {
                if (programText.IndexOf(conditionOperator, StringComparison.Ordinal) == 0)
                {
                    AddValueToTokens(conditionOperator, Tokens.TokenTypes.Conditions, conditionOperator.Length);
                    return true;
                }
            }

            return false;
        }

        /* Get close parantheses ')' index */
        private int GetCloseParanthesesIndex(int index)
        {
            int closeParanthesesIndex = 0;
            int inParanthesesIndex = 0;

            for (int i = index; i < programText.Length; ++i)
            {
                var currentChar = programText[i];
                if (currentChar.Equals('('))
                    inParanthesesIndex++;

                if (currentChar.Equals(')'))
                {
                    inParanthesesIndex--;

                    if (inParanthesesIndex > 0)
                        continue;

                    closeParanthesesIndex = i;
                    break;
                }
            }

            return closeParanthesesIndex;
        }
        
        /**
         * Add all data about variables to tokens list
         * All variables have the next structure: _type _Name = _value
         * 
         * Variables have next methods:
         * - Add variable type
         * - Add variable name
         * - Add variable value
         */
        /* Add variable type to tokens */
        private bool AddVariableTypeToTokens()
        {
            foreach (var type in Tokens.Types)
            {
                if (programText.IndexOf(type, StringComparison.Ordinal) == 0 &&
                    programText[type.Length].Equals(' '))
                {
                    AddValueToTokens(type.ToString(), Tokens.TokenTypes.Type, type.Length + 1);

                    var positions = new[]
                    {
                        programText.IndexOf(" ", StringComparison.Ordinal),
                        programText.IndexOf(";", StringComparison.Ordinal)
                    };
                    
                    var variableName = programText.Substring(
                        0,
                        positions.Where(value => value > 0).Min()
                    );

                    AddValueToTokens(variableName, Tokens.TokenTypes.Word, variableName.Length);

                    if (!Tokens.Variables.ContainsKey(variableName))
                    {
                        Tokens.Variables[variableName] = new Variable(null, type);
                    }

                    return true;
                }
            }

            return false;
        }

        /* Add variable name to tokens */
        private bool AddVariableNameToTokens()
        {
            foreach (var variableName in Tokens.Variables.Keys)
            {
                if (programText.IndexOf(variableName, StringComparison.Ordinal) == 0)
                {
                    AddValueToTokens(variableName.ToString(), Tokens.TokenTypes.Word, variableName.Length);
                    return true;
                }
            }
            return false;
        }

        /* Add variable value to tokens */
        private void AddVariableValueToTokens()
        {
            List<int> positions = new List<int>()
            {
                //programText.IndexOf(" ", StringComparison.Ordinal),
                programText.IndexOf(";", StringComparison.Ordinal),
                programText.IndexOf("+", StringComparison.Ordinal),
                programText.IndexOf("-", StringComparison.Ordinal),
                programText.IndexOf("*", StringComparison.Ordinal),
                programText.IndexOf("/ ", StringComparison.Ordinal)
            };

            int nextOperationIndex = positions.Where(value => value > 0).Min();
            //int endVariableValueIndex = programText.IndexOf(';');
            var variableValue = programText.Substring(0, nextOperationIndex);

            AddValueToTokens(variableValue.ToString(), Tokens.TokenTypes.Value, variableValue.Length);
        }
    }
}