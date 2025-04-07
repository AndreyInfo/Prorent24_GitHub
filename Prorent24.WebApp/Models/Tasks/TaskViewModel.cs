using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.CrewMember;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prorent24.WebApp.Models.Tasks
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }
        public string CompletedBy { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime DeadLine { get; set; }
        public bool IsPublic { get; set; }
        public List<CrewMemberShortViewModel> CrewMembers { get; set; }
        public List<CrewMemberShortViewModel> Executors { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ModuleTypeEnum BelongsTo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ExpiredTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CompletedIn { get; set; }

        /// <summary>
        /// ReferenceId by EntityEnum
        /// </summary>
        public string ReferenceId { get; set; }


        public string DeadLineGroupName { get; set; }
        public string ExecutorView { get; set; }

        //public List<TagViewModel> Tags { get; set; }
        //public List<NoteViewModel> Notes { get; set; }
    }
}
