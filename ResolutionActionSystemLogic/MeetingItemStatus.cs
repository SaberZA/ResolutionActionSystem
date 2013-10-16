using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ResolutionActionSystemLogic
{
    [Table("MeetingItemStatus")]
    public class MeetingItemStatus
    {
        [Key]
        public int MeetingItemStatusId { get; set; }

        public virtual MeetingItemStatusLu MeetingItemStatusLu { get; set; }

        [Timestamp, Required]
        public DateTime MeetingItemStatusDate { get; set; }

        public virtual Meeting Meeting { get; set; }

        public virtual MeetingItem MeetingItem { get; set; }

        public virtual ICollection<MeetingAction> MeetingActions { get; set; }
    }
}
