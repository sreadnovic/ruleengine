using RuleEngine.Common;
using RuleEngine.Models;
using System.Collections.Generic;

namespace RuleEngine
{
    class RuleService
    {
        private List<RuleLiveEventsTracker> _ruleTrackers;
        private IEnumerable<Rule> _allRules;

        public RuleService(IEnumerable<Rule> allRules)
        {
            _allRules = allRules;
            InitializeRuleTrackers();
        }

        public void CheckAllRules(LiveEvent liveEvent)
        {
            foreach (RuleLiveEventsTracker ruleTracker in _ruleTrackers)
            {
                ruleTracker.AddLiveEventToRule(liveEvent);;

                RuleChecker ruleChecker = new RuleChecker(ruleTracker.Rule, ruleTracker.GetLiveEventsThatFitIntoRule());

                if (ruleChecker.RuleIsSatisfied())
                {
                    RuleNotifier notifier = new RuleNotifier(ruleTracker.Rule.Diagnosis);
                    notifier.Notify();
                }
            }
        }

        private void InitializeRuleTrackers()
        {
            if (_ruleTrackers != null)
            {
                return;
            }

            _ruleTrackers = new List<RuleLiveEventsTracker>();
            
            foreach (Rule rule in _allRules)
            {
                _ruleTrackers.Add(new RuleLiveEventsTracker(rule));
            }
        }
    }
}
