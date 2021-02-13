using RuleEngine.Models;
using System.Collections.Generic;

namespace RuleEngine.Strategy
{
    class Context
    {
        private IRuleStrategy _strategy;

        public Context(IRuleStrategy strategy)
        {
            _strategy = strategy;
        }

        public bool Execute(Rule rule, List<LiveEvent> liveEvents)
        {
            return _strategy.RuleFulfilled(rule, liveEvents);
        }
    }
}
