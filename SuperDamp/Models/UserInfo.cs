using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuperDamp.Models
{
    public class UserInfo
    {
        [Key]
        public string username { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public string mobilePhone { get; set; }
        public string gener { get; set; }
        public string province { get; set; }
        public string address { get; set; }
        public string comments { get; set; }

    }

    public enum Gender
    {
        Male,
        Femaile,
        BiSexual

    }

    public enum Province
    {
        ON,
        AL,
        QC,
        MT,
        BC
    }
}