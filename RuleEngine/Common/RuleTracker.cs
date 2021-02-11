using RuleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Common
{
    class RuleTracker
    {

        #region Fields

        private List<LiveEvent> _liveEventsThatFitIntoRule;

        #endregion

        #region Properties

        public Rule Rule { get; set; }
        public DateTime LastTimeRuleIsSatisfied { get; set; }

        #endregion

        public void DetermineIfLiveEventFitsIntoRule(LiveEvent liveEvent)
        {
            bool liveEventTurbineBelongsToRule = Rule.TurbineIds.Contains(liveEvent.TurbineId);
            bool liveEventHasRequiredEvents = liveEvent.EventIds.Intersect(Rule.RequiredEvents).Any();
            bool liveEventDoesNotHaveForbiddenEvents = !liveEvent.EventIds.Intersect(Rule.ForbidenEvents).Any();

            if (liveEventTurbineBelongsToRule && liveEventHasRequiredEvents && liveEventDoesNotHaveForbiddenEvents)
            {
                if (_liveEventsThatFitIntoRule == null)
                {
                    InitializeLiveEventsThatFitIntoRule();
                }

                _liveEventsThatFitIntoRule.Add(liveEvent);
            }
        }

        public void DetermineIfRuleIsSatisfied()
        {
            if (_liveEventsThatFitIntoRule == null)
            {
                return;
            }

            List<string> ruleTurbines = Rule.TurbineIds;
            List<string> liveEventsThatFitIntoRuleTurbines = _liveEventsThatFitIntoRule.Select(x => x.TurbineId).ToList();

            if (Rule.TurbineAggregation == TurbineAggregation.All)
            {
                if (ruleTurbines.All(x => liveEventsThatFitIntoRuleTurbines.Contains(x)))
                {
                    WriteDiagnosis(Rule);
                }
            } 
            else if (Rule.TurbineAggregation == TurbineAggregation.Any) 
            {
                if (ruleTurbines.Any(x => liveEventsThatFitIntoRuleTurbines.Contains(x)))
                {
                    WriteDiagnosis(Rule);
                }
            }
            else
            {
                if (ruleTurbines.Where(x => liveEventsThatFitIntoRuleTurbines.Contains(x))?.Count() == 1)
                {
                    WriteDiagnosis(Rule);
                }
            }
        }

        private void WriteDiagnosis(Rule rule)
        {
            Console.WriteLine(Rule.Diagnosis);
            InitializeLiveEventsThatFitIntoRule();
        }

        private void InitializeLiveEventsThatFitIntoRule()
        {
            _liveEventsThatFitIntoRule = new List<LiveEvent>();
        }

    }
}
