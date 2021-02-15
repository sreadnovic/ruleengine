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
            if (liveEventsThatFitIntoRule.Select(x => x.TurbineId).Count() != rule.TurbineIds.Count)
            {
                return false;
            }

            int count = 0;

            foreach (LiveEvent liveEvent in liveEventsThatFitIntoRule)
            {
                if (liveEvent.EventIds.Intersect(rule.RequiredEvents).Any())
                {
                    count++;
                }
            }

            return count == 1;
        }
    }
}
