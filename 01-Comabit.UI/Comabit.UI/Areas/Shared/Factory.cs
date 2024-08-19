using Comabit.BL.Shared.DTO;
using Comabit.UI.Areas.Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.UI.Areas.Shared
{
    public static class Factory
    {
        internal static double RetrieveTrend(DaysCountResultItem item)
        {
            double perday1 = item.CountValue1 / item.Days1;
            double perday2 = item.CountValue2 / item.Days2;
            double percent = 0;
            if (perday2 != 0)
            {
                percent = 100 * perday1 / perday2 - 100;
            }
            var result = Math.Round(percent, 2);
            return result;
        }

        internal static ChartViewModel MapDateCountResultToChartViewModel(ICollection<DateCountResultItem> resultItems)
        {
            var labels = new List<string>();
            var data = new List<int>();
            var maxvalue = 0;
            foreach (var item in resultItems)
            {
                labels.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Date.Month));
                data.Add(item.CountValue);
                if (item.CountValue > maxvalue)
                {
                    maxvalue = item.CountValue;
                }
            }
            var dataList = new List<int[]>();
            dataList.Add(data.ToArray());
            var result = new ChartViewModel()
            {
                Labels = labels.ToArray(),
                Data = dataList,
                yAxisStepSize = CalculateStepSize(maxvalue)
            };
            return result;
        }

        internal static ChartViewModel MapDateCount2ListsResultToChartViewModel(ICollection<DateCountResultItem> result1Items, ICollection<DateCountResultItem> result2Items)
        {
            var labels = new List<string>();
            var data1 = new List<int>();
            var data2 = new List<int>();
            var maxvalue = 0;
            foreach (var item in result1Items)
            {
                labels.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Date.Month));
                data1.Add(item.CountValue);
                var value2 = result2Items.Where(o => o.Date.Equals(item.Date)).Select(o => o.CountValue).FirstOrDefault();
                data2.Add(value2);
                if (item.CountValue > maxvalue)
                {
                    maxvalue = item.CountValue;
                }
                if (value2 > maxvalue)
                {
                    maxvalue = value2;
                }
            }
            var dataList = new List<int[]>();
            dataList.Add(data1.ToArray());
            dataList.Add(data2.ToArray());
            var result = new ChartViewModel()
            {
                Labels = labels.ToArray(),
                Data = dataList,
                yAxisStepSize = CalculateStepSize(maxvalue)
            };
            return result;
        }

        private static int CalculateStepSize(int maxvalue)
        {
            var result = 2;
            if (maxvalue > 10)
            {
                var firstdigit = Convert.ToInt32(maxvalue.ToString()[0].ToString());
                result = firstdigit * 2;
                while (result * 10 < maxvalue)
                {
                    result *= 10;
                }
            }

            return result;
        }
    }
}
