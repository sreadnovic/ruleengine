using RuleEngine.Common;
using System.Collections.Generic;

namespace RuleEngine.Models
{
    class Rule
	{
		public List<string> TurbineIds { get; set; }
		public TurbineAggregation TurbineAggregation { get; set; }
		public List<string> ForbidenEvents { get; set; }
		public List<string> RequiredEvents { get; set; }
		public string Diagnosis { get; set; }
	}
}
