using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora.Operations
{
    public class Addition : IOperation
    {
        public double Calculate(double firstNumber, double secondNumber)
        {
            return firstNumber + secondNumber;
        }
    }
}
