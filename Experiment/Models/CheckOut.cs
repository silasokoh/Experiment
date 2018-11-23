using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace Experiment.Models
{
    public class CheckOut
    {
        private DateTime checkOutDate;
        private DateTime checkOutTime;
        private string name;

        [Key]
        public int Id { get; set; }
       
        public string Name  { get { return name; } set { name = value; } }

        [HiddenInput]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm tt}", ApplyFormatInEditMode = true)]
        [Editable(false, AllowInitialValue = true)]
        //[DataMember(EmitDefaultValue = true)]
        [Display(Name = "Date")]
        public DateTime CheckOutDate
        {
            get
            {
                return checkOutDate;
            }
            set
            {
                checkOutDate = value;
            }
        }
        [HiddenInput]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Editable(false, AllowInitialValue = true)]       
        [Display(Name = "Date")]
        public DateTime Date
        {
            get
            {
                return checkOutDate;
            }
            set
            {
                checkOutDate = value;
            }
        }
        [HiddenInput]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:h:mm tt}", ApplyFormatInEditMode = true)]
        [Editable(false, AllowInitialValue = true)]
       
        [Display(Name = "Time")]
        public DateTime Time
        {
            get
            {
                return checkOutTime;
            }
            set
            {
                checkOutTime = value;
            }
        }

        public CheckOut()
        {
            checkOutTime = DateTime.Now.ToLocalTime();

            checkOutDate = DateTime.Now;

        }
        public virtual Schedule Schedule { get; set; }
    }  
}