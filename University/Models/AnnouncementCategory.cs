using System;
using System.Collections.Generic;

namespace University.Models;

public partial class AnnouncementCategory
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
}
