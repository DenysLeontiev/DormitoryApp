using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class AnnouncementDto
    {
        public int Id { get; set; } // to get Announcement id
        public string Header { get; set; }
        public string MainText { get; set; }
        public int DormitoryNumber { get; set; }
        public int DormitoryRoomNumber { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
    }
}