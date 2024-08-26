namespace UpdateMobileNumber.Infrastructure;
public sealed class MobileNumberDatabaseService(IOptions<AppSettings> appSettings, ILogger<MobileNumberDatabaseService> logger)
    : IMobileNumberDatabaseService
{
    private readonly string _connectionString = appSettings.Value.ConnectionString;

    public async Task<long?> GetPurchasedPolicyDetailIdAsync(string policyNumber, CancellationToken cancellationToken = default)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var sql = "SELECT PurchasedPolicyDetailID FROM purchasedPolicydetail WHERE PolicyNumber = @PolicyNumber";
        var id = await connection.QuerySingleOrDefaultAsync<long?>(sql, new { PolicyNumber = policyNumber });

        if (id.HasValue)
        {
            logger.LogInformation($"Fetched Id: {id.Value} for Policy Number: {policyNumber}");
        }
        else
        {
            logger.LogWarning($"No Id found for Policy Number: {policyNumber}");
        }
        return id;
    }

    public async Task<PolicyAmendmentDetails?> GetPolicyAmendmentsByPolicyDetailIdAsync(long policyDetailId, CancellationToken cancellationToken = default)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var sql = @"EXEC GetPolicyAmendmentsByPolicyDetailID @PurchasedPolicyDetailID";

        var amendment = await connection.QueryFirstOrDefaultAsync<PolicyAmendmentDetails>(sql, new { PurchasedPolicyDetailID = policyDetailId });
        if (amendment is null)
        {
            logger.LogWarning($"No amendments found for Policy Detail ID: {policyDetailId}");
        }
        else
        {
            logger.LogInformation($"Fetched amendments for Policy Detail ID: {policyDetailId}");
        }
        return amendment;
    }

    public async Task<long> AddUpdatePolicyAmendmentAsync(PolicyAmendment policyAmendment, CancellationToken cancellationToken = default)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var sql = @"EXEC [dbo].[AddUpdatePolicyAmendment] 
               @PolicyAmendmentID
             , @PurchasedPolicyDetailID
             , @RequestedAccountTypeID
             , @CreatedBy
             , @AmendmentTypeID
             , @RequestedDate
             , @AmendmentStatusID
             , @Amount
             , @AmountVAT
             , @TotalAmount
             , @NewValue
             , @OldValue
             , @IsAcknowledged
             , @ErrorMessage";

        var parameters = new
        {
            PolicyAmendmentID = policyAmendment.PolicyAmendmentID,
            PurchasedPolicyDetailID = policyAmendment.PurchasedPolicyDetailID,
            RequestedAccountTypeID = policyAmendment.RequestedAccountTypeID,
            CreatedBy = policyAmendment.CreatedBy,
            AmendmentTypeID = policyAmendment.AmendmentTypeID,
            RequestedDate = policyAmendment.RequestedDate,
            AmendmentStatusID = policyAmendment.AmendmentStatusID,
            Amount = policyAmendment.Amount,
            AmountVAT = policyAmendment.AmountVAT,
            TotalAmount = policyAmendment.TotalAmount,
            NewValue = policyAmendment.NewValue,
            OldValue = policyAmendment.OldValue,
            IsAcknowledged = policyAmendment.IsAcknowledged,
            ErrorMessage = string.Empty
        };

        var result = await connection.ExecuteScalarAsync<long>(sql, parameters);

        logger.LogInformation($"Added/Updated Policy Amendment with ID: {result}");
        return result;
    }
}
