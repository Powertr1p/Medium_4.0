using System;
using System.IO;

namespace Writer
{
    class Program
    {
        static void Main(string[] args)
        {
            var secureLog = new A(new SecureLog(new ConsoleLog()));
            var consoleLog = new A(new ConsoleLog());
            var secureFileLog = new A(new SecureLog(new FileLog()));
            var consoleAndFileLog = new A(new ConsoleLog(new FileLog()));
            var mostSecuredLogEver = new A(new SecureLog(new ConsoleLog(new FileLog())));
        }
    }

    interface ILogger
    {
        void WriteMessage(string message);
    }

    class A
    {
        private ILogger _log;

        public A(ILogger log)
        {
            _log = log;
        }
    }

    class ConsoleLog : ILogger
    {
        private ILogger _logger;
        public ConsoleLog() { }
        public ConsoleLog(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
            _logger?.WriteMessage(message);
        }
    }

    class FileLog : ILogger
    {
        private ILogger _logger;

        public FileLog() { }
        public FileLog(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteMessage(string message)
        {
            File.WriteAllText("log.txt", message);
            _logger?.WriteMessage(message);
        }
    }

    class SecureLog : ILogger
    {
        private ILogger _logger;

        public SecureLog(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteMessage(string message)
        {
            if (DateTime.Now.Day % 2 == 0)
            {
                _logger.WriteMessage(message);
            }
        }
    }
}