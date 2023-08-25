using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora.Operations
{
    interface IOperation
    {
        double Calculate(double firstNumber, double secondNumber);
    }
}
