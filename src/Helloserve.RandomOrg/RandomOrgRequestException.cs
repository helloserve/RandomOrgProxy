/*
Copyright 2017 Henk Roux (helloserve Productions)

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
    public class RandomOrgRequestException : Exception
    {
        public RandomOrgRequestException(string message)
            : base(message) { }

        public RandomOrgRequestException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
