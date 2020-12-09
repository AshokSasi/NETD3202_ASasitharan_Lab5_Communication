/*
 * Name: Ashok Sasitharan
 * ID:100745484
 * Date: December 1 2020
 * Project: NETD3202 Lab5
 * File: Appointment.cs
 * Purpose: This file contains the patients class and holds the variables and data validation for patients
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETD3202_ASasitharan_Lab5_Comm2.Models
{
    public class Patient
    {
        public int patientId { get; set; }
        public string patientfname { get; set; }
        public string patientlname { get; set; }
        public string patientphone { get; set; }
        public string patientfullname { get { return "(ID: " + this.patientId + ") " + this.patientfname + " " + this.patientlname; } }
       
        //Validation function
        public bool Validate(string patientfname, string patientlname, string phone)
        {
            Int64 numPhone;

            if (string.IsNullOrEmpty(patientfname) || string.IsNullOrEmpty(patientlname) || string.IsNullOrEmpty(phone) || patientfname.Length < 2 || patientlname.Length < 2 || phone.Length != 10 || Int64.TryParse(phone, out numPhone) == false)
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
