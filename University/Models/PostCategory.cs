using System;
using System.Collections.Generic;

namespace University.Models;

public partial class PostCategory
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
