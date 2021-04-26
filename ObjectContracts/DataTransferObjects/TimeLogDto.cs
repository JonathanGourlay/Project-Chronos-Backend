using System;

namespace ObjectContracts.DataTransferObjects
{
    public class TimeLogDto
    {
        public int TimeLogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TotalTime { get; set; }
        public string Billable { get; set; }
        public string Archived { get; set; }

        public TimeLogDto(){}

        public TimeLogDto(TimeLogViewDto dto)
        {
            TimeLogId = dto.TimeLogId;
            StartTime = dto.StartTime;
            EndTime = dto.EndTime;
            TotalTime = dto.TotalTime;
            Billable = dto.Billable;
            Archived = dto.Archived;
        }
    }
}