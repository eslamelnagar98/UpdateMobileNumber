namespace UpdateMobileNumber.Models;
public class ICUpdatePolicyResponse
{
    public long ICUpdatePolicyRequestID { get; set; }
    public long? ICUpdatePolicyResponseID { get; set; }
    public bool Status { get; set; }
    public List<ErrorDetails> Errors { get; set; } = [];
}

public class ErrorDetails
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorDetails"/> class.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <param name="message">The message.</param>
    /// <param name="code">The code.</param>
    public ErrorDetails(string field, string message, string code)
    {
        Field = field;
        Message = message;
        Code = code;
    }

    /// <summary>
    /// Gets or sets the field.
    /// </summary>
    /// <value>
    /// The field.
    /// </value>
    [JsonProperty("field")]
    public string Field { get; set; }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    /// <value>
    /// The code.
    /// </value>
    [JsonProperty("code")]
    public string Code { get; set; }
}
