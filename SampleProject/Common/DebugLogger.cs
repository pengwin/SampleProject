using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Diagnostics;

namespace SampleProject.Common
{
    /// <summary>
    /// Simple logger implementation.
    /// Prints messages to the debug console.
    /// </summary>
    public class DebugLogger : ILogger
    {
        #region Implementation of ILogger

        public void Info(string message)
        {
            System.Diagnostics.Debug.Print("Info: {0}", message);
        }

        public void Debug(string message)
        {
            System.Diagnostics.Debug.Print("Debug: {0}", message);
        }

        public void Error(string message)
        {
            System.Diagnostics.Debug.Print("Error: {0}",message);
        }

        #endregion
    }
}