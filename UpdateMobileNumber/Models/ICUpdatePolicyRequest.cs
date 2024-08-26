namespace UpdateMobileNumber.Models;
public class ICUpdatePolicyRequest
{
    public long PurchasedPolicyDetailID { get; set; }
    public long PolicyAmendmentID { get; set; }
    public short InsuranceCompanyCode { get; set; }
    public long QuoteReferenceNo { get; set; }
    public long PolicyReferenceNo { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public long LessorID { get; set; }
    public long LesseeID { get; set; }
    public long? VehicleSequenceNumber { get; set; }
    public long? VehicleCustomID { get; set; }
    public short? VehiclePlateTypeID { get; set; }
    public short? VehiclePlateNumber { get; set; }
    public short? FirstPlateLetterID { get; set; }
    public short? SecondPlateLetterID { get; set; }
    public short? ThirdPlateLetterID { get; set; }
    public string Email { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public List<NationalAddress> LesseeNationalAddress { get; set; } = new();
    public short UpdatePolicyReasonID { get; set; }
    public int? CreatedBy { get; set; }

}
