using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analise_Acoes_NICHOLAS.Models;
using Analise_Acoes_NICHOLAS.Operations;
using Analise_Acoes_NICHOLAS.YahooFinance;

namespace Analise_Acoes_NICHOLAS.AsyncLoaders
{
    public class AsyncLoadersF
    {
        public static Task<IEnumerable<DataPoint>> GetDatapointsAsync(string symbol, DateTime start, DateTime end)
        {
            return Task.Run(() =>
            {
                List<Double> eod = YahooFinanceF.YahooLoadData(symbol, start, end);
                List<DateTime> dates = YahooFinanceF.YahooLoadDates(symbol, start, end);


                List<DataPoint> dataPointsL = new List<DataPoint> { };
                for (int i = 0; i < eod.Count; i++)
                {
                    dataPointsL.Add(
                        new DataPoint(dates[i].ToString("yyyy-MM-dd"), eod[i]));
                }
                IEnumerable<DataPoint> dataPoints = dataPointsL.AsEnumerable();

                return dataPoints;
            });
        }


        public static Task<IEnumerable<DataPoint>> GetFeedbackAsync(string symbol, DateTime start, DateTime end)
        {
            return Task.Run(() =>
            {
                List<Double> feedback = OperationsF.GetStockFeedback(symbol, start, end);
                List<DateTime> dates = YahooFinanceF.YahooLoadDates(symbol, start, end);


                List<DataPoint> dataPointsL = new List<DataPoint> { };
                for (int i = 0; i < feedback.Count; i++)
                {
                    dataPointsL.Add(
                        new DataPoint(dates[i].ToString("yyyy-MM-dd"), feedback[i]));
                }
                IEnumerable<DataPoint> dataPoints = dataPointsL.AsEnumerable();

                return dataPoints;
            });
        }

        public static Task<IEnumerable<DataPoint>> GetLogFeedbackAsync(string symbol, DateTime start, DateTime end)
        {
            return Task.Run(() =>
            {
                List<Double> feedback = OperationsF.GetLogFeedback(symbol, start, end);
                List<DateTime> dates = YahooFinanceF.YahooLoadDates(symbol, start, end);


                List<DataPoint> dataPointsL = new List<DataPoint> { };
                for (int i = 0; i < feedback.Count; i++)
                {
                    dataPointsL.Add(
                        new DataPoint(dates[i].ToString("yyyy-MM-dd"), feedback[i]));
                }
                IEnumerable<DataPoint> dataPoints = dataPointsL.AsEnumerable();

                return dataPoints;
            });
        }


        public static Task<IEnumerable<DataPoint>> GetHistogramAsync(string symbol, DateTime start, DateTime end)
        {
            return Task.Run(() =>
            {
                List<Double> feedback = OperationsF.GetStockFeedback(symbol, start, end);
                Dictionary<Double, int> HistogramPoints = OperationsF.GenerateFeedbackHistogram(feedback);

                List<DataPoint> dataPointsL = new List<DataPoint> { };
                foreach (var key in HistogramPoints.Keys)
                {
                    dataPointsL.Add(
                        new DataPoint((((key * 100)).ToString() + "%"), HistogramPoints[key]));
                }
                IEnumerable<DataPoint> dataPoints = dataPointsL.AsEnumerable();

                return dataPoints;
            });
        }


        public static Task<IEnumerable<DataPoint>> GetVolumeAsync(string symbol, DateTime start, DateTime end)
        {
            return Task.Run(() =>
            {
                List<Double> Volume = YahooFinanceF.YahooLoadVolume(symbol, start, end);
                List<DateTime> dates = YahooFinanceF.YahooLoadDates(symbol, start, end);


                List<DataPoint> dataPointsL = new List<DataPoint> { };
                for (int i = 0; i < Volume.Count; i++)
                {
                    dataPointsL.Add(
                        new DataPoint(dates[i].ToString("yyyy-MM-dd"), Volume[i]));
                }
                IEnumerable<DataPoint> dataPoints = dataPointsL.AsEnumerable();

                return dataPoints;
            });
        }

    }
}

