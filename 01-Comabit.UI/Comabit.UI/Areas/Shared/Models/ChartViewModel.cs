using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.UI.Areas.Shared.Models
{
    public class ChartViewModel
    {
        public string[] Labels { get; set; }

        public ICollection<int[]> Data { get; set; }

        public int yAxisStepSize { get; set; }
    }
}
