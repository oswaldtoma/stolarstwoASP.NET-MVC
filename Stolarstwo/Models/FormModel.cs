using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Stolarstwo.Models
{
    public class FormModel
    {

        public enum MirrorTypeEnum
        {
            [Display(Name = "Rustic")]
            RUSTIC,
            [Display(Name = "Standard (smooth)")]
            STANDARD_SMOOTH,
        }

        public enum FrameWidthEnum
        {
            [Display(Name = "6 cm")]
            FW_6CM,
            [Display(Name = "8 cm")]
            FW_8CM,
        }

        public enum ColorChoiceEnum
        {
            [Display(Name = "Transparent")]
            TRANSPARENT,
            [Display(Name = "White")]
            WHITE,
            [Display(Name = "Wax oil")]
            WAX_OIL,
            [Display(Name = "Oak")]
            OAK,
            [Display(Name = "Bright oak")]
            BRIGHT_OAK,
            [Display(Name = "Teak")]
            TEAK,
            [Display(Name = "Palisander")]
            PALISANDER
        }

        public enum ShipmentTypeEnum
        {
            [Display(Name = "Prepaid")]
            PREPAID,
            [Display(Name = "On delivery")]
            ON_DELIVERY,
        }

        private readonly decimal rusticCostPerMeterSquared = 700;
        private readonly decimal standardSmoothCostPerMeterSquared = 580;
        public int FormModelId { get; set; }
        [BindProperty]
        [Required]
        public MirrorTypeEnum MirrorType { get; set; }
        [BindProperty]
        [Required]
        [Range(0, 200)]
        public int Height { get; set; }
        [BindProperty]
        [Required]
        [Range(0, 200)]
        public int Width { get; set; }
        [BindProperty]
        [Required]
        public FrameWidthEnum FrameWidth { get; set; }
        [BindProperty]
        [Required]
        public ColorChoiceEnum ColorChoice { get; set; }
        [BindProperty]
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        [BindProperty]
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
        [BindProperty]
        [Required]
        [MinLength(3)]
        public string StreetAndNumber { get; set; }
        [BindProperty]
        [Required]
        [MinLength(3)]
        public string PostalCode { get; set; }
        [BindProperty]
        [Required]
        [MinLength(3)]
        public string City { get; set; }
        [BindProperty]
        [Required]
        [Phone]
        [MinLength(9)]
        [MaxLength(12)]
        public string PhoneNumber { get; set; }
        [BindProperty]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [BindProperty]
        [Required]
        public ShipmentTypeEnum ShipmentType { get; set; }

        public decimal GetCalculatedPricePLN()
        {
            decimal costPerMeterSquared = 0;

            switch (MirrorType)
            {
                case MirrorTypeEnum.RUSTIC:
                    costPerMeterSquared = rusticCostPerMeterSquared;
                    break;

                case MirrorTypeEnum.STANDARD_SMOOTH:
                    costPerMeterSquared = standardSmoothCostPerMeterSquared;
                    break;
            }

            return (Height * Width) * costPerMeterSquared / 1000;
        }
    }
}
