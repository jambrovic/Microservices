using System;
using System.Collections.Generic;
using System.Linq;
using TeamService.Models;

namespace TeamService.Persistence
{
    public class MemoryTeamRepository : ITeamRepository
    {
        protected static ICollection<Team> teams;

        public MemoryTeamRepository()
        {
            if (teams == null)
            {
                teams = new List<Team>();
            }
        }

        public MemoryTeamRepository(ICollection<Team> teams)
        {
            MemoryTeamRepository.teams = teams;
        }
        public Team Add(Team team)
        {
            teams.Add(team);
            return team;
        }

        public Team Delete(Guid id)
        {
            var teamForDelete = teams.Where(t => t.ID == id).FirstOrDefault();
            teams.Remove(teamForDelete);
            return teamForDelete;
        }

        public Team Get(Guid id)
        {
            return teams.Where(t => t.ID == id).FirstOrDefault();
        }

        public IEnumerable<Team> List()
        {
            return teams;
        }

        public Team Update(Team team)
        {
            if(this.Delete(team.ID)!=null)
            {
                teams.Add(team);
                return team;
            }
            else
            {
                return null;
            }

        }
    }
}