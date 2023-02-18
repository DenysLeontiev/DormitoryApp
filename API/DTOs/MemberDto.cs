using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MemberDto // for AppUser
    {
        public string UserName { get; set; }
        public List<AnnouncementDto> Announcements { get; set; }
    }
}