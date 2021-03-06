﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ResolutionActionSystemLogic
{
    [Table("MeetingAction")]
    public class MeetingAction
    {
        [Key]
        public int MeetingActionId { get; set; }

        [Required]
        public string ActionDescription { get; set; }

        public virtual MeetingItemStatus MeetingItemStatus { get; set; }

        public virtual Person PersonResponsible { get; set; }

        public override string ToString()
        {
            return ActionDescription;
        }
    }
}
