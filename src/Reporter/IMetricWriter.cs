﻿namespace AppHarbor.Metrics.Reporter
{
	public interface IMetricWriter
	{
		void Write(Metric metric, string source);
	}
}
