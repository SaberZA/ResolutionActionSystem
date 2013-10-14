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
    }
}
