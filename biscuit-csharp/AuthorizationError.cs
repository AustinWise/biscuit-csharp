using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public enum AuthorizationErrorKind
{
    LogicUnauthorized,
    LogicNoMatchingPolicy,
}

public unsafe class AuthorizationError
{
    internal static AuthorizationErrorKind? TryGetAuthorizationErrorKind()
    {
        switch (error_kind())
        {
            case generated.ErrorKind.LogicUnauthorized:
                return AuthorizationErrorKind.LogicUnauthorized;
            case generated.ErrorKind.LogicNoMatchingPolicy:
                return AuthorizationErrorKind.LogicNoMatchingPolicy;
            default:
                return null;
        }

    }

    internal AuthorizationError(AuthorizationErrorKind kind)
    {
        Kind = kind;
        ulong count = error_check_count();
        var checks = new AuthorizationCheckFailure[count];
        for (ulong ndx = 0; ndx < count; ndx++)
        {
            checks[ndx] = new AuthorizationCheckFailure(
            error_check_is_authorizer(ndx) != 0,
            ndx,
            error_check_id(ndx),
            error_check_block_id(ndx),
            // error_check_rule takes care of handling the lifetime of the string
            CString.ToString(error_check_rule(ndx)));
        }
        this.Checks = checks;
    }

    public AuthorizationErrorKind Kind { get; }

    public AuthorizationCheckFailure[] Checks { get; }

}

public readonly struct AuthorizationCheckFailure
{
    public AuthorizationCheckFailure(bool isAuthorizer, ulong index, ulong id, ulong block, string rule)
    {
        this.IsAuthorizer = isAuthorizer;
        this.Index = index;
        this.Id = id;
        this.Block = block;
        this.Rule = rule;
    }

    public ulong Id { get; }
    public ulong Index { get; }

    /// <summary>
    /// Only meaningful when <see cref="IsAuthorizer"/> is <c>false</c>.
    /// </summary>
    public ulong Block { get; }

    public string Rule { get; }

    public bool IsAuthorizer { get; }

    public override string ToString()
    {
        if (IsAuthorizer)
        {
            return $"Authorizer Failed: Index={Index} ID={Id} Rule={Rule}";
        }
        else
        {
            return $"Block Failed: Index={Index} ID={Id} Block={Block} Rule={Rule}";
        }
    }
}
