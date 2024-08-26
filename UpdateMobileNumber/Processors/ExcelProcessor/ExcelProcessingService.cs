namespace UpdateMobileNumber.Processors.ExcelProcessor;
public class ExcelProcessingService(ILogger<ExcelProcessingService> logger, IOptions<AppSettings> options) : IExcelProcessingService
{
    private static readonly Dictionary<string, string> _columnMappings = new()
    {
        { nameof(MobileNumberAmendment.PolicyNumber), "PolicyNumber" },
        { nameof(MobileNumberAmendment.PolicyHolderUniqueId), "PolicyHolderUniqueID" },
        { nameof(MobileNumberAmendment.VehicleVIN), "VehicleVIN" },
        { nameof(MobileNumberAmendment.OldMobileNumber), "PPD.PolicyHolderMobileNo" },
        { nameof(MobileNumberAmendment.NewMobileNumber), "NewMobileNumber" },
    };

    public IEnumerable<MobileNumberAmendment> ProcessExcel(string filePath)
    {
        logger.LogInformation("Starting to process Excel file: {FilePath}", filePath);

        try
        {
            var dataTable = LoadDataTable(filePath);

            return ProcessDataTable(dataTable, filePath);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to process Excel file: {FilePath}", filePath);
            throw;
        }
    }

    private DataTable LoadDataTable(string filePath)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = ExcelReaderFactory.CreateReader(stream);

        var result = reader.AsDataSet(new ExcelDataSetConfiguration
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                UseHeaderRow = true
            }
        });

        var dataTable = result.Tables[options.Value.ExcelSheetTableName];
        if (dataTable is null)
        {
            logger.LogError("Sheet 'Updated Policies' not found in Excel file: {FilePath}", filePath);
            throw new Exception("Sheet 'Updated Policies' not found");
        }

        return dataTable;
    }

    private IEnumerable<MobileNumberAmendment> ProcessDataTable(DataTable dataTable, string filePath)
    {
        var records = new List<MobileNumberAmendment>();

        logger.LogInformation("Excel sheet 'Updated Policies' contains {RowCount} rows.", dataTable.Rows.Count);

        foreach (DataRow row in dataTable.Rows)
        {
            try
            {
                var record = CreateMobileNumberAmendment(row);
                records.Add(record);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing row in Excel file: {RowIndex}", dataTable.Rows.IndexOf(row));
            }
        }

        logger.LogInformation("Finished processing Excel sheet 'Updated Policies': {FilePath}. Total records: {RecordCount}", filePath, records.Count);

        return records;
    }

    private static MobileNumberAmendment CreateMobileNumberAmendment(DataRow row)
    {
        return new MobileNumberAmendment
        {
            PolicyNumber = row[_columnMappings[nameof(MobileNumberAmendment.PolicyNumber)]]?.ToString() ?? string.Empty,
            PolicyHolderUniqueId = Convert.ToInt64(row[_columnMappings[nameof(MobileNumberAmendment.PolicyHolderUniqueId)]]),
            VehicleVIN = row[_columnMappings[nameof(MobileNumberAmendment.VehicleVIN)]]?.ToString() ?? string.Empty,
            OldMobileNumber = row[_columnMappings[nameof(MobileNumberAmendment.OldMobileNumber)]]?.ToString() ?? string.Empty,
            NewMobileNumber = row[_columnMappings[nameof(MobileNumberAmendment.NewMobileNumber)]]?.ToString() ?? string.Empty,
        };
    }
}
