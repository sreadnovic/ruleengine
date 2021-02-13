using RuleEngine.Factory;
using RuleEngine.Models;
using RuleEngine.Strategy;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngine
{
    class RuleChecker
    {
        private Rule _rule;
        private IEnumerable<LiveEvent> _liveEventsThatFitIntoRule;

        public RuleChecker(Rule rule, IEnumerable<LiveEvent> liveEventsThatFitIntoRule)
        {
            _rule = rule;
            _liveEventsThatFitIntoRule = liveEventsThatFitIntoRule;
        }

        public bool RuleIsSatisfied()
        {
            RuleFactory ruleFactory = new RuleFactory();
            IRuleStrategy ruleStrategy = ruleFactory.GetRuleStrategy(_rule);

            Context ruleContext = new Context(ruleStrategy);

            if (ruleContext.Execute(_rule, _liveEventsThatFitIntoRule.ToList()))
            {
                ResetEvents();
                return true;
            }

            return false;
        }

        private void ResetEvents()
        {
            _liveEventsThatFitIntoRule = new List<LiveEvent>();
        }
    }
}
