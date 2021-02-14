using NUnit.Framework;
using RuleEngine.Common;
using RuleEngine.Models;
using System;
using System.Collections.Generic;

namespace RuleEngine.Test
{
    class RuleLiveEventsTrackerTest
    {
        [Test]
        public void AddLiveEventToRule_AddValidLiveEventToRule_LiveEventsThatFitIntoRuleIncrease()
        {
            // Arrange
            Rule rule = new Rule
            {
                TurbineIds = new List<string> { "ns1", "ns2" },
                TurbineAggregation = TurbineAggregation.All,
                ForbidenEvents = new List<string> { "started", "turned180", "maintained" },
                RequiredEvents = new List<string> { "stopped" },
                Diagnosis = "All NS turbines stopped!"
            };

            LiveEvent liveEvent = new LiveEvent { EventIds = new List<string> { "stopped" }, TurbineId = "ns1", Timestamp = DateTime.Now };

            RuleLiveEventsTracker ruleLiveEventsTracker = new RuleLiveEventsTracker(rule);

            // Act
            ruleLiveEventsTracker.AddLiveEventToRule(liveEvent);

            // Assert
            Assert.AreEqual(ruleLiveEventsTracker.GetLiveEventsThatFitIntoRule().Count, 1);
        }

        [Test]
        public void AddLiveEventToRule_AddInValidLiveEventListToRule_LiveEventsThatFitIntoRuleIsZero()
        {
            // Arrange
            Rule rule = new Rule
            {
                TurbineIds = new List<string> { "ns1", "ns2" },
                TurbineAggregation = TurbineAggregation.All,
                ForbidenEvents = new List<string> { "started", "turned180", "maintained" },
                RequiredEvents = new List<string> { "stopped" },
                Diagnosis = "All NS turbines stopped!"
            };

            LiveEvent liveEvent = new LiveEvent { EventIds = new List<string> { "stopped", "started" }, TurbineId = "ns1", Timestamp = DateTime.Now };

            RuleLiveEventsTracker ruleLiveEventsTracker = new RuleLiveEventsTracker(rule);

            // Act
            ruleLiveEventsTracker.AddLiveEventToRule(liveEvent);

            // Assert
            Assert.AreEqual(ruleLiveEventsTracker.GetLiveEventsThatFitIntoRule().Count, 0);
        }

        [Test]
        public void AddLiveEventToRule_AddInValidTurbineToRule_LiveEventsThatFitIntoRuleIsZero()
        {
            // Arrange
            Rule rule = new Rule
            {
                TurbineIds = new List<string> { "ns1", "ns2" },
                TurbineAggregation = TurbineAggregation.All,
                ForbidenEvents = new List<string> { "started", "turned180", "maintained" },
                RequiredEvents = new List<string> { "stopped" },
                Diagnosis = "All NS turbines stopped!"
            };

            LiveEvent liveEvent = new LiveEvent { EventIds = new List<string> { "stopped" }, TurbineId = "ns3", Timestamp = DateTime.Now };

            RuleLiveEventsTracker ruleLiveEventsTracker = new RuleLiveEventsTracker(rule);

            // Act
            ruleLiveEventsTracker.AddLiveEventToRule(liveEvent);

            // Assert
            Assert.AreEqual(ruleLiveEventsTracker.GetLiveEventsThatFitIntoRule().Count, 0);
        }
    }
}