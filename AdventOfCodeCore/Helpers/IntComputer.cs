using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    public static class IntComputer
    {
        public static int Run(int? noun, int? verb, int[] sourceCode, params int[] parameters)
        {
            if (noun != null)
                sourceCode[1] = noun.Value;
            if (verb != null)
                sourceCode[2] = verb.Value;
            try
            {
                for (int i = 0; i < sourceCode.Length; i += 4)
                {
                    if (sourceCode[i].ToString().Length > 1)
                    {
                        int opCode = Convert.ToInt32(sourceCode[i].ToString().Tail(2));
                        int numberOfParams;
                        if (opCode == 01 || opCode == 02)
                            numberOfParams = 3;
                        else if (opCode == 3 || opCode == 4)
                            numberOfParams = 1;



                        switch (opCode)
                        {
                            case 1:
                                sourceCode[sourceCode[i + 3]] = sourceCode[sourceCode[i + 1]] + sourceCode[sourceCode[i + 2]];
                                break;

                            case 2:
                                sourceCode[sourceCode[i + 3]] = sourceCode[sourceCode[i + 1]] * sourceCode[sourceCode[i + 2]];
                                break;

                            case 3:
                                sourceCode[i + 1] = parameters[0];
                                break;

                            case 4:
                                return sourceCode[sourceCode[i + 1]];

                            case 99:
                                throw new Exception();
                        }
                    }
                    else
                    {
                        //old instrucitons
                        switch (sourceCode[i])
                        {
                            case 1:
                                sourceCode[sourceCode[i + 3]] = sourceCode[sourceCode[i + 1]] + sourceCode[sourceCode[i + 2]];
                                break;

                            case 2:
                                sourceCode[sourceCode[i + 3]] = sourceCode[sourceCode[i + 1]] * sourceCode[sourceCode[i + 2]];
                                break;

                            case 3:
                                sourceCode[i + 1] = parameters[0];
                                break;

                            case 4:
                                return sourceCode[sourceCode[i + 1]];

                            case 99:
                                throw new Exception();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return sourceCode[0];
            }
            return -1;
        }
    }

    public class Instruction
    {
        public int OpCode { get; set; }
        public List<Parameter> Parameters = new List<Parameter>();
        public Instruction(int instruction, int[] sourceCode)
        {
            OpCode = Convert.ToInt16(instruction.ToString().Tail(2));

            if (OpCode == 02 || OpCode == 01)
            {
                //three parameters
                var paramValue = Convert.ToInt32(instruction.ToString().Tail(3)[0].ToString());
                Parameters.Add(new Parameter(sourceCode, 0, paramValue.ToParameterMode()))
            }
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