using System;
using Microsoft.AspNetCore.Mvc;
using TeamService.Models;
using TeamService.Persistence;

namespace TeamService.Controllers
{
    [Route("[controller]")]
    public class TeamsController : Controller
    {
        ITeamRepository repository;

        public TeamsController(ITeamRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet]
        public virtual IActionResult GetAllTeams()
        {
            return this.Ok(repository.List());
        }

        [HttpGet("{id}")]
        public IActionResult GetTeam([FromBody]Guid id)
        {
            Team team=repository.Get(id);
            if (team!=null)
            {
                return this.Ok(team);
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        public virtual IActionResult CreateTeam([FromBody]Team newTeam)
        {
            repository.Add(newTeam);
            return this.Created($"/teams/{newTeam.ID}",newTeam);
        }

        [HttpPut("{id}")]
        public virtual IActionResult UpdateTeam([FromBody]Team team, Guid id)
        {
            team.ID=id;
            if(repository.Update(team) != null)
            {
                return this.Ok(team);
            }
            return this.NotFound();
        }

        [HttpDelete("{id}")]
        public virtual IActionResult DeleteTeam(Guid id)
        {
            Team team=repository.Delete(id);
            if(team!=null)
            {
                return this.Ok(team);
            }

            return this.NotFound();
        }
    }
}