using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ResolutionActionSystemLogic
{
    [Table("MeetingItemStatusLu")]
    public class MeetingItemStatusLu
    {
        public MeetingItemStatusLu()
        {
            this.MeetingItemStatusDesc = "CREATED";
        }
        
        [Key]
        public int MeetingItemStatusLuId { get; set; }

        [Required]
        public string MeetingItemStatusDesc { get; set; }
    }
}
