using System;
using LocationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocationService.Controllers
{
    [Route("locations/{memberId}")]
    public class LocationRecordController : Controller
    {
        private ILocationRecordRepository locationRepository;

        public LocationRecordController(ILocationRecordRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        [HttpPost]
        public IActionResult AddLocation(Guid memberID, [FromBody]LocationRecord locationRecord)
        {
            locationRepository.Add(locationRecord);
            return this.Created($"/locations/{memberID}/{locationRecord.ID}", locationRecord);
        }

        [HttpGet]
        public IActionResult GetLocationsForMember(Guid memberID)
        {
            return this.Ok(locationRepository.AllForMember(memberID));
        }

        [HttpGet("latest")]
        public IActionResult GetLatestForMember(Guid memberID)
        {
            return this.Ok(locationRepository.GetLatestForMember(memberID));
        }
    }
}