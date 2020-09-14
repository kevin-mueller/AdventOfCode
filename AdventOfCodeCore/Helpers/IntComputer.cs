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

                    switch (opCode)
                    {
                        case 01:
                            {
                                numberOfParameters = 3;
                                p = GetParameters(sourceCode, i, numberOfParameters);
                                
                                sourceCode[p[2].ImmediateValue] = p[0].Value + p[1].Value;
                                break;
                            }
                        case 02:
                            {
                                numberOfParameters = 3;
                                p = GetParameters(sourceCode, i, numberOfParameters);

                                sourceCode[p[2].ImmediateValue] = p[0].Value * p[1].Value;
                                break;
                            }
                        case 03:
                            {
                                numberOfParameters = 1;
                                p = GetParameters(sourceCode, i, numberOfParameters);

                                sourceCode[p[0].ImmediateValue] = parameters[0];
                                break;
                            }
                        case 04:
                            {
                                numberOfParameters = 1;
                                p = GetParameters(sourceCode, i, numberOfParameters);

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
                Console.WriteLine(ex.StackTrace);
                return returnValues;
            }
            return null;
        }

        private static List<Parameter> GetParameters(int[] sourceCode, int pointer, int numberOfParameters)
        {
            List<Parameter> res = new List<Parameter>();
            string instructionString = sourceCode[pointer].ToString();

            int expectedLength = numberOfParameters + 2;
            while (instructionString.Length < expectedLength)
                instructionString = "0" + instructionString;

            
            for (int i = 1; i <= numberOfParameters; i++)
            {
                res.Add(new Parameter(sourceCode, sourceCode[pointer + i], Convert.ToInt32(instructionString.ToString().Tail(2 + i)[0].ToString()).ToParameterMode()));
            }

            return res;
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