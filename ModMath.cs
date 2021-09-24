using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialMod
{
    public class ModMath
    {
        public static double NewtonMethod(double a, double b, double c, double d, double e, double guess)
        {
            int i = 0;
            double x0 = guess;
            while (i < 10000)
            {
                x0 = x0 - (a * Math.Pow(x0, 4) + b * Math.Pow(x0, 3) + c * Math.Pow(x0, 2) + d * x0 + e) / (a * 4 * Math.Pow(x0, 3) + b * 3 * Math.Pow(x0, 2) + c * 2 * x0 + d);
                i++;
            }
            return x0;
        }
    }
}
