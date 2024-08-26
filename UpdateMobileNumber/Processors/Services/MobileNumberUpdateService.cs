namespace UpdateMobileNumber.Processors.Services;
public sealed class MobileNumberUpdateService(IFileProcessingFactory fileProcessingFactory,
                                              IMobileNumberDatabaseService databaseService,
                                              IPolicyUpdateEndpoint policyUpdateEndpoint,
                                              ILogger<MobileNumberUpdateService> logger) : IMobileNumberUpdateService
{
    public async Task ProcessAndFetchIdsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Starting the process to fetch IDs from mobile number amendments.");

        var mobileNumberAmendments = fileProcessingFactory.ProcessFile();
        foreach (var mobileNumberAmendment in mobileNumberAmendments)
        {
            await ProcessAmendmentAsync(mobileNumberAmendment, cancellationToken);
        }

        logger.LogInformation("Finished processing mobile number amendments.");
    }

    private async Task ProcessAmendmentAsync(MobileNumberAmendment mobileNumberAmendment, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Processing amendment for Policy Number: {mobileNumberAmendment.PolicyNumber}");

        var policyDetailId = await FetchPolicyDetailIdAsync(mobileNumberAmendment.PolicyNumber, cancellationToken);
        if (policyDetailId == null)
        {
            LogMissingPolicyId(mobileNumberAmendment.PolicyNumber);
            return;
        }

        var policyAmendmentDetails = await FetchAndLogAmendmentsAsync(policyDetailId.Value, cancellationToken);
        if (policyAmendmentDetails == null)
        {
            return;
        }

        var policyUpdateRequest = CreatePolicyUpdateRequest(mobileNumberAmendment, policyAmendmentDetails, policyDetailId.Value);
        await databaseService.AddUpdatePolicyAmendmentAsync(policyUpdateRequest);

        var iCUpdatePolicyRequest = CreateICUpdatePolicyRequest(policyUpdateRequest, policyAmendmentDetails, policyDetailId.Value);
        LogPolicyUpdateRequest(iCUpdatePolicyRequest);

        var response = await SendPolicyUpdateRequestAsync(iCUpdatePolicyRequest, cancellationToken);
        LogPolicyUpdateResponse(response);
    }

    private async Task<long?> FetchPolicyDetailIdAsync(string policyNumber, CancellationToken cancellationToken)
    {
        var policyDetailId = await databaseService.GetPurchasedPolicyDetailIdAsync(policyNumber, cancellationToken);
        LogFetchedPolicyId(policyDetailId, policyNumber);
        return policyDetailId;
    }

    private void LogFetchedPolicyId(long? policyDetailId, string policyNumber)
    {
        if (policyDetailId.HasValue)
        {
            logger.LogInformation($"Successfully fetched Id: {policyDetailId.Value} for Policy Number: {policyNumber}");
        }
        else
        {
            logger.LogWarning($"No Id found for Policy Number: {policyNumber}");
        }
    }

    private void LogMissingPolicyId(string policyNumber)
    {
        logger.LogWarning($"No Id found for Policy Number: {policyNumber}");
    }

    private async Task<PolicyAmendmentDetails?> FetchAndLogAmendmentsAsync(long policyDetailId, CancellationToken cancellationToken)
    {
        var amendment = await databaseService.GetPolicyAmendmentsByPolicyDetailIdAsync(policyDetailId, cancellationToken);
        LogFetchedAmendment(amendment, policyDetailId);
        return amendment;
    }

    private void LogFetchedAmendment(PolicyAmendmentDetails? amendment, long policyDetailId)
    {
        if (amendment != null)
        {
            logger.LogInformation($"Amendments fetched for Policy Detail ID: {policyDetailId}");
        }
        else
        {
            logger.LogWarning($"No amendments found for Policy Detail ID: {policyDetailId}");
        }
    }

    private PolicyAmendment CreatePolicyUpdateRequest(MobileNumberAmendment numberAmendment, PolicyAmendmentDetails policyAmendmentDetails, long policyDetailId)
    {
        return new PolicyAmendment
        {
            PolicyAmendmentID = 0,
            PurchasedPolicyDetailID = policyDetailId,
            RequestedAccountTypeID = (byte)RequestedAccountType.ByLessee,
            RequestedDate = DateTime.Now,
            AmendmentStatusID = (byte)AmendmentStatus.Initiated,
            AmendmentTypeID = (byte)AmendmentsType.AmendMobileNumber,
            NewValue = numberAmendment.NewMobileNumber,
            OldValue = numberAmendment.OldMobileNumber,
            IsAcknowledged = null,
        };
    }

    private ICUpdatePolicyRequest CreateICUpdatePolicyRequest(PolicyAmendment policyUpdateRequest, PolicyAmendmentDetails policyAmendmentDetails, long policyDetailId)
    {
        return new ICUpdatePolicyRequest
        {
            PolicyAmendmentID = policyUpdateRequest.PolicyAmendmentID,
            PurchasedPolicyDetailID = policyDetailId,
            LessorID = policyUpdateRequest.LessorID,
            MobileNo = policyUpdateRequest.NewValue,
            PolicyNumber = policyAmendmentDetails.PolicyNumber,
            PolicyReferenceNo = policyAmendmentDetails.PolicyReferenceNo,
            QuoteReferenceNo = policyAmendmentDetails.QuoteReferenceNo.GetValueOrDefault(),
            InsuranceCompanyCode = policyAmendmentDetails.CompanyCode,
            UpdatePolicyReasonID = (int)AmendmentReasonType.UpdatingEmailAndMobileNo,
            VehicleSequenceNumber = policyAmendmentDetails.SequenceNumber,
            VehicleCustomID = policyAmendmentDetails.CustomID
        };
    }

    private void LogPolicyUpdateRequest(ICUpdatePolicyRequest request)
    {
        logger.LogInformation("Policy Update Request: {@Request}", request);
    }

    private async Task<ICUpdatePolicyResponse?> SendPolicyUpdateRequestAsync(ICUpdatePolicyRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending Policy Update Request to external API");

        var apiResponse = await policyUpdateEndpoint.UpdatePolicyAsync(request, request.LessorID.ToString());

        return apiResponse.Content?.FirstOrDefault();
    }

    private void LogPolicyUpdateResponse(ICUpdatePolicyResponse? response)
    {
        if (response == null)
        {
            logger.LogError("Policy Update Response was null or empty.");
            return;
        }

        logger.LogInformation("Policy Update Response: {@Response}", response);

        response.Errors.ForEach(error => logger.LogWarning("First error in Policy Update Response: {@Error}", error));
    }
}
