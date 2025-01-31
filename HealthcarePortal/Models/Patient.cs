using System;
using System.Collections.Generic;

namespace HealthcarePortal.Models;

public partial class Patient
{
    public int PId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Gender { get; set; }

    public string? BloodGroup { get; set; }

    public DateOnly? Dob { get; set; }

    public int? Age { get; set; }

    public string? EmailId { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public long? AdharNumber { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
