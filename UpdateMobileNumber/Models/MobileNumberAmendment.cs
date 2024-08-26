namespace UpdateMobileNumber.Models;
public class MobileNumberAmendment
{
    public long PolicyHolderUniqueId { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public string OldMobileNumber { get; set; } = string.Empty;
    public string NewMobileNumber { get; set; } = string.Empty;
    public string VehicleVIN { get; set; } = string.Empty;
}
