using System;
using System.Collections.Generic;

namespace University.Models;

public partial class WeeklySchedule
{
    public int ScheduleId { get; set; }

    public string? DayOfWeek { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? Time { get; set; }

    public string? Content { get; set; }

    public string? Participants { get; set; }

    public string? Location { get; set; }

    public string? Host { get; set; }
}
