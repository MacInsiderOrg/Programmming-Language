using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ProgrammingLanguage
{
    class SyntaxAnalyzer
    {
        // Tokens list
        private readonly List<Tokens.Token> tokens; 

        // Variables -> contain all variables with value and type
        private readonly Dictionary<string, Variable> variables;

        public Dictionary<string, Variable> Variables => variables;

        // Our methods
        private List<string> methods;

        // Delegate for my methods
        private delegate void Func(object[] param, WebBrowser webBrowser);

        // List of functions
        private readonly List<Func> functions;
        
        // If for loop operator starts, this value maked true, otherwise false
        private bool loopOperatorStart;

        private readonly WebBrowser webBrowser;

        /**
         * Initialize Syntax Analyzer, which checks tokens for errors,
         * and executes all code 
         */
        public SyntaxAnalyzer(List<Tokens.Token> tokens, List<string> methods, Dictionary<string, Variable> variables, WebBrowser webBrowser)
        {
            this.tokens = new List<Tokens.Token>();
            this.tokens = tokens;
            this.methods = methods;
           
            this.variables = new Dictionary<string, Variable>();
            this.variables = variables;
            this.webBrowser = webBrowser;
            
            functions = new List<Func>
            {
                Functions.OpenUrl,
                Functions.ClickOnButton,
                Functions.GetElementById,
                Functions.SetElementById,
                Functions.GetElementByName,
                Functions.SetElementByName,
                Functions.GetElementByClassName,
                Functions.SetElementByClassName
            };
        }

        /**
         * Create Tree
         * Recursive function, which is called, when
         * the program contain loops, conditions and 
         * another operators
         */
        public object CreateTree(ref int startIndex)
        {
            int index = startIndex + 1;

            if (index == tokens.Count - 1)
                return null;

            for (; index < tokens.Count; ++index)
            {
                // Create variable list
                CreateVariableList(ref index);

                // Analyze condition operators
                AnalyzeConditionOperators(ref index);

                // Run methods if finded
                RunMethods(ref index);
            }
            
            return null;
        }

        /**
         * Create variables list
         * If system finds a new variable and this variable 
         * doesn't appear previously,
         * system creates new variable in variables list,
         * otherwise variable value is rewritten by new value,
         * which is located in the right side
         */
        private void CreateVariableList(ref int index)
        {
            if (tokens[index].Type == Tokens.TokenTypes.Word)
            {
                var currentToken = tokens[index + 1];
                if (currentToken.Type == Tokens.TokenTypes.Operation)
                {
                    if ((currentToken.Value.Equals("=")) &&
                        (tokens[index + 3].Type == Tokens.TokenTypes.Operation))
                    {
                        var expression = new List<dynamic>();
                        dynamic resultValue = 0;
                        int semicolonIndex = 0;

                        for (int i = index + 2; i < tokens.Count; i++)
                        {
                            if (tokens[i].Type == Tokens.TokenTypes.Semicolon)
                            {
                                semicolonIndex = i;
                                break;
                            }
                        }

                        for (int i = index + 2; i < semicolonIndex; i++)
                        {
                            if (tokens[i + 1].Type == Tokens.TokenTypes.Operation &&
                                (tokens[i + 1].Value.Equals("*") || tokens[i + 1].Value.Equals("/")))
                            {
                                var leftOperand = SetOperandValue(i);
                                var rightOperand = SetOperandValue(i + 2);

                                string currentOperation = tokens[i + 1].Value.ToString();
                                
                                switch (currentOperation)
                                {
                                    case "*":
                                        resultValue = leftOperand * rightOperand;
                                        break;
                                    case "/":
                                        resultValue = leftOperand / rightOperand;
                                        break;
                                    default:
                                        resultValue = 0;
                                        break;
                                }

                                expression.Add(resultValue);

                                i += 3;

                                continue;
                            }

                            if (tokens[i].Type == Tokens.TokenTypes.Word || tokens[i].Type == Tokens.TokenTypes.Value)
                                expression.Add(SetOperandValue(i));
                        }

                        int iteration = 0;

                        for (int i = index + 2; i < semicolonIndex; i++)
                        {
                            if (tokens[i].Type == Tokens.TokenTypes.Operation &&
                                (tokens[i].Value.Equals("+") || tokens[i].Value.Equals("-")))
                            {
                                var leftOperand = SetExpressionOperand(expression[iteration++]);
                                var rightOperand = SetExpressionOperand(expression[iteration]);
                                
                                string currentOperation = tokens[i].Value.ToString();

                                switch (currentOperation)
                                {
                                    case "+":
                                        resultValue = leftOperand + rightOperand;
                                        break;
                                    case "-":
                                        resultValue = leftOperand - rightOperand;
                                        break;
                                    default:
                                        resultValue = 0;
                                        break;
                                }

                                if (TryParseTypes(resultValue) == "string")
                                {
                                    resultValue = $"\"{resultValue}\"";
                                }

                                if (expression.Count != 0)
                                    expression[iteration] = resultValue;
                            }
                        }

                        variables[tokens[index].Value] = SetVariableData(
                            resultValue,
                            index
                        );
                    }

                    else if (currentToken.Value.Equals("="))
                    {
                        variables[tokens[index].Value] = SetVariableData(
                            tokens[index + 2].Value,
                            index
                        );
                    }

                    else if (currentToken.Value.Equals("++"))
                    {
                        variables[tokens[index].Value] = SetVariableData(
                            variables[tokens[index].Value].Value + 1,
                            index
                        );
                    }

                    else if (currentToken.Value.Equals("--"))
                    {
                        variables[tokens[index].Value] = SetVariableData(
                            variables[tokens[index].Value].Value - 1,
                            index
                        );
                    }

                    for (int i = index; i < tokens.Count; i++)
                    {
                        if (tokens[i].Type == Tokens.TokenTypes.Semicolon)
                        {
                            index = i;
                            break;
                        }
                    }
                }
            }
        }

        /**
         * Contain Loops and condition operators
         * Method can call next operators:
         * - while loop 
         * - for loop
         * - if condition
         */
        private void AnalyzeConditionOperators(ref int index)
        {
            if (tokens[index].Type == Tokens.TokenTypes.Conditions)
            {
                var flowControl = tokens[index].Value;

                if (flowControl.Equals("while"))
                    WhileLoopOperator(ref index);
                
                else if (flowControl.Equals("for"))
                    ForLoopOperator(ref index);
                
                else if (flowControl.Equals("if"))
                    IfConditionOperator(ref index);
            }
        }

        /**
         * Loops operators
         * - While -> while (conditionParam) { // operators }
         * - For   -> for (initializeParam; conditionParam; iterationParam) { // operators }
         */

        /**
         * While loop operator
         * Repeat operators, when the while condition is successful,
         * otherwise leave from the current loop
         */
        private void WhileLoopOperator(ref int index)
        {
            int openBracesIndex = GetOpenBracesIndex(index);
            int closeBracesIndex = GetCloseBracesIndex(index);

            while (true)
            {
                if (CheckCondition(index, openBracesIndex - 1))
                    CreateTree(ref openBracesIndex);
                else
                {
                    index = closeBracesIndex;
                    break;
                }
            }
        }

        /**
         * For loop operator
         * Repeat inline operators, when the conditionParam is successful,
         * otherwise leave from the current loop
         */
        private void ForLoopOperator(ref int index)
        {
            int openBracesIndex = index + 13;
            int closeBracesIndex = GetCloseBracesIndex(index);

            if (!loopOperatorStart)
            {
                variables[tokens[index + 2].Value] = SetVariableData(
                    tokens[index + 4].Value,
                    index + 2
                );
                loopOperatorStart = true;
            }

            while (true)
            {
                var cycleOperand = GetOperandValue(tokens[index + 2].Value);
                var operandType = TryParseTypes(cycleOperand);
                cycleOperand = SetVariableType(cycleOperand, operandType);

                var conditionOperand = GetOperandValue(tokens[index + 8].Value);
                operandType = TryParseTypes(conditionOperand);
                conditionOperand = SetVariableType(conditionOperand, operandType);

                loopOperatorStart = true;
                if (CheckOperationInCondition(cycleOperand, conditionOperand, tokens[index + 7].Value))
                {
                    if (tokens[index + 10].Value.Equals("++"))
                    {
                        variables[tokens[index + 2].Value] = SetVariableData(
                            variables[tokens[index + 2].Value].Value + 1,
                            index + 2
                        );
                    }

                    else if (tokens[index + 10].Value.Equals("--"))
                    {
                        variables[tokens[index + 2].Value] = SetVariableData(
                            variables[tokens[index + 2].Value].Value - 1,
                            index + 2
                        );
                    }

                    CreateTree(ref openBracesIndex);
                }
                else
                {
                    index = closeBracesIndex;
                    loopOperatorStart = false;
                    break;
                }
            }
        }

        /**
         * If condition operator
         * Check if current condition is successful and
         * execute inner operators, 
         * otherwise leave from the current operator
         */
        private void IfConditionOperator(ref int index)
        {
            int openBracesIndex = GetOpenBracesIndex(index);
            int closeBracesIndex = GetCloseBracesIndex(index);

            index = CheckCondition(index, openBracesIndex - 1) ? openBracesIndex : closeBracesIndex;
        }

        /**
         * Execute Methods
         * If system finds method in token list,
         * it redirect to execute this method in
         * static class Function and it runs
         */
        private void RunMethods(ref int index)
        {
            if (tokens[index].Type == Tokens.TokenTypes.Method)
            {
                for (int i = 0; i < Tokens.Methods.Count; i++)
                {
                    var method = Tokens.Methods[i];

                    if (method.Equals(tokens[index].Value))
                    {
                        dynamic param1 = GetOperandValue(tokens[index + 2].Value);
                        dynamic param2 = null;

                        if (tokens[index + 3].Type == Tokens.TokenTypes.Coma)
                            param2 = GetOperandValue(tokens[index + 4].Value);

                        object[] parameters = (param2 == null) ? new object[] { param1 } : new object[] { param1, param2 };

                        functions[i](parameters, webBrowser);
                        break;
                    }
                }
            }
        }

        /* Check condition in while and if operators */
        private bool CheckCondition(int index, int closeParanthesesIndex)
        {
            var conditionsList = new List<bool>();

            for (int i = index + 1; i < closeParanthesesIndex; i++)
            {
                if (tokens[i].Type == Tokens.TokenTypes.Operation &&
                    !(tokens[i].Value.Equals("&&") || tokens[i].Value.Equals("||")))
                {
                    var leftOperand = GetOperandValue(tokens[i - 1].Value);
                    var rightOperand = GetOperandValue(tokens[i + 1].Value);

                    var rightOperatorType = TryParseTypes(rightOperand);
                    rightOperand = SetVariableType(rightOperand, rightOperatorType);

                    conditionsList.Add(CheckOperationInCondition(leftOperand, rightOperand, tokens[i].Value));
                }
            }

            int iteration = 0;

            for (int i = index + 1; i < closeParanthesesIndex; i++)
            {
                if (tokens[i].Type == Tokens.TokenTypes.Operation &&
                    (tokens[i].Value.Equals("&&") || tokens[i].Value.Equals("||")))
                {
                    var leftOperand = conditionsList[iteration++];
                    var rightOperand = conditionsList[iteration];

                    if (CheckOperationInCondition(leftOperand, rightOperand, tokens[i].Value))
                        conditionsList[iteration] = true;
                    else
                        conditionsList[iteration] = false;
                }

            }

            if (conditionsList.Count < 1)
                return false;

            if (conditionsList[conditionsList.Count - 1])
                return true;

            return false;
        }

        /* Get open braces index "{" of current context */
        private int GetOpenBracesIndex(int index)
        {
            int openBracesIndex = 0;
            for (int i = index; i < tokens.Count; ++i)
            {
                if (tokens[i].Value.Equals("{"))
                {
                    openBracesIndex = i;
                    break;
                }
            }

            return openBracesIndex;
        }


        /* Get close braces index "}" of current context */
        private int GetCloseBracesIndex(int index)
        {
            int closeBracesIndex = 0;
            int inBracesIndex = 0;

            for (int i = index; i < tokens.Count; ++i)
            {
                if (tokens[i].Value.Equals("{"))
                    inBracesIndex++;

                if (tokens[i].Value.Equals("}"))
                {
                    inBracesIndex--;

                    if (inBracesIndex > 0)
                        continue;

                    closeBracesIndex = i;
                    break;
                }
            }

            return closeBracesIndex;
        }

        /* Check operation (>, < or =) */
        private bool CheckOperationInCondition(dynamic leftOperand, dynamic rightOperand, dynamic operation)
        {
            return ( operation.Equals("&&") && (leftOperand == rightOperand) == true ||
                     operation.Equals("||") && (leftOperand == true || rightOperand == true) ||
                     operation.Equals("==") &&  leftOperand == rightOperand ||
                     operation.Equals("!=") &&  leftOperand != rightOperand ||
                     operation.Equals(">=") &&  leftOperand >= rightOperand ||
                     operation.Equals("<=") &&  leftOperand <= rightOperand ||
                     operation.Equals(">")  &&  leftOperand >  rightOperand ||
                     operation.Equals("<")  &&  leftOperand <  rightOperand );
        }

        /* Set operand value in variable list */
        private dynamic SetOperandValue(int tokenIndex)
        {
            Tokens.Token operand = tokens[tokenIndex];
            dynamic operandValue = 0;
            
            if (operand.Type == Tokens.TokenTypes.Word)
            {
                operandValue = SetVariableType(
                    variables
                        .Where(pair => pair.Key.ToString() == operand.Value)
                        .Select(pair => pair.Value)
                        .FirstOrDefault()?.Value,
                    GetVariableType(tokenIndex)
                );
            }
            else
            {
                operandValue = SetVariableType(
                    SetVariableType(operand.Value, TryParseTypes(operand.Value)),
                    TryParseTypes(operand.Value)
                );
            }

            return operandValue;
        }

        /* Get operand value if this is variable, or get number or text from code */
        private dynamic GetOperandValue(dynamic operand)
        {
            return variables.ContainsKey(operand) ? variables[operand].Value : operand;
        }

        /**
         * Set variable data
         * First parameter is variable value in current type
         * Second parameter is variable type
         */
        private Variable SetVariableData(object variable, int index)
        {
            return new Variable(
                SetVariableType(
                    variable,
                    GetVariableType(index)
                ),
                GetVariableType(index)
            );
        } 

        /* Set variable type by string value, which was finded before */
        private object SetVariableType(object variable, string variableType)
        {
            switch (variableType)
            {
                case "int":
                    return Convert.ToInt32(variable);

                case "double":
                    return Convert.ToDouble(variable);
                    
                case "string":
                    return Convert.ToString(variable);

                case "bool":
                    return Convert.ToBoolean(variable);

                default:
                    return variable;
            }
        }

        /* Get variable type by position of tokens */
        private string GetVariableType(int index)
        {
            for (int i = 0; i <= index; i++)
            {
                if (tokens[index].Value.ToString() == tokens[i].Value.ToString())
                {
                    return tokens[i - 1].Value.ToString();
                }
            }

            return "";
        }

        /* Detect variable type by parsing value */
        private string TryParseTypes(object valueToConvert)
        {
            int intTemp;
            double doubleTemp;
            bool boolTemp;

            if (int.TryParse(valueToConvert.ToString(), out intTemp))
                return "int";

            if (double.TryParse(valueToConvert.ToString(), out doubleTemp))
                return "double";

            if (bool.TryParse(valueToConvert.ToString(), out boolTemp))
                return "bool";

            return "string";
        }

        /* Set operand value in expression */
        private dynamic SetExpressionOperand(dynamic operand)
        {
            var operandType = TryParseTypes(operand);
            operand = SetVariableType(operand, operandType);

            if (operandType == "string")
            {
                object value = operand;
                operand = value.ToString().Trim('"');
            }

            return operand;
        }
    }
}
