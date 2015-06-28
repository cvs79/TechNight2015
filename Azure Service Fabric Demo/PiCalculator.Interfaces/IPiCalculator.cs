using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using System.Numerics;

namespace PiCalculator.Interfaces
{
    public interface IPiCalculator : IActor
    {
        Task<BigInteger> GetPi(int iterations);
    }
}
