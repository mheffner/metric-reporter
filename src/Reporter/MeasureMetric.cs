﻿namespace AppHarbor.Metrics.Reporter
{
	public class MeasureMetric : Metric
	{
		public MeasureMetric(string name, double value)
			: base(MetricType.Gauge, name, value)
		{
		}
	}
}
