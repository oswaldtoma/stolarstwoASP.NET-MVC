using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stolarstwo.Models
{
    public enum MirrorType
    {
        RUSTIC,
        STANDARD_SMOOTH,
    }

    public enum FrameWidth
    {
        FW_6CM,
        FW_8CM,
    }

    public enum ColorChoice
    {
        TRANSPARENT,
        WHITE,
        OILWAX,
        OAK,
        BRIGHT_OAK,
        TEAK,
        PALISANDER
    }

    public class FormModel
    {
        private readonly decimal rusticCostPerMeterSquared = 700;
        private readonly decimal standardSmoothCostPerMeterSquared = 580;
        public MirrorType MirrorType { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public FrameWidth FrameWidth { get; set; }
        public ColorChoice ColorChoice { get; set; }

        public decimal GetCalculatedPricePLN()
        {
            decimal costPerMeterSquared = 0;

            switch(MirrorType)
            {
                case MirrorType.RUSTIC:
                    costPerMeterSquared = rusticCostPerMeterSquared;
                break;

                case MirrorType.STANDARD_SMOOTH:
                    costPerMeterSquared = standardSmoothCostPerMeterSquared;
                break;
            }

            return (Height * Width / 10000) * costPerMeterSquared;
        }
    }
}
