using System;
using System.Collections.Generic;

namespace MVCWebAppEFTest.DAL.Entities;

public partial class VisitorLog
{
    public int VisitorLogsId { get; set; }

    public int PatientId { get; set; }

    public int VisitorId { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Visitor Visitor { get; set; } = null!;
}
