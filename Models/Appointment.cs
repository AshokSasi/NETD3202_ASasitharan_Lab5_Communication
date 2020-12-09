/*
 * Name: Ashok Sasitharan
 * ID:100745484
 * Date: December 1 2020
 * Project: NETD3202 Lab5
 * File: Appointment.cs
 * Purpose: This file contains the appointments class and holds the variables and data validation for appointments
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETD3202_ASasitharan_Lab5_Comm2.Models
{
    public class Appointment
    {
        public int appointmentId { get; set; }
        public string appointmentType { get; set; }
        public DateTime appointmentDate { get; set; }

        public string doctorfname { get; set; }
        public string patientfname { get;  }
        public string patientlname { get;  }
        public string doctorlname { get; set; }
        public string fullname { get { return  "(ID: "+this.doctorId+ ") " + this.doctorfname + " " + this.doctorlname; } }
        public string patientfullname { get { return "(ID: " + this.patientId + ") " + this.patientfname + " " + this.patientlname; } }
        public int doctorId { get; set; }
    
        public virtual Doctor Doctor { get; set; }
       
        
        public int patientId { get; set; }
        public virtual Patient Patient { get; set; }


        public bool Validate(string appointmentType)
        {
         
            if (string.IsNullOrEmpty(appointmentType))
            {

                return false;
            }
            else
            {
                return true;
            }


        }

    }
}
