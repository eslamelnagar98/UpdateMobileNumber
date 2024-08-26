namespace UpdateMobileNumber.Processors.CSVProcessor;
public interface ICsvProcessingService
{
    IEnumerable<MobileNumberAmendment> ProcessCsv(string filePath);
}
