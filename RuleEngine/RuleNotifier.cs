using System;

namespace RuleEngine
{
    class RuleNotifier
    {
        private string _message;

        public RuleNotifier(string message)
        {
            _message = message;
        }

        public void Notify()
        {
            Console.WriteLine(_message);
        }
    }
}
