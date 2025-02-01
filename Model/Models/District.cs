using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class District
{
    public int DistrictId { get; set; }

    public string? Name { get; set; }

    public bool? Active { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public virtual ICollection<Taluka> Talukas { get; set; } = new List<Taluka>();
}
