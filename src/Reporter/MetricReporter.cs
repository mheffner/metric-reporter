﻿using System;

namespace AppHarbor.Metrics.Reporter
{
	public class MetricReporter : IMetricReporter
	{
		private readonly IMetricWriter _metricWriter;
		private readonly string _defaultSource;
		private readonly StopwatchFactory _stopwatchFactory;

		public MetricReporter(IMetricWriter metricWriter)
			: this(metricWriter, null, new StopwatchFactory())
		{
		}

		public MetricReporter(IMetricWriter metricWriter, string defaultSource, StopwatchFactory stopwatchFactory)
		{
			_metricWriter = metricWriter;
			_defaultSource = defaultSource;
			_stopwatchFactory = stopwatchFactory;
		}

		public void Group(string prefix, Action<IMetricReporter> logReporterAction)
		{
			var metricWriter = new PrefixingMetricWriter(prefix, _metricWriter);
			var logReporter = new MetricReporter(metricWriter);

			logReporterAction(logReporter);
		}

		public void Increment(string counterName, double incrementBy = 1, string source = null)
		{
			var metric = new CounterMetric(counterName, incrementBy);
			_metricWriter.Write(metric, source);
		}

		public void Measure(string gaugeName, Action action, string source = null)
		{
			var stopWatch = _stopwatchFactory.Get();

			stopWatch.Start();
			action();
			stopWatch.Stop();

			Measure(gaugeName, stopWatch.ElapsedMilliseconds, source);
		}

		public void Measure(string gaugeName, double value, string source = null)
		{
			var metric = new GaugeMetric(gaugeName, value);
			_metricWriter.Write(metric, source);
		}
	}
}
