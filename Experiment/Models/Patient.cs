using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace Experiment.Models
{
    public class Patient
    {

        private DateTime dob = DateTime.Now;
        private DateTime creationDate;
        private string id;

        public Patient()
        {
            dob = DateTime.Now;
            creationDate = DateTime.Now;
            Random rnd = new Random();
            id = rnd.Next(1001, 9999).ToString();

        }
        public Patient(string newId)
        {
            id = newId;
        }

        [Key]
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        [StringLength(10, ErrorMessage = "Pin must either be a 4-digit user's id or 10-digit user's phone number.")]
        public string PIN { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth
        {
            get { return dob; }
            set { dob = value; }
        }

        [Required]
        [HiddenInput]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm tt}", ApplyFormatInEditMode = true)]
        [Editable(false, AllowInitialValue = true)]
        [Display(Name = "Creation Date")]         
        public DateTime CreationDate
        {
            get
            {
                return creationDate;
            }
            set
            {
                creationDate = value;
            }

        }
        public override string ToString()
        {
            string result = FirstName + " " + LastName;
            return result;
        }
        public virtual MyUser User { get; set; }   
        public virtual ICollection<Schedule> Schedules { get; set; } 

    }
}