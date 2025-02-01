using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class Taluka
{
    public int TalukaId { get; set; }

    public int DistrictId { get; set; }

    public string? Name { get; set; }

    public bool? Active { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual ICollection<Village> Villages { get; set; } = new List<Village>();
}
