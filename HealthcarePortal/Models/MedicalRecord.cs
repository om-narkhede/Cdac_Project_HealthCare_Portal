using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthcarePortal.Models;

public partial class MedicalRecord
{
    public int RecordId { get; set; }

    public int? DId { get; set; }

    public int? PId { get; set; }

    public DateOnly RecordDate { get; set; }

    public string? Description { get; set; }

    public string? Prescription { get; set; }

    public string? Attachment { get; set; }
    [JsonIgnore]
    public virtual Doctor? DIdNavigation { get; set; }
    [JsonIgnore]
    public virtual Patient? PIdNavigation { get; set; }
}
