namespace UpdateMobileNumber.Processors.ExcelProcessor;
public interface IExcelProcessingService
{
    IEnumerable<MobileNumberAmendment> ProcessExcel(string filePath);
}
