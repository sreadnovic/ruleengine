using RuleEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngine.Common
{
    public class RuleLiveEventsTracker
    {
        private List<LiveEvent> _liveEventsThatFitIntoRule;
        public Rule Rule { get; }

        public RuleLiveEventsTracker(Rule rule)
        {
            Rule = rule;
        }

        public List<LiveEvent> GetLiveEventsThatFitIntoRule()
        {
            return _liveEventsThatFitIntoRule;
        }

        public void AddLiveEventToRule(LiveEvent liveEvent)
        {
            InitializeLiveEventsThatFitIntoRule();

            if (LiveEventTurbineBelongsToRule(liveEvent)
                && LiveEventDoesNotHaveRuleForbiddenEvents(liveEvent))
            {
                _liveEventsThatFitIntoRule.Add(liveEvent);
            }
        }

        private void InitializeLiveEventsThatFitIntoRule()
        {
            if (_liveEventsThatFitIntoRule == null)
            {
                _liveEventsThatFitIntoRule = new List<LiveEvent>();
            }
        }

        private bool LiveEventTurbineBelongsToRule(LiveEvent liveEvent) => Rule.TurbineIds.Contains(liveEvent.TurbineId);
        private bool LiveEventDoesNotHaveRuleForbiddenEvents(LiveEvent liveEvent) => !liveEvent.EventIds.Intersect(Rule.ForbidenEvents).Any();
    }
}
