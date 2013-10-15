using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ResolutionActionSystemLogic
{
    [Table("MeetingType")]
    public class MeetingType
    {
        [Key]
        public int MeetingTypeId { get; set; }
        [Required]
        public string MeetingTypeName { get; set; }

        [NotMapped]
        public string MeetingTypeAbbreviation
        {
            get 
            { 
                var tokenisedMeetingTime = MeetingTypeName.Split(new string[] {" "}, StringSplitOptions.None);
                return tokenisedMeetingTime.Aggregate("", (current, s) => current + s.First().ToString().ToUpper());
            }
        }
    }
}
