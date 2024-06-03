using System;
using System.Collections.Generic;

namespace University.Models;

public partial class HolidaySchedule
{
    public int HolidayId { get; set; }

    public string? Title { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}
