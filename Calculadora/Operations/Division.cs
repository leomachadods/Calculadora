using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora.Operations
{
    public class Division : IOperation
    {
        public double Calculate(double firstNumber, double secondNumber)
        {
            if(secondNumber == 0)
            {
                throw new DivideByZeroException("Division by zero is now allowed");
            }
            return firstNumber / secondNumber;
        }
    }
}
