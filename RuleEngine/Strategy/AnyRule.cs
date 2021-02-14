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
            bool res = false;

            foreach (LiveEvent liveEvent in liveEventsThatFitIntoRule)
            {
                if (liveEvent.EventIds.Intersect(rule.RequiredEvents).Any())
                {
                    res = true;
                    break;
                }
            }

            return res;
        }
    }
}
