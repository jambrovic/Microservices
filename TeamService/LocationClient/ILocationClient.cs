using System;
using System.Threading.Tasks;
using LocationService.Models;

namespace TeamService.LocationClient
{
    public interface ILocationClient
    {
         Task<LocationRecord> GetLatestForMember(Guid memberId);
    }
}