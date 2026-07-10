using System;
using System.Collections.Generic;

namespace MINI_DGPAY.DataHub.Models;

public partial class BtTransaction
{
    public string TranId { get; set; } = null!;

    public DateTime? TranDate { get; set; }

    public string? TranSender { get; set; }

    public string? TranRecver { get; set; }

    public decimal? TranAmount { get; set; }

    public string? TranRemk { get; set; }

    public string? TranType { get; set; }

    public int? TranStatus { get; set; }
}
