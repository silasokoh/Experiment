using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Experiment.Models
{
    public class CheckIn
    {

        private DateTime checkInDate;
        private DateTime checkInTime;
        private string name;

        [Key]
        public int Id { get; set; }
       
        
        public string Name { get { return name; } set { name = value; }  }

        [HiddenInput]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm tt}", ApplyFormatInEditMode = true)]
        [Editable(false, AllowInitialValue = true)]
         //[DataMember(EmitDefaultValue = true)]
        [Display(Name = "Date")]
        public DateTime CheckInDate
        {
            get
            {
                return checkInDate;
            }
            set
            {
                checkInDate = value;
            }
        }

        [HiddenInput]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Editable(false, AllowInitialValue = true)]
         //[DataMember(EmitDefaultValue = true)]
        [Display(Name = "Date")]
        public DateTime Date
        {
            get
            {
                return checkInDate;
            }
            set
            {
                checkInDate = value;
            }
        }

        [HiddenInput]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:h:mm tt}", ApplyFormatInEditMode = true)]
        [Editable(false, AllowInitialValue = true)]
         //[DataMember(EmitDefaultValue = true)]
        [Display(Name = "Time")]
        public DateTime Time
        {
            get
            {
                return checkInTime;
            }
            set
            {
                checkInTime = value;
            }
        }
        public CheckIn()
        {
            checkInTime = DateTime.Now.ToLocalTime();

            checkInDate = DateTime.Now;

        }
        public virtual Schedule Schedule { get; set; }        
    }
}