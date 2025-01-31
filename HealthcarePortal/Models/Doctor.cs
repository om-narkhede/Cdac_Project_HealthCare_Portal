using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthcarePortal.Models;

public partial class Doctor
{
    public int DId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? Dob { get; set; }

    public string? Qualification { get; set; }

    public string? Specialization { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }
    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    [JsonIgnore]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
