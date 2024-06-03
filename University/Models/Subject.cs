using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public virtual ICollection<TeachingSchedule> TeachingSchedules { get; set; } = new List<TeachingSchedule>();
}
