using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public virtual ICollection<TeachingSchedule> TeachingSchedules { get; set; } = new List<TeachingSchedule>();
}
