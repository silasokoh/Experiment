using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace Experiment.Models
{
    public class Schedule
    {
        private DateTime scheduleDate;
       
        private string name;
        private CultureInfo ci;

        public Schedule()
        {
            ci = new CultureInfo("en-US");
            scheduleDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }       

        [StringLength(10, ErrorMessage = "Pin must either be a 4-digit user's id or 10-digit user's phone number.")]
        public string PIN { get; set; }
       
        [Display(Name = "Scheduled")]
        public bool Scheduled { get; set; }

       [HiddenInput]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:m tt}", ApplyFormatInEditMode = true)]             
        [Display(Name = "Schedule Date")]
        public DateTime ScheduleDate
        {
            get
            {
                return scheduleDate;
            }
            set
            {
                scheduleDate = value;
            }
        }       
               
        public virtual Patient Patient { get; set; }
        public virtual ICollection<CheckIn> CheckIns { get; set; }
        public virtual ICollection<CheckOut> CheckOuts { get; set; }
    }
}