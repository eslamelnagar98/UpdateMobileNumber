namespace UpdateMobileNumber.Processors.ProcessorFactory;
public interface IFileProcessingFactory
{
    IEnumerable<MobileNumberAmendment> ProcessFile();
}
