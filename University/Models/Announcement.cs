using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Announcement
{
    public int AnnouncementId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public int? AuthorId { get; set; }

    public DateOnly? PublishedDate { get; set; }

    public int? CategoryId { get; set; }

    public virtual User? Author { get; set; }

    public virtual AnnouncementCategory? Category { get; set; }
}
