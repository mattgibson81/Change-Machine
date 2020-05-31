using System;

namespace Change_Machine
{
    internal class CalculateChangeException : Exception
    {
        internal CalculateChangeException(string message) : base(message)
        {

        }
    }
}