using NUnit.Framework;
using RuleEngine.Common;
using RuleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Test
{
    class RuleCheckerTest
    {
        [Test]
        public void RuleIsSatisfied_AllRule_IsTrue()
        {
            // Arrange
            List<LiveEvent> liveEventsThatFitIntoRule = new List<LiveEvent>
            {
                new LiveEvent { EventIds = new List<string> { "stopped" }, TurbineId = "ns1", Timestamp = DateTime.Now },
                new LiveEvent { EventIds = new List<string> { "stopped" }, TurbineId = "ns2", Timestamp = DateTime.Now.AddSeconds(10) }
            };

            Rule rule = new Rule
            {
                TurbineIds = new List<string> { "ns1", "ns2" },
                TurbineAggregation = TurbineAggregation.All,
                ForbidenEvents = new List<string> { "started" },
                RequiredEvents = new List<string> { "stopped" },
                Diagnosis = "All NS turbines stopped!"
            };

            // Act
            RuleChecker ruleChecker = new RuleChecker(rule, liveEventsThatFitIntoRule);

            // Assert
            Assert.IsTrue(ruleChecker.RuleIsSatisfied());
        }

        [Test]
        public void RuleIsSatisfied_AnyRule_IsTrue()
        {
            // Arrange
            List<LiveEvent> liveEventsThatFitIntoRule = new List<LiveEvent>
            {
                new LiveEvent { EventIds = new List<string> { "started" }, TurbineId = "bp1", Timestamp = DateTime.Now },
                new LiveEvent { EventIds = new List<string> { "started" }, TurbineId = "bg1", Timestamp = DateTime.Now.AddSeconds(10) },
            };

            Rule rule = new Rule
            {
                TurbineIds = new List<string> { "bp1", "bp2", "ns1", "ns2", "bg1" },
                TurbineAggregation = TurbineAggregation.Any,
                ForbidenEvents = new List<string> { "stopped" },
                RequiredEvents = new List<string> { "started" },
                Diagnosis = "Some turbine started!"
            };

            // Act
            RuleChecker ruleChecker = new RuleChecker(rule, liveEventsThatFitIntoRule);

            // Assert
            Assert.IsTrue(ruleChecker.RuleIsSatisfied());
        }

        [Test]
        public void RuleIsSatisfied_SingleRule_IsTrue()
        {
            // Arrange
            List<LiveEvent> liveEventsThatFitIntoRule = new List<LiveEvent>
            {
                new LiveEvent { EventIds = new List<string> { "stopped" }, TurbineId = "ns1", Timestamp = DateTime.Now },
                new LiveEvent { EventIds = new List<string> { "stopped" }, TurbineId = "ns2", Timestamp = DateTime.Now.AddSeconds(10) },
                new LiveEvent { EventIds = new List<string> { "started" }, TurbineId = "bp1", Timestamp = DateTime.Now.AddSeconds(20) },
                new LiveEvent { EventIds = new List<string> { "started" }, TurbineId = "bg1", Timestamp = DateTime.Now.AddSeconds(30) },
                new LiveEvent { EventIds = new List<string> { "maintained"}, TurbineId = "bp2", Timestamp = DateTime.Now.AddSeconds(40) }
            };

            Rule rule = new Rule
            {
                TurbineIds = new List<string> { "bp1", "bp2", "ns1", "ns2", "bg1" },
                TurbineAggregation = TurbineAggregation.Single,
                ForbidenEvents = new List<string> { "turned180", },
                RequiredEvents = new List<string> { "maintained" },
                Diagnosis = "Turbine is being maintained!"
            };

            // Act
            RuleChecker ruleChecker = new RuleChecker(rule, liveEventsThatFitIntoRule);

            // Assert
            Assert.IsTrue(ruleChecker.RuleIsSatisfied());
        }
    }
}
