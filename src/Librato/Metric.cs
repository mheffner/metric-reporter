﻿namespace Librato
{
	public abstract class Metric
	{
		private readonly MetricType _metricType;
		private readonly string _name;
		private readonly double _value;

		public Metric(MetricType metricType, string name, double value)
		{
			_metricType = metricType;
			_name = name;
			_value = value;
		}

		public virtual double Value
		{
			get
			{
				return _value;
			}
		}
	}
}