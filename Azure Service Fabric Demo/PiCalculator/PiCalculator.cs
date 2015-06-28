using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PiCalculator.Interfaces;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Actors;
using System.Numerics;

namespace PiCalculator
{
    #region PartDeux
    //[Serializable]
    //public class PiCalculatorState
    //{
    //    public int digits { get; set; }

    //    public BigInteger pi { get; set; }
    //}
    #endregion

    public class PiCalculator : Actor, IPiCalculator
    {
        private int digits = 1;
        private BigInteger pi = 0;

        public Task<BigInteger> GetPi(int iterations)
        {
            this.pi = 16 * ArcTan1OverX(5, this.digits).ElementAt(iterations)
            - 4 * ArcTan1OverX(239, this.digits).ElementAt(iterations);

            this.digits++;

            return Task.FromResult(this.pi);
        }

        #region PiCalc
        private IEnumerable<BigInteger> ArcTan1OverX(int x, int digits)
        {
            var mag = BigInteger.Pow(10, digits);
            var sum = BigInteger.Zero;
            bool sign = true;
            for (int i = 1; true; i += 2)
            {
                var cur = mag / (BigInteger.Pow(x, i) * i);
                if (sign)
                {
                    sum += cur;
                }
                else
                {
                    sum -= cur;
                }
                yield return sum;
                sign = !sign;
            }
        }
        #endregion
    }
}
