using Amazon.CloudWatch.EMF.Logger;
using Amazon.CloudWatch.EMF.Model;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MyLambdaFunction;

public class Function
{
    public void FunctionHandler(string input, ILambdaContext context)
    {
        try
        {
            SendMetrics("Service", "aggregator1", "ProcessingLatency1", 100, Unit.MILLISECONDS);
            SendMetrics("Service", "aggregator1", "ProcessingLatency2", 90.55, Unit.MILLISECONDS);
            SendMetrics("Service", "aggregator1", "Memory.HeapUsed", 1600424.0, Unit.BYTES);
            SendMetrics("Service", "aggregator2", "Memory.HeapUsage", 0.76, Unit.PERCENT);
            SendMetrics("Service", "aggregator2", "Memory.NonHeapUsed", 1600424.0, Unit.BYTES);
            SendMetrics("SQLQuery", "table1_Column", "1", 3, Unit.COUNT);

            context.Logger.LogLine($"Metrics sent successfully.");
        }
        catch (Exception ex)
        {
            context.Logger.LogLine($"Error sending metrics: {ex.Message}");
            throw;
        }
    }

    private void SendMetrics(string dimensionName, string metricsName, string metricsKey, double value, Unit unit)
    {
        using (var logger = new MetricsLogger())
        {
            // 名前空間を設定
            logger.SetNamespace("Canary");
            var dimensionSet = new DimensionSet();

            // ディメンション（ラベル）を設定
            dimensionSet.AddDimension(dimensionName, metricsName);
            logger.SetDimensions(dimensionSet);

            // メトリクスを記録
            logger.PutMetric(metricsKey, value, unit, StorageResolution.STANDARD);

            // メトリクスを送信
            logger.Flush();
        }
    }
}
