using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                return MeetingType == null ? "" : MeetingType.MeetingTypeAbbreviation + MeetingNumber;
            }
        }

        [NotMapped]
        public string MeetingCodeAndType
        {
            get
            {
                return MeetingType == null ? "" : String.Format("{0} - {1}",MeetingType.MeetingTypeAbbreviation + MeetingNumber,MeetingType.MeetingTypeName);
            }
        }

        public virtual ICollection<MeetingItemStatus> MeetingItemStatuses { get; set; }

        
    }
}
