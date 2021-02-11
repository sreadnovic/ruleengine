using System;
using System.Collections.Generic;

namespace RuleEngine.Models
{
    class LiveEvent
	{
		public string TurbineId { get; set; }
		public List<string> EventIds { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
