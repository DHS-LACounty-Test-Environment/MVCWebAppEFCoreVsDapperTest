using System;
using System.Collections.Generic;

namespace MVCWebAppEFTest.DAL.Entities;

public partial class Visitor
{
    public int VisitorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public short? Age { get; set; }

    public DateTime? Dob { get; set; }

    public string? Address { get; set; }

    public bool IsMinor { get; set; }

    public string PatientMrn { get; set; } = null!;

    public string? PatientFin { get; set; }

    public short RelationshipId { get; set; }

    public short VisitTypeId { get; set; }

    public DateTime CheckInDateTime { get; set; }

    public DateTime? CheckOutDateTime { get; set; }

    public string? Comments { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? PatientLocation { get; set; }

    public virtual ICollection<VisitorLog> VisitorLogs { get; set; } = new List<VisitorLog>();
}
