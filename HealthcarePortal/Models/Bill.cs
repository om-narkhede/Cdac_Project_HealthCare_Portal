using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthcarePortal.Models;

public partial class Bill
{
    public int BillNo { get; set; }

    public int? PId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public decimal? Amount { get; set; }
    [JsonIgnore]
    public virtual Patient? PIdNavigation { get; set; }
}
