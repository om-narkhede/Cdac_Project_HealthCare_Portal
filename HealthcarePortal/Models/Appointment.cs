using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthcarePortal.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? DId { get; set; }

    public int? PId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public string? Status { get; set; }

    [JsonIgnore]
    public virtual Doctor? DIdNavigation { get; set; }
    [JsonIgnore]
    public virtual Patient? PIdNavigation { get; set; }
}
