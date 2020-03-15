using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collegiate.Models
{
    public class Preference
    {
        public string PreferenceId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public bool Smoking { get; set; }
        public string Music { get; set; }
    }
}