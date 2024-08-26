namespace UpdateMobileNumber.Processors.CSVProcessor;
public class CsvProcessingService(ILogger<CsvProcessingService> logger) : ICsvProcessingService
{
    public IEnumerable<MobileNumberAmendment> ProcessCsv(string filePath)
    {
        logger.LogInformation("Processing CSV file: {FilePath}", filePath);

        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new StreamReader(stream);

        var csvConfig = CreateCsvConfiguration();

        using var csv = new CsvReader(reader, csvConfig);

        csv.Context.RegisterClassMap<MobileNumberAmendmentMapper>();

        return GetRecords(csv).ToList();
    }

    private static CsvConfiguration CreateCsvConfiguration()
    {
        return new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MissingFieldFound = HandleMissingField,
            PrepareHeaderForMatch = args => args.Header.Trim()
        };
    }

    private static IEnumerable<MobileNumberAmendment> GetRecords(CsvReader csv)
    {
        return csv.GetRecords<MobileNumberAmendment>();
    }

    private static void HandleMissingField(MissingFieldFoundArgs args)
    {
        Console.WriteLine($"Field with name '{args.HeaderNames?.FirstOrDefault()}' at index '{args.Index}' is missing.");
    }
}
