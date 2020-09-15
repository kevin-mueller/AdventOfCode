using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    public class IntComputer
    {
        private readonly int[] initialSourceCode;
        private int numberOfInputInstructions;
        private int[] sourceCode;
        public IntComputer(int[] sourceCode)
        {
            this.sourceCode = sourceCode;
            initialSourceCode = sourceCode;

            numberOfInputInstructions = -1;
        }

        public List<int> Run(int? noun = null, int? verb = null, params int[] parameters)
        {
            if (noun != null)
                sourceCode[1] = noun.Value;
            if (verb != null)
                sourceCode[2] = verb.Value;

            List<int> returnValues = new List<int>();
            try
            {
                int increment = 3; //default
                for (int i = 0; i < sourceCode.Length; i += increment + 1)
                {

                    var opCode = Convert.ToInt32(sourceCode[i].ToString().Tail(2));
                    List<Parameter> p = new List<Parameter>();

                    switch (opCode)
                    {
                        case 01:
                            {
                                p = GetParameters(i, 3);

                                sourceCode[p[2].ImmediateValue] = p[0].Value + p[1].Value;
                                break;
                            }
                        case 02:
                            {
                                p = GetParameters(i, 3);

                                sourceCode[p[2].ImmediateValue] = p[0].Value * p[1].Value;
                                break;
                            }
                        case 03:
                            {
                                numberOfInputInstructions++;
                                //if (numberOfInputInstructions > parameters.Length -1)
                                //    numberOfInputInstructions = 0;

                                p = GetParameters(i, 1);

                                sourceCode[p[0].ImmediateValue] = parameters[numberOfInputInstructions];
                                break;
                            }
                        case 04:
                            {
                                p = GetParameters(i, 1);

                                returnValues.Add(p[0].Value);
                                break;
                            }
                        case 05:
                            {
                                p = GetParameters(i, 2);
                                if (p[0].Value != 0)
                                {
                                    i = p[1].Value;

                                    //set increment to -1 so the for loop does not increment on its own. A bit ugly, I know...
                                    increment = -1;
                                    continue;
                                }
                                break;
                            }
                        case 06:
                            {
                                p = GetParameters(i, 2);
                                if (p[0].Value == 0)
                                {
                                    i = p[1].Value;

                                    //set increment to -1 so the for loop does not increment on its own. A bit ugly, I know...
                                    increment = -1;
                                    continue;
                                }
                                break;
                            }
                        case 07:
                            {
                                p = GetParameters(i, 3);
                                if (p[0].Value < p[1].Value)
                                {
                                    sourceCode[p[2].ImmediateValue] = 1;
                                }
                                else
                                {
                                    sourceCode[p[2].ImmediateValue] = 0;
                                }
                                break;
                            }
                        case 08:
                            {
                                p = GetParameters(i, 3);
                                if (p[0].Value == p[1].Value)
                                {
                                    sourceCode[p[2].ImmediateValue] = 1;
                                }
                                else
                                {
                                    sourceCode[p[2].ImmediateValue] = 0;
                                }
                                break;
                            }
                        case 99:
                            {
                                throw new Exception("Program halt.");
                            }
                        default:
                            throw new Exception("Unexpected OpCode");
                    }
                    increment = p.Count;
                }
            }
            catch (Exception ex)
            {
                if (!ex.Message.Equals("Program halt."))
                    Console.WriteLine(ex.StackTrace);
                return returnValues;
            }
            return null;
        }

        public void Reset()
        {
            sourceCode = initialSourceCode;
            numberOfInputInstructions = -1;
        }

        private List<Parameter> GetParameters(int pointer, int numberOfParameters)
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