using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    public static class IntComputer
    {
        public static List<int> Run(int? noun, int? verb, int[] sourceCode, params int[] parameters)
        {
            if (noun != null)
                sourceCode[1] = noun.Value;
            if (verb != null)
                sourceCode[2] = verb.Value;

            List<int> returnValues = new List<int>();
            try
            {
                int numberOfParameters = 3; //default
                for (int i = 0; i < sourceCode.Length; i += numberOfParameters + 1)
                {

                    var opCode = Convert.ToInt32(sourceCode[i].ToString().Tail(2));
                    List<Parameter> p = new List<Parameter>();
                    string instructionString = sourceCode[i].ToString();

                    switch (opCode)
                    {
                        case 01:
                        case 02:
                            {
                                numberOfParameters = 3;
                                int expectedLength = 4;
                                while (instructionString.Length < expectedLength)
                                    instructionString = "0" + instructionString;

                                //three parameters
                                p.Add(new Parameter(sourceCode, sourceCode[i + 1], Convert.ToInt32(instructionString.ToString().Tail(3)[0].ToString()).ToParameterMode()));
                                p.Add(new Parameter(sourceCode, sourceCode[i + 2], Convert.ToInt32(instructionString.ToString().Tail(4)[0].ToString()).ToParameterMode()));
                                p.Add(new Parameter(sourceCode, sourceCode[i + 3], Convert.ToInt32(instructionString.ToString().Tail(5)[0].ToString()).ToParameterMode()));
                                break;
                            }

                        case 03:
                        case 04:
                            {
                                numberOfParameters = 1;
                                int expectedLength = 2;
                                while (instructionString.Length < expectedLength)
                                    instructionString = "0" + instructionString;

                                //one parameter
                                p.Add(new Parameter(sourceCode, sourceCode[i + 1], Convert.ToInt32(instructionString.Tail(3)[0].ToString()).ToParameterMode()));
                                break;
                            }

                        case 99:
                            throw new Exception("Program halt.");
                        default:
                            throw new Exception("Invalid OpCode!");
                    }

                    switch (opCode)
                    {
                        case 01:
                            {
                                
                                break;
                            }
                        case 02:
                            {
                                sourceCode[p[2].ImmediateValue] = p[0].Value * p[1].Value;
                                break;
                            }
                        case 03:
                            {
                                sourceCode[p[0].ImmediateValue] = parameters[0];
                                break;
                            }
                        case 04:
                            {
                                returnValues.Add(p[0].Value);
                                break;
                            }
                        case 99:
                            {
                                throw new Exception("Program halt.");
                            }
                        default:
                            throw new Exception("Unexpected OpCode");
                    }
                }
            }
            catch (Exception ex)
            {
                return returnValues;
            }
            return null;
        }
    }

    public class Parameter
    {
        public ParameterMode Mode { get; set; }
        public int Value
        {
            get
            {
                if (Mode == ParameterMode.Immediate)
                    return _value;

                if (Mode == ParameterMode.Position)
                    return sourceCode[_value];

                return -1;
            }
        }

        public int ImmediateValue { get => _value; }

        private readonly int _value;
        private readonly int[] sourceCode;
        public Parameter(int[] sourceCode, int value, ParameterMode mode)
        {
            this.sourceCode = sourceCode;
            _value = value;
            Mode = mode;
        }
    }

    public enum ParameterMode
    {
        Position,
        Immediate
    }
}