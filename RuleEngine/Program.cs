using RuleEngine.Models;
using System;
using System.Collections.Generic;
using RuleEngine.Common;
using System.Linq;
using RuleEngine;

IEnumerable<Turbine> turbines = new List<Turbine> 
{ 
    new Turbine { Id = "bp1", Name = "Backa Palanka 1" },
    new Turbine { Id = "bp2", Name = "Backa Palank 2" },
    new Turbine { Id = "ns1", Name = "Novi Sad 1" },
    new Turbine { Id = "ns2", Name = "Novi Sad 2" },
    new Turbine { Id = "bg1", Name = "Beograd 1"}
};

IEnumerable<Event> events = new List<Event>
{
    new Event { Id = "started", Description = "Started roating" },
    new Event { Id = "stopped", Description = "Stopped rotating" },
    new Event { Id = "turned180", Description = "Turned for 180 degrees" },
    new Event { Id = "maintained", Description = "Switched off for maintenance" }
};

IEnumerable<Rule> rules = new List<Rule>
{
    //new Rule 
    //{ 
    //    TurbineIds = new List<string>{ "bp1", "bp2" }, 
    //    TurbineAggregation = TurbineAggregation.All,
    //    ForbidenEvents = new List<string> { "stopped", "maintained" },
    //    RequiredEvents = new List<string> { "started", "turned180" },
    //    Diagnosis = "All BP turbines are on the move!"
    //},
    new Rule
    {
        TurbineIds = new List<string>{ "ns1", "ns2" },
        TurbineAggregation = TurbineAggregation.All,
        ForbidenEvents = new List<string> { "started", "turned180", "maintained" },
        RequiredEvents = new List<string> { "stopped" },
        Diagnosis = "All NS turbines stopped!"
    },
    //new Rule
    //{
    //    TurbineIds = new List<string>{ "bp1", "bp2", "ns1", "ns2", "bg1" },
    //    TurbineAggregation = TurbineAggregation.Any,
    //    ForbidenEvents = new List<string> { "stopped", "maintained" },
    //    RequiredEvents = new List<string> { "started", "turned180" },
    //    Diagnosis = "Something moved on some turbine!"
    //},
    //new Rule
    //{
    //    TurbineIds = new List<string>{ "bp1", "bp2", "ns1", "ns2", "bg1" },
    //    TurbineAggregation = TurbineAggregation.Single,
    //    ForbidenEvents = new List<string> { "started", "stopped", "turned180" },
    //    RequiredEvents = new List<string> { "maintained" },
    //    Diagnosis = "Turbine is being maintained!"
    //}
};

List<RuleLiveEventsTracker> ruleTrackers = new List<RuleLiveEventsTracker>();

foreach (Rule rule in rules)
{
    ruleTrackers.Add(new RuleLiveEventsTracker(rule));
}

void CheckAllRules(LiveEvent liveEvent)
{
    foreach (RuleLiveEventsTracker ruleTracker in ruleTrackers)
    {
        IEnumerable<LiveEvent> liveEventsThatFitIntoRule = ruleTracker.GetLiveEventsThatFitIntoRule(liveEvent);

        RuleChecker ruleChecker = new RuleChecker(ruleTracker.Rule, liveEventsThatFitIntoRule);

        if (ruleChecker.RuleIsSatisfied())
        {
            RuleNotifier notifier = new RuleNotifier(ruleTracker.Rule.Diagnosis);
            notifier.Notify();
        }
    }
}

CheckAllRules(new LiveEvent { EventIds = new List<string> { "stopped" }, TurbineId = "ns1", Timestamp = DateTime.Now });
CheckAllRules(new LiveEvent { EventIds = new List<string> { "stopped" }, TurbineId = "ns2", Timestamp = DateTime.Now });

Console.ReadLine();