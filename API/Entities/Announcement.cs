using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string MainText { get; set; }
        public int DormitoryNumber { get; set; }
        public int DormitoryRoomNumber { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}