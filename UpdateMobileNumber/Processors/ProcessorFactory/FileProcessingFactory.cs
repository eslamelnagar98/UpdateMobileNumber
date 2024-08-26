namespace UpdateMobileNumber.Processors.ProcessorFactory;
public sealed class FileProcessingFactory(IExcelProcessingService excelService,
                                          ICsvProcessingService csvService,
                                          IOptions<AppSettings> appSettings) : IFileProcessingFactory
{
    public IEnumerable<MobileNumberAmendment> ProcessFile()
    {
        var filePath = appSettings.Value.ExcelFilePath;
        var extension = Path.GetExtension(filePath)?.ToLower();

        return extension switch
        {
            ExcelConst.xlsx => excelService.ProcessExcel(filePath),
            ExcelConst.csv => csvService.ProcessCsv(filePath),
            _ => throw new NotSupportedException($"File extension {extension} is not supported")
        };
    }
}

