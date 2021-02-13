using RuleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngine.Strategy
{
    class SingleRule
        : IRuleStrategy
    {
        public bool RuleFulfilled(Rule rule, List<LiveEvent> liveEventsThatFitIntoRule)
        {
            return rule.TurbineIds.Where(x => liveEventsThatFitIntoRule.Select(x => x.TurbineId).Contains(x))?.Count() == 1;
        }
    }
}
