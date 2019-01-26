using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamService.Models;
using TeamService.Persistence;

namespace TeamService.Controllers
{
    [Route("/teams/{teamId}/[controller]")]
    public class MembersController : Controller
    {
        ITeamRepository repository;

        public MembersController(ITeamRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetMembers(Guid teamId, Guid memberId)
        {
            Team team = repository.Get(teamId);
            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(team.Members);
            }
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Member))]
        [ProducesResponseType(404, Type = typeof(Member))]
        [ProducesResponseType(404, Type = typeof(Team))]
        [Route("/teams/{teamId}/[controller]/{memberId}")]
        public IActionResult GetMember(Guid teamId, Guid memberId)
        {
            Team team = repository.Get(teamId);
            if (team == null)
            {
                return this.NotFound();
            }

            var member = team.Members.Where(m => m.ID == memberId).FirstOrDefault();
            if (member == null)
            {
                return this.NotFound();
            }

            return this.Ok(member);
        }

        [HttpPut]
        [Route("/teams/{teamId}/[controller]/{memberId}")]
        public virtual IActionResult UpdateMember([FromBody]Member updatedMember, Guid teamId, Guid memberId)
        {
            Team team = repository.Get(teamId);
            if (team == null)
            {
                return this.NotFound();
            }

            var member = team.Members.Where(m => m.ID == memberId).FirstOrDefault();
            if (member == null)
            {
                return this.NotFound();
            }

            team.Members.Remove(member);
            team.Members.Add(updatedMember);
            return this.Ok();
        }

        [HttpPost]
        public virtual IActionResult Createmember([FromBody]Member newMember, Guid teamId)
        {
            Team team = repository.Get(teamId);
            if (team == null)
            {
                return this.NotFound();
            }

            team.Members.Add(newMember);
            var teamMember = new { TeamID = teamId, MemberID = newMember.ID };
            return this.Created($"/teams/{teamMember.TeamID}/[controller]/{teamMember.MemberID}", teamMember);
        }

        [HttpGet]
        [Route("/members/{memberId}/team")]
        public virtual IActionResult GetTeamForMember(Guid memberId)
        {
            var teamID = GetTeamIdForMember(memberId);
            if (teamID == Guid.Empty)
            {
                return this.NotFound();
            }

            return this.Ok(new { TeamID = teamID });

        }

        private Guid GetTeamIdForMember(Guid memberId)
        {
            foreach (var team in repository.List())
            {
                var member = team.Members.Where(m => m.ID == memberId).FirstOrDefault();
                if (member != null)
                {
                    return team.ID;
                }
            }
            return Guid.Empty;
        }
    }
}