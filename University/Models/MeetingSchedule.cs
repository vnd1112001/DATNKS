using System;
using System.Collections.Generic;

namespace University.Models;

public partial class MeetingSchedule
{
    public int MeetingId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? MeetingDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }
}
