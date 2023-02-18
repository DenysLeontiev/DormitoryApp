using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AnnouncementsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public AnnouncementsController(DataContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet("my-announcements")]
        public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetMyAnnouncementsAsync()
        {
            // var username = User.FindFirst(ClaimTypes.Name).Value;
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users.Include(x => x.Announcements).FirstOrDefaultAsync(u => u.UserName == username);

            if(user == null)
            {
                return NotFound("Користувача не знайдено (GetMyAnnouncementsAsync)");
            }

            var announcements = user.Announcements.ToList();

            var announcementDto = _mapper.Map<List<AnnouncementDto>>(announcements);

            return announcementDto;
       }

        [HttpPost("add-announcement")]
        public async Task<ActionResult> AddAnnouncement(AnnouncementDto announcementDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            AppUser user = await _context.Users.Include(x => x.Announcements).FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
            {
                return NotFound("Користувача не знайдено");
            }

            Announcement announcement = _mapper.Map<Announcement>(announcementDto);

            user.Announcements.Add(announcement);

            if(await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }        

            return BadRequest("Сталася помилка під час додавання оголошення");
        }
    }
}