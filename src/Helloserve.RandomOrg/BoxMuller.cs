/*
Copyright 2019 Henk Roux (helloserve Productions)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;

namespace Helloserve.RandomOrg
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
