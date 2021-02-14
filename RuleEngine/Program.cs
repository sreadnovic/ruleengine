using RuleEngine;
using RuleEngine.Common;
using RuleEngine.Models;
using System;
using System.Collections.Generic;

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
    new Rule
    {
        TurbineIds = new List<string>{ "ns1", "ns2" },
        TurbineAggregation = TurbineAggregation.All,
        ForbidenEvents = new List<string> { "started", "turned180", "maintained" },
        RequiredEvents = new List<string> { "stopped" },
        Diagnosis = "All NS turbines stopped!"
    },
    new Rule
    {
        TurbineIds = new List<string>{ "bp1", "bp2", "ns1", "ns2", "bg1" },
        TurbineAggregation = TurbineAggregation.Any,
        ForbidenEvents = new List<string> { "stopped" },
        RequiredEvents = new List<string> { "started" },
        Diagnosis = "Some turbine started!"
    },
    new Rule
    {
        TurbineIds = new List<string>{ "bp1", "bp2", "ns1", "ns2", "bg1" },
        TurbineAggregation = TurbineAggregation.Single,
        ForbidenEvents = new List<string> { "turned180", },
        RequiredEvents = new List<string> { "maintained" },
        Diagnosis = "Turbine is being maintained!"
    }
};

List<LiveEvent> liveEvents = new List<LiveEvent> 
{ 
    new LiveEvent { EventIds = new List<string> { "stopped" }, TurbineId = "ns1", Timestamp = DateTime.Now },
    new LiveEvent { EventIds = new List<string> { "stopped" }, TurbineId = "ns2", Timestamp = DateTime.Now.AddSeconds(10) },
    new LiveEvent { EventIds = new List<string> { "started" }, TurbineId = "bp1", Timestamp = DateTime.Now.AddSeconds(20) },
    new LiveEvent { EventIds = new List<string> { "started" }, TurbineId = "bg1", Timestamp = DateTime.Now.AddSeconds(30) },
    new LiveEvent { EventIds = new List<string> { "maintained"}, TurbineId = "bp2", Timestamp = DateTime.Now.AddSeconds(40) }
};

RuleService ruleService = new RuleService(rules);

foreach (LiveEvent liveEvent in liveEvents)
{
    ruleService.CheckAllRules(liveEvent);
}

Console.ReadLine();