using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UsersController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<MemberDto>> GetUsers()
        {
            List<MemberDto> members = _mapper.Map<IEnumerable<MemberDto>>(_context.Users.Include(x => x.Announcements)).ToList(); //source - AppUser
            
            return members;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetUserById(int id)
        {
            var member = _mapper.Map<MemberDto>(await _context.Users.Include(a => a.Announcements).FirstOrDefaultAsync(x => x.Id == id));

            if(member == null)
            {
                return NotFound();
            }

            return member;
        }
    }
}