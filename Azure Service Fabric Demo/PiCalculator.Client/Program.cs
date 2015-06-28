using PiCalculator.Interfaces;
using Microsoft.ServiceFabric.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PiCalculator.Client
{
    public class Program
    {
        private const string ApplicationName = "fabric:/PiCalculatorApplication";

        public static void Main(string[] args)
        {
            var piCalculator = ActorProxy.Create<IPiCalculator>(
                 new ActorId("PiCalculator-1"),
                 ApplicationName);

            PrintConnnectionInfo(piCalculator);

            for (; ;)
            {
                var pi = piCalculator.GetPi(10).Result;
                var piString = pi.ToString().Insert(1, ".");

                Console.WriteLine(@"Calculator {1} value: {0}", piString, piCalculator.GetActorId());
                Console.WriteLine();

                Thread.Sleep(1000);
            }
        }

        #region printInfo
        private static void PrintConnnectionInfo(IPiCalculator piCalculator)
        {
            var proxy = piCalculator as IActorProxy;
            var uri = piCalculator.GetActorReference().ServiceUri;

            proxy.ActorServicePartitionClient.Factory.ClientConnected += Factory_ClientConnected;
        }

        private static void Factory_ClientConnected(object sender, Microsoft.ServiceFabric.Services.CommunicationClientEventArgs<Microsoft.ServiceFabric.Actors.Communication.Contract.IActorCommunicationClient> e)
        {
            var ep = e.Client.ResolvedServicePartition.Endpoints;
            string endpointAdress = string.Empty;

            if (ep.Count == 1)
            {
                endpointAdress = ep.First().Address;
            }
            else if (ep.Count > 1)
            {
                endpointAdress = ep.Where(endpoint => endpoint.Role == System.Fabric.ServiceEndpointRole.StatefulPrimary).First().Address;
            }
            else
            {
                endpointAdress = "Cant connect";
            }
            Console.WriteLine(@"## Connected to actor service at: {0}", endpointAdress);
        }

        #endregion
    }
}
