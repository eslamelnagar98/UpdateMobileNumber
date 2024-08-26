namespace UpdateMobileNumber.Options;
public sealed class AppSettings
{
    public const string SectionName = "AppSettings";
    public string ExcelFilePath { get; init; } = string.Empty;
    public string ApiBaseUrl { get; init; } = string.Empty;
    public string ConnectionString { get; init; } = string.Empty;
    public string ExcelSheetTableName { get; set; } = string.Empty;
}
