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
            bool liveEventTurbineBelongsToRule = Rule.TurbineIds.Contains(liveEvent.TurbineId);
            bool liveEventHasRequiredEvents = liveEvent.EventIds.Intersect(Rule.RequiredEvents).Any();
            bool liveEventDoesNotHaveForbiddenEvents = !liveEvent.EventIds.Intersect(Rule.ForbidenEvents).Any();

            if (liveEventTurbineBelongsToRule && liveEventHasRequiredEvents && liveEventDoesNotHaveForbiddenEvents)
            {
                if (_liveEventsThatFitIntoRule == null)
                {
                    _liveEventsThatFitIntoRule = new List<LiveEvent>();
                }

                _liveEventsThatFitIntoRule.Add(liveEvent);
            }

            return _liveEventsThatFitIntoRule;

        }

    }
}
