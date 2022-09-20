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
namespace Helloserve.RandomOrg
{
    public class RandomOrgOptions
    {
        private string _url = "https://api.random.org/json-rpc/{0}/invoke";
        private string _apiVersion = "4";
        private bool _fallback = true;
        private bool _replacement = true;
        private char[] _allowedCharacters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                                                         'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                                                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                                                         '~', '!', '@', '#', '$', '%', '^', '&', '*', '-', '=' };

        /// <summary>
        /// Your API key for random.org. This value must be set since it cannot be defaulted.
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// The API Version to use. Production API keys require version 2. See Url property.
        /// </summary>
        public string ApiVersion { get { return _apiVersion; } set { _apiVersion = value; } }

        /// <summary>
        /// The Url for the random.org API endpoint. This is defaulted to "https://api.random.org/json-rpc/{ApiVersion}/invoke";
        /// </summary>
        internal string Url { get { return string.Format(_url, ApiVersion); } }
        
        /// <summary>
        /// Gets or sets the behavior of the client in the case of denied, throttled or HTTP error scenarios. Default is true. Setting it to false will not fall back to the Random() class, and you will receive exceptions.
        /// </summary>
        public bool ShouldFallback { get { return _fallback; } set { _fallback = value; } }

        /// <summary>
        /// Gets or sets a global setting for generation with replacement. Setting it to false means that no sequence of generated results will have the same value twice. The Random.Org default is true, and this setting is not applicable to Gaussians.
        /// </summary>
        public bool WithReplacement { get { return _replacement; } set { _replacement = value; } }

        /// <summary>
        /// Gets or sets the default set of characters to use when calling the reduced overload methods to generating strings.
        /// </summary>
        public char[] AllowedStringCharacters
        {
            get { return _allowedCharacters; }
            set { _allowedCharacters = value; }
        }
    }
}
