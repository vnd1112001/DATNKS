using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models;

public partial class PostCategory
{
    
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
