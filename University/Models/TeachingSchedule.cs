using System;
using System.Collections.Generic;

namespace University.Models;

public partial class TeachingSchedule
{
    public int TeachingId { get; set; }

    public int? TeacherId { get; set; }

    public int? SubjectId { get; set; }

    public int? ClassId { get; set; }

    public DateTime? TeachingDate { get; set; }

    public string? DayOfWeek { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual User? Teacher { get; set; }
}
