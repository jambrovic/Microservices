using System;
using System.Collections.Generic;
using System.Linq;
using LocationService.Models;

namespace LocationService.Persistence
{
    public class InMemoryLocationRecordRepository : ILocationRecordRepository
    {
        private static Dictionary<Guid, SortedList<long, LocationRecord>> locationRecords;

        public LocationRecord Add(LocationRecord locationRecord)
        {
            var memberRecords = getMemberRecords(locationRecord.MemberID);
            memberRecords.Add(locationRecord.Timestamp, locationRecord);
            return locationRecord;
        }

        private SortedList<long, LocationRecord> getMemberRecords(Guid memberID)
        {
            if (!locationRecords.ContainsKey(memberID))
            {
                locationRecords.Add(memberID, new SortedList<long, LocationRecord>());
            }

            return locationRecords[memberID];
        }

        public ICollection<LocationRecord> AllForMember(Guid memberID)
        {
            var memberRecords = getMemberRecords(memberID);
            return memberRecords.Values.Where(l => l.MemberID == memberID).ToList();
        }

        public LocationRecord Delete(Guid memberID, Guid recordID)
        {
            var memberRecords = getMemberRecords(memberID);
            LocationRecord lr = memberRecords.Values.Where(l => l.ID == recordID).FirstOrDefault();

            if (lr != null)
            {
                memberRecords.Remove(lr.Timestamp);
            }

            return lr;
        }

        public LocationRecord Get(Guid memberID, Guid recordID)
        {
            var memberRecords = getMemberRecords(memberID);
            return memberRecords.Values.Where(l => l.ID == recordID).FirstOrDefault();
        }

        public LocationRecord GetLatestForMember(Guid memberID)
        {
            var memberRecords = getMemberRecords(memberID);
            return memberRecords.Values.LastOrDefault();
        }

        public LocationRecord Update(LocationRecord locationRecord)
        {
            var deleted = Delete(locationRecord.MemberID, locationRecord.ID);
            if(deleted!=null)
            {
                locationRecords[locationRecord.MemberID].Add(locationRecord.Timestamp,locationRecord);
            }
            return deleted;
        }
    }
}