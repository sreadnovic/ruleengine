using RuleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngine.Strategy
{
    class AnyRule
        : IRuleStrategy
    {
        public bool RuleFulfilled(Rule rule, List<LiveEvent> liveEventsThatFitIntoRule)
        {
            return rule.TurbineIds.Any(x => liveEventsThatFitIntoRule.Select(x => x.TurbineId).Contains(x));
        }
    }
}
