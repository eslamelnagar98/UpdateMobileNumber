namespace UpdateMobileNumber.Infrastructure;
public interface IMobileNumberDatabaseService
{
    Task<long?> GetPurchasedPolicyDetailIdAsync(string policyNumber, CancellationToken cancellationToken = default);
    Task<PolicyAmendmentDetails?> GetPolicyAmendmentsByPolicyDetailIdAsync(long policyDetailId, CancellationToken cancellationToken = default);
    Task<long> AddUpdatePolicyAmendmentAsync(PolicyAmendment policyAmendment, CancellationToken cancellationToken = default);
}
