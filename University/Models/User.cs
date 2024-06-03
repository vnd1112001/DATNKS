using System;
using System.Collections.Generic;

namespace University.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<TeachingSchedule> TeachingSchedules { get; set; } = new List<TeachingSchedule>();
}
