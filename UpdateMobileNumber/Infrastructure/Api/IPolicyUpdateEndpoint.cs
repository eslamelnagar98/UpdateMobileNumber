namespace UpdateMobileNumber.Infrastructure.Api;
public interface IPolicyUpdateEndpoint
{
    [Post("/update-policy")]
    Task<ApiResponse<List<ICUpdatePolicyResponse>>> UpdatePolicyAsync([Body] ICUpdatePolicyRequest request, [Header("LessorID")] string lessorId);
}
