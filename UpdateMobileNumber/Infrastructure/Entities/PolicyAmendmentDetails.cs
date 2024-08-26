namespace UpdateMobileNumber.Infrastructure.Entities;
public class PolicyAmendmentDetails
{
    public long PolicyHolderID { get; set; }
    public string PolicyHolderName { get; set; } = string.Empty;
    public long? PolicyHolderUniqueID { get; set; }
    public string PolicyHolderEmail { get; set; } = string.Empty;
    public long? PolicyHolderMobileNo { get; set; }
    public string PolicyHolderAddress { get; set; } = string.Empty;
    public DateTime? PolicyExpiryDate { get; set; }
    public DateTime PolicyPurchasedDate { get; set; }
    public string VehicleMake { get; set; } = string.Empty;
    public string VehicleModel { get; set; } = string.Empty;
    public string VehicleMakeLogo { get; set; } = string.Empty;
    public short? VehiclePlateNumber { get; set; }
    public string VehiclePlateNumberAr { get; set; } = string.Empty;
    public string VehiclePlateLetterEn { get; set; } = string.Empty;
    public string VehiclePlateLetterAr { get; set; } = string.Empty;
    public string CompanyAliasEnglish { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string CompanyNameEnglish { get; set; } = string.Empty;
    public string CompanyNameArabic { get; set; } = string.Empty;
    public int InsuranceCompanyID { get; set; }
    public short? VehicleModelID { get; set; }
    public short? VehicleMakeID { get; set; }
    public long VehicleID { get; set; }
    public long? CustomID { get; set; }
    public long? SequenceNumber { get; set; }
    public short? VehicleUniqueTypeID { get; set; }
    public short? BuildingNumber { get; set; }
    public string Street { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int? ZipCode { get; set; }
    public short? AdditionalNumber { get; set; }
    public long? PolicyQuoteResponseID { get; set; }
    public long PolicyDetailID { get; set; }
    public long QueryID { get; set; }
    public string LessorIdentity { get; set; } = string.Empty;
    public long LessorID { get; set; }
    public long? LessorPolicyHolderID { get; set; }
    public bool AmendEmail { get; set; }
    public bool AmendMobileNumber { get; set; }
    public bool AmendPlateNumber { get; set; }
    public bool AmendAdditionalCoverage { get; set; }
    public bool AmendAdditionalDriver { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public long? QuoteReferenceNo { get; set; }
    public short CompanyCode { get; set; }
    public long PolicyReferenceNo { get; set; }
    public string VehicleVIN { get; set; } = string.Empty;
    public long LessorUniqueId { get; set; }
    public short? ManufactureYear { get; set; }
    public byte? RepairMethodID { get; set; }
    public string RepairMethodEnglishName { get; set; } = string.Empty;
    public string RepairMethodArabicName { get; set; } = string.Empty;
    public long VehicleEstimatedValue { get; set; }
    public string VehicleColor { get; set; } = string.Empty;
    public string VehicleColorAr { get; set; } = string.Empty;
    public short? CancellationStatusID { get; set; }
    public bool IsCancelled { get; set; }
}
