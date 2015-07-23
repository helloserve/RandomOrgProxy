using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg
{
    internal class BoxMuller
    {
        private double _z0;
        private double _z1;
        private bool _generate;

        private Random _random = new Random((int)DateTime.Now.Ticks);

        public double GenerateGaussian(double mean, double deviation)
        {
            const double two_pi = 2.0 * Math.PI;

            _generate = !_generate;

            if (!_generate)
                return _z1 * deviation + mean;

            double u1, u2;
            do
            {
                u1 = _random.NextDouble();
                u2 = _random.NextDouble();
            }
            while (u1 == 0);

            _z0 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(two_pi * u2);
            _z1 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(two_pi * u2);
            return _z0 * deviation + mean;
        }
    }
}
