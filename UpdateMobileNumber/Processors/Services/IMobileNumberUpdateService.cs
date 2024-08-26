namespace UpdateMobileNumber.Processors.Services;
public interface IMobileNumberUpdateService
{
    Task ProcessAndFetchIdsAsync(CancellationToken cancellationToken = default);

}
