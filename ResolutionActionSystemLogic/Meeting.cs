using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ResolutionActionSystemLogic
{
    [Table("Meeting")]
    public class Meeting
    {
        [Key]
        public int MeetingId { get; set; }

        public virtual MeetingType MeetingType { get; set; }

        [Required]
        public int MeetingNumber { get; set; }
        
        public virtual Meeting PreviousMeeting { get; set; }

        [Required]
        public DateTime MeetingDate { get; set; }

        [NotMapped]
        public string MeetingCode 
        {
            get 
            { 
                return MeetingType == null ? "" : MeetingType.MeetingTypeName + MeetingNumber;
            }
        }

        public virtual ICollection<MeetingItemStatus> MeetingItemStatuses { get; set; }

        
    }
}
