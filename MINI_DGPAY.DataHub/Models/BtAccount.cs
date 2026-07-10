using System;
using System.Collections.Generic;

namespace MINI_DGPAY.DataHub.Models;

public partial class BtAccount
{
    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? UserPass { get; set; }

    public int? UserPin { get; set; }

    public string? UserMobileNo { get; set; }

    public decimal? UserBalance { get; set; }

    public int? UserStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }
}
