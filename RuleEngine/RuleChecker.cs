using RuleEngine.Common;
using RuleEngine.Models;
using System;
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
            if (_liveEventsThatFitIntoRule == null || _rule == null)
            {
                return false;
            }

            List<string> ruleTurbines = _rule.TurbineIds;
            List<string> liveEventsThatFitIntoRuleTurbines = _liveEventsThatFitIntoRule.Select(x => x.TurbineId).ToList();

            if (_rule.TurbineAggregation == TurbineAggregation.All)
            {
                if (ruleTurbines.All(x => liveEventsThatFitIntoRuleTurbines.Contains(x)))
                {
                    ResetEvents();
                    return true;
                }
            }
            else if (_rule.TurbineAggregation == TurbineAggregation.Any)
            {
                if (ruleTurbines.Any(x => liveEventsThatFitIntoRuleTurbines.Contains(x)))
                {
                    ResetEvents();
                    return true;
                }
            }
            else
            {
                if (ruleTurbines.Where(x => liveEventsThatFitIntoRuleTurbines.Contains(x))?.Count() == 1)
                {
                    ResetEvents();
                    return true;
                }
            }

            return false;
        }

        private void ResetEvents()
        {
            _liveEventsThatFitIntoRule = new List<LiveEvent>();
        }
    }
}
