using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ResolutionActionSystemLogic
{
    [Table("MeetingItem")]
    public class MeetingItem
    {
        [Key]
        public int MeetingItemId { get; set; }

        [Required]
        public string MeetingItemDesc { get; set; }

        public DateTime MeetingItemDueDate { get; set; }

        public virtual Person PersonResponsible { get; set; }

        public virtual ICollection<MeetingItemStatus> MeetingItemStatuses { get; set; }
    }
}
