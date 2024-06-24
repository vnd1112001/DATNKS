using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Post
{
    public int PostId { get; set; }

    public string? Title { get; set; }

    public string PostImage { get; set; } = null!;

    public string? Content { get; set; }

    public int? AuthorId { get; set; }

    public DateOnly? PublishedDate { get; set; }

    public int? CategoryId { get; set; }

    public virtual User? Author { get; set; }

    public virtual PostCategory? Category { get; set; }
}
