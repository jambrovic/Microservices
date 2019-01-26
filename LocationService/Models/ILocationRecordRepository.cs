using System;
using System.Collections.Generic;

namespace LocationService.Models
{
    public interface ILocationRecordRepository
    {
         LocationRecord Add(LocationRecord locationRecord);
         LocationRecord Update(LocationRecord locationRecord);
         LocationRecord Get(Guid memberID, Guid recordID);
        LocationRecord Delete(Guid memberID, Guid recordID);
        LocationRecord GetLatestForMember(Guid memberID);
        ICollection<LocationRecord> AllForMember(Guid memberID);
    }
}