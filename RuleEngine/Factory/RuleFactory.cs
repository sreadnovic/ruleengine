using RuleEngine.Common;
using RuleEngine.Models;
using RuleEngine.Strategy;

namespace RuleEngine.Factory
{
    class RuleFactory
    {
        public IRuleStrategy GetRuleStrategy(Rule rule)
        {
            if (rule.TurbineAggregation == TurbineAggregation.All)
            {
                return new AllRule();
            } 
            else if (rule.TurbineAggregation == TurbineAggregation.Any)
            {
                return new AnyRule();
            }
            else
            {
                return new SingleRule();
            }
        }
    }
}
