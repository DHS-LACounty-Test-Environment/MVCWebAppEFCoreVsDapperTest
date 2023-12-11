using System;
using System.Collections.Generic;

namespace MVCWebAppEFTest.DAL.Entities;

public partial class Patient
{
    public int PatientId { get; set; }

    public string Name { get; set; } = null!;

    public short Age { get; set; }

    public virtual ICollection<VisitorLog> VisitorLogs { get; set; } = new List<VisitorLog>();
}
