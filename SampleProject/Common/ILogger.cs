using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleProject.Common
{
    /// <summary>
    /// Logger interface
    /// </summary>
    public interface ILogger
    {
        void Info(string message);
        void Debug(string message);
        void Error(string message);
    }
}