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
using System.Threading.Tasks;

namespace Helloserve.RandomOrg
{
    public interface IRandomOrgClient
    {
        /// <summary>
        /// Gets or sets a global setting for generation with replacement. Setting it to false means that no sequence of generated results will have the same value twice. The Random.Org default is true, and this setting is not applicable to Gaussians.
        /// </summary>
        bool WithReplacement { get; set; }

        /// <summary>
        /// Gets a value indicating if there are enough usages left to make another request. Does not take the advised time delay into account.
        /// </summary>
        bool CanMakeRequest { get; }
        /// <summary>
        /// Gets a value indicating the next request time advised by Random.Org
        /// </summary>
        DateTime NextRequestTime { get; }
        /// <summary>
        /// Asynchronously returns the usage count left for your API key
        /// </summary>
        /// <returns></returns>
        Task<int> GetUsageLeftAsync();
        /// <summary>
        /// Returns the usage count left for your API key
        /// </summary>
        /// <returns></returns>
        int GetUsageLeft();

        /// <summary>
        /// Gets a single random integer value between min and max.
        /// </summary>
        /// <param name="min">The minimum value of the random range. Must be greater than -1e9.</param>
        /// <param name="max">The maximum value of the random range. Must be less than 1e9.</param>
        /// <returns>A random integer value.</returns>
        Task<int> GetIntegerAsync(int min, int max);
        /// <summary>
        /// Gets a single random integer value between min and max.
        /// </summary>
        /// <param name="min">The minimum value of the random range. Must be greater than -1e9.</param>
        /// <param name="max">The maximum value of the random range. Must be less than 1e9.</param>
        /// <returns>A random integer value.</returns>
        int GetInteger(int min, int max);
        /// <summary>
        /// Gets an array of random integers.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="min">The minimum value of the random range. Must be greater than 1e9.</param>
        /// <param name="max">The maximum value of the random range. Must be less than 1e9.</param>
        /// <returns>A integer array of random numbers.</returns>
        Task<int[]> GetIntegersAsync(int count, int min, int max);
        /// <summary>
        /// Gets an array of random integers.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="min">The minimum value of the random range. Must be greater than 1e9.</param>
        /// <param name="max">The maximum value of the random range. Must be less than 1e9.</param>
        /// <returns>A integer array of random numbers.</returns>
        int[] GetIntegers(int count, int min, int max);

        /// <summary>
        /// Gets a single random integer value between min and max, with a specified base.
        /// </summary>
        /// <param name="min">The minimum value of the random range. Must be greater than -1e9.</param>
        /// <param name="max">The maximum value of the random range. Must be less than 1e9.</param>
        /// <param name="numberBase">The base of the integer value to return.</param>
        /// <returns>A random integer value.</returns>
        Task<int> GetIntegerBaseAsync(int min, int max, IntegerBase integerBase);
        /// <summary>
        /// Gets a single random integer value between min and max.
        /// </summary>
        /// <param name="min">The minimum value of the random range. Must be greater than -1e9.</param>
        /// <param name="max">The maximum value of the random range. Must be less than 1e9.</param>
        /// <param name="numberBase">The base of the integer value to return.</param>
        /// <returns>A random integer value.</returns>
        int GetIntegerBase(int min, int max, IntegerBase integerBase);
        /// <summary>
        /// Gets an array of random integers.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="min">The minimum value of the random range. Must be greater than 1e9.</param>
        /// <param name="max">The maximum value of the random range. Must be less than 1e9.</param>
        /// <param name="numberBase">The base of the integer value to return.</param>
        /// <returns>A integer array of random numbers.</returns>
        Task<int[]> GetIntegersBaseAsync(int count, int min, int max, IntegerBase integerBase);
        /// <summary>
        /// Gets an array of random integers.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="min">The minimum value of the random range. Must be greater than 1e9.</param>
        /// <param name="max">The maximum value of the random range. Must be less than 1e9.</param>
        /// <param name="numberBase">The base of the integer value to return.</param>
        /// <returns>A integer array of random numbers.</returns>
        int[] GetIntegersBase(int count, int min, int max, IntegerBase integerBase);

        /// <summary>
        /// Gets a single random double value between 0.0 and 1.0.
        /// </summary>
        /// <param name="decimalPlaces">The number of decimal places for the value. Must range from 1 to 20.</param>
        /// <returns>A random double value between 0.0 and 1.0.</returns>
        Task<double> GetDoubleAsync(int decimalPlaces);
        /// <summary>
        /// Gets a single random double value between 0.0 and 1.0.
        /// </summary>
        /// <param name="decimalPlaces">The number of decimal places for the value. Must range from 1 to 20.</param>
        /// <returns>A random double value between 0.0 and 1.0.</returns>
        double GetDouble(int decimalPlaces);
        /// <summary>
        /// Gets an array of random double values, between 0.0 and 1.0.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="decimalPlaces">The number of decimal places for each value. Must range from 1 and 20.</param>
        /// <returns>An double array of random values between 0.0 and 1.0.</returns>
        Task<double[]> GetDoublesAsync(int count, int decimalPlaces);
        /// <summary>
        /// Gets an array of random double values, between 0.0 and 1.0.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="decimalPlaces">The number of decimal places for each value. Must range from 1 and 20.</param>
        /// <returns>An double array of random values between 0.0 and 1.0.</returns>
        double[] GetDoubles(int count, int decimalPlaces);

        /// <summary>
        /// Returns a single random double value from a standard Gaussian distribution.
        /// </summary>
        /// <returns>A random double value between 0.0 and 1.0.</returns>
        Task<double> GetGaussianAsync();
        /// <summary>
        /// Returns a single random double value from a standard Gaussian distribution.
        /// </summary>
        /// <returns>A random double value between 0.0 and 1.0.</returns>
        double GetGaussian();
        /// <summary>
        /// Returns a single random double value between 0.0 and 1.0 from a Gaussian distribution.
        /// </summary>
        /// <param name="mean">The distribution's mean. Must range from -1e6 to 1e6.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must range from -1e6 to 1e6.</param>
        /// <param name="significantDigits">The number of significant digits to use. Must range from 2 to 20.</param>
        /// <returns>A random double value between 0.0 and 1.0.</returns>
        Task<double> GetGaussianAsync(double mean, double standardDeviation, int significantDigits);
        /// <summary>
        /// Returns a single random double value between 0.0 and 1.0 from a Gaussian distribution.
        /// </summary>
        /// <param name="mean">The distribution's mean. Must range from -1e6 to 1e6.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must range from -1e6 to 1e6.</param>
        /// <param name="significantDigits">The number of significant digits to use. Must range from 2 to 20.</param>
        /// <returns>A random double value between 0.0 and 1.0.</returns>
        double GetGaussian(double mean, double standardDeviation, int significantDigits);
        /// <summary>
        /// Gets an array of random double values between 0.0 and 1.0 from a standard Gaussian distribution.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <returns>A double array of random values between 0.0 and 1.0.</returns> 
        Task<double[]> GetGaussiansAsync(int count);
        /// <summary>
        /// Gets an array of random double values between 0.0 and 1.0 from a standard Gaussian distribution.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <returns>A double array of random values between 0.0 and 1.0.</returns> 
        double[] GetGaussians(int count);
        /// <summary>
        /// Returns an array of random double values between 0.0 and 1.0 from a Gaussian distribution.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="mean">The distribution's mean. Must range from -1e6 to 1e6.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must range from -1e6 to 1e6.</param>
        /// <param name="significantDigits">The number of significant digits to use. Must range from 2 to 20.</param>
        /// <returns>A double array of random values.</returns> 
        Task<double[]> GetGaussiansAsync(int count, double mean, double standardDeviation, int significantDigits);
        /// <summary>
        /// Returns an array of random double values between 0.0 and 1.0 from a Gaussian distribution.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="mean">The distribution's mean. Must range from -1e6 to 1e6.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must range from -1e6 to 1e6.</param>
        /// <param name="significantDigits">The number of significant digits to use. Must range from 2 to 20.</param>
        /// <returns>A double array of random values.</returns> 
        double[] GetGaussians(int count, double mean, double standardDeviation, int significantDigits);

        /// <summary>
        /// Gets a random string based on the content of the AllowedStringCharacters property.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <returns>A random string value.</returns>
        Task<string> GetStringAsync(int length);
        /// <summary>
        /// Gets a random string based on the content of the AllowedStringCharacters property.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <returns>A random string value.</returns>
        string GetString(int length);
        /// <summary>
        /// Gets a random string.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <param name="characters">A array containing all the characters allowed in the random string. Maximum amount is 80 characters.</param>
        /// <returns>A random string value.</returns>
        Task<string> GetStringAsync(int length, char[] characters);
        /// <summary>
        /// Gets a random string.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <param name="characters">A array containing all the characters allowed in the random string. Maximum amount is 80 characters.</param>
        /// <returns>A random string value.</returns>
        string GetString(int length, char[] characters);
        /// <summary>
        /// Gets a random string.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <param name="characters">A string containing all the characters allowed in the random string. Maximum amount is 80 characters.</param>
        /// <returns>A random string value.</returns>
        Task<string> GetStringAsync(int length, string characters);
        /// <summary>
        /// Gets a random string.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <param name="characters">A string containing all the characters allowed in the random string. Maximum amount is 80 characters.</param>
        /// <returns>A random string value.</returns>
        string GetString(int length, string characters);
        /// <summary>
        /// Gets a random string array based on the content of the AllowedStringCharacters property.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="length">The length of the strings.</param>
        /// <returns>A string array of random values.</returns>
        Task<string[]> GetStringsAsync(int count, int length);
        /// <summary>
        /// Gets a random string array based on the content of the AllowedStringCharacters property.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="length">The length of the strings.</param>
        /// <returns>A string array of random values.</returns>
        string[] GetStrings(int count, int length);
        /// <summary>
        /// Gets a random string array.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="length">The length of the strings.</param>
        /// <param name="characters">A array containing all the characters allowed in the random strings. Maximum amount is 80 characters.</param>
        /// <returns>A string array of random values.</returns>
        Task<string[]> GetStringsAsync(int count, int length, char[] characters);
        /// <summary>
        /// Gets a random string array.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="length">The length of the strings.</param>
        /// <param name="characters">A array containing all the characters allowed in the random strings. Maximum amount is 80 characters.</param>
        /// <returns>A string array of random values.</returns>
        string[] GetStrings(int count, int length, char[] characters);
        /// <summary>
        /// Gets a random string array.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="length">The length of the strings.</param>
        /// <param name="characters">A string containing all the characters allowed in the random strings. Maximum amount is 80 characters.</param>
        /// <returns>A string array of random values.</returns>
        Task<string[]> GetStringsAsync(int count, int length, string characters);
        /// <summary>
        /// Gets a random string array.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="length">The length of the strings.</param>
        /// <param name="characters">A string containing all the characters allowed in the random strings. Maximum amount is 80 characters.</param>
        /// <returns>A string array of random values.</returns>
        string[] GetStrings(int count, int length, string characters);

        /// <summary>
        /// Get a random Guid.
        /// </summary>
        /// <returns>A random Guid value.</returns>
        Task<Guid> GetGuidAsync();
        /// <summary>
        /// Get a random Guid.
        /// </summary>
        /// <returns>A random Guid value.</returns>
        Guid GetGuid();
        /// <summary>
        /// Get a Guid array.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <returns>A Guid array of random values.</returns>
        Task<Guid[]> GetGuidsAsync(int count);        
        /// <summary>
        /// Get a Guid array.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <returns>A Guid array of random values.</returns>
        Guid[] GetGuids(int count);
    }
}
