namespace UpdateMobileNumber.Processors.CSVProcessor;
public class MobileNumberAmendmentMapper : ClassMap<MobileNumberAmendment>
{
    public MobileNumberAmendmentMapper()
    {
        Map(m => m.PolicyNumber).Name("PolicyNumber");
        Map(m => m.PolicyHolderUniqueId).Name("PolicyHolderUniqueID");
        Map(m => m.VehicleVIN).Name("VehicleVIN");
        Map(m => m.OldMobileNumber).Name("PPD.PolicyHolderMobileNo");
        Map(m => m.NewMobileNumber).Name("NewMobileNumber");
    }
}
