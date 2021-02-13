using RuleEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngine.Common
{
    class RuleLiveEventsTracker
    {
        private List<LiveEvent> _liveEventsThatFitIntoRule;
        public Rule Rule { get; }

        public RuleLiveEventsTracker(Rule rule)
        {
            Rule = rule;
        }

        public List<LiveEvent> GetLiveEventsThatFitIntoRule(LiveEvent liveEvent)
        {
            if (liveEvent == null)
            {
                return null;
            }

            InitializeLiveEventsThatFitIntoRule();

            if (LiveEventTurbineBelongsToRule(liveEvent) 
                && LiveEventHasRuleRequiredEvents(liveEvent)
                && LiveEventDoesNotHaveRuleForbiddenEvents(liveEvent))
            {
                _liveEventsThatFitIntoRule.Add(liveEvent);
            }

            return _liveEventsThatFitIntoRule;
        }

        private void InitializeLiveEventsThatFitIntoRule()
        {
            if (_liveEventsThatFitIntoRule == null)
            {
                _liveEventsThatFitIntoRule = new List<LiveEvent>();
            }
        }

        private bool LiveEventTurbineBelongsToRule(LiveEvent liveEvent) => Rule.TurbineIds.Contains(liveEvent.TurbineId);
        private bool LiveEventHasRuleRequiredEvents(LiveEvent liveEvent) => liveEvent.EventIds.Intersect(Rule.RequiredEvents).Any();
        private bool LiveEventDoesNotHaveRuleForbiddenEvents(LiveEvent liveEvent) => !liveEvent.EventIds.Intersect(Rule.ForbidenEvents).Any();

    }
}
