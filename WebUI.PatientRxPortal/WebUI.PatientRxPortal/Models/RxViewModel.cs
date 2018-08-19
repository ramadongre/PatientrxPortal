using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.PatientRxPortal.Models
{
    public class RxViewModel
    {
        public int RxDataID { get; set; }

        [Required]
        [Display(Name = "Rx Date")]
        public DateTime rxDate { get; set; }

        [Required]
        [Display(Name = "Rx Doctor")]
        public string rxDoctor { get; set; }

        [Required]
        [Display(Name = "Prescription 1")]
        public string Prescription1 { get; set; }
                
        [Display(Name = "Prescription 2")]
        public string Prescription2 { get; set; }

        [Display(Name = "Prescription 3")]
        public string Prescription3 { get; set; }

        [Display(Name = "Prescription 4")]
        public string Prescription4 { get; set; }

        [Display(Name = "Prescription 5")]
        public string Prescription5 { get; set; }
    }
}