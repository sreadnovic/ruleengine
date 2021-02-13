using RuleEngine.Models;
using System.Collections.Generic;

namespace RuleEngine.Strategy
{
    interface IRuleStrategy
    {
        bool RuleFulfilled(Rule rule, List<LiveEvent> liveEventsThatFitIntoRule);
    }
}
