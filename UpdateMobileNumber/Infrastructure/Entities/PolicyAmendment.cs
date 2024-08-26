namespace UpdateMobileNumber.Infrastructure.Entities;
public class PolicyAmendment
{
    public long PolicyAmendmentID { get; set; }
    public long PurchasedPolicyDetailID { get; set; }
    public byte? RequestedAccountTypeID { get; set; }
    public int CreatedBy { get; set; }
    public byte AmendmentTypeID { get; set; }
    public DateTime RequestedDate { get; set; }
    public byte AmendmentStatusID { get; set; }
    public decimal? Amount { get; set; }
    public decimal? AmountVAT { get; set; }
    public decimal? TotalAmount { get; set; }
    public string NewValue { get; set; } = string.Empty;
    public string OldValue { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool? IsAcknowledged { get; set; }
    public decimal VatRate { get; set; }
    public string PercentageDriverMapping { get; set; } = string.Empty;
    public long LessorID { get; set; }
    public string LessorNameArabic { get; set; } = string.Empty;
    public string LessorNameEnglish { get; set; } = string.Empty;
}
