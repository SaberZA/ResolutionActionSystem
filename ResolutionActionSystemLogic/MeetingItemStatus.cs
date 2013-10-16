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

        //[ForeignKey("MeetingItemStatusLuId"), Required]
        //public int MeetingItemStatusLuId { get; set; }

        public virtual MeetingItemStatusLu MeetingItemStatusLu { get; set; }

        [Timestamp, Required]
        public DateTime MeetingItemStatusDate { get; set; }

        //[ForeignKey("MeetingId"), Required]
        //public int MeetingId { get; set; }

        public virtual Meeting Meeting { get; set; }

        public virtual MeetingItem MeetingItem { get; set; }

        //[ForeignKey("MeetingItemId"), Required]
        //public int MeetingItemId { get; set; }

        public virtual ICollection<MeetingAction> MeetingActions { get; set; }

        //[System.ComponentModel.DataAnnotations.Association("MeetingItem","MeetingItemId","MeetingItemId")]
        //public MeetingItem MeetingItem { get; set; }

    }
}
