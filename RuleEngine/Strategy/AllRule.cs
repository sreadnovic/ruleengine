using RuleEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngine.Strategy
{
    class AllRule
        : IRuleStrategy
    {
        public bool RuleFulfilled(Rule rule, List<LiveEvent> liveEventsThatFitIntoRule)
        {
            if (liveEventsThatFitIntoRule.Select(x => x.TurbineId).Count() != rule.TurbineIds.Count())
            {
                return false;
            }

            bool res = false;

            foreach (LiveEvent liveEvent in liveEventsThatFitIntoRule)
            {
                if (liveEvent.EventIds.Intersect(rule.RequiredEvents).Any())
                {
                    res = true;
                }
                else
                {
                    res = false;
                    break;
                }
            }

            return res;
        }
    }
}
