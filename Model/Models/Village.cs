using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class Village
{
    public int VillageId { get; set; }

    public int TalukaId { get; set; }

    public string? Name { get; set; }

    public bool? Active { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public virtual Taluka Taluka { get; set; } = null!;
}
