using System.Diagnostics.CodeAnalysis;
using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class Authorizer : IDisposable
{
    private generated.Authorizer* _handle;

    public static Authorizer Create(Biscuit token, Action<AuthorizerBuilder> builderCallback)
    {
        if (token is null)
            throw new ArgumentNullException(nameof(token));
        if (builderCallback is null)
            throw new ArgumentNullException(nameof(builderCallback));

        var ret = new Authorizer(); // create ahead of time so that we don't have to deal with OOM after creating the native handle
        lock (token)
        {
            if (token._handle == null)
            {
                throw new ObjectDisposedException(nameof(token));
            }
            generated.AuthorizerBuilder* builder = authorizer_builder();
            if (builder == null)
                throw BiscuitException.FromLastError();
            try
            {
                builderCallback(new AuthorizerBuilder(builder));
            }
            catch
            {
                authorizer_builder_free(builder);
                throw;
            }
            // This consume the AuthorizerBuilder*
            ret._handle = authorizer_builder_build(builder, token._handle);
            GC.KeepAlive(token);
        }
        if (ret._handle == null)
        {
            throw BiscuitException.FromLastError();
        }

        return ret;
    }

    private Authorizer()
    {
    }

    public void Authorize()
    {
        bool throwLastError = false;
        lock (this)
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(Authorizer));
            throwLastError = 0 == authorizer_authorize(_handle);
            GC.KeepAlive(this);
        }
        if (throwLastError)
        {
            AuthorizationError? authorizationError = null;
            AuthorizationErrorKind? kind = AuthorizationError.TryGetAuthorizationErrorKind();
            if (kind.HasValue)
            {
                authorizationError = new AuthorizationError(kind.Value);
            }
            throw BiscuitException.FromLastError(authorizationError);
        }
    }

    /// <returns>true if the authorization succeeded</returns>
    /// <exception cref="BiscuitException">When an error other than authorization occurs.</exception>
    public bool TryAuthorize()
    {
        bool ret;
        lock (this)
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(Authorizer));
            ret = 0 != authorizer_authorize(_handle);
            GC.KeepAlive(this);
        }

        if (!ret && !AuthorizationError.TryGetAuthorizationErrorKind().HasValue)
        {
            throw BiscuitException.FromLastError();
        }

        return ret;
    }

    /// <param name="authError">if authorization fails, this contains why</param>
    /// <returns>true if the authorization succeeded</returns>
    /// <exception cref="BiscuitException">When an error other than authorization occurs.</exception>
    public bool TryAuthorize([NotNullWhen(false)] out AuthorizationError? authError)
    {
        authError = default;

        bool ret;
        lock (this)
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(Authorizer));
            ret = 0 != authorizer_authorize(_handle);

            GC.KeepAlive(this);
        }

        if (!ret)
        {
            AuthorizationErrorKind? kind = AuthorizationError.TryGetAuthorizationErrorKind();
            if (kind.HasValue)
            {
                authError = new AuthorizationError(kind.Value);
            }
            else
            {
                throw BiscuitException.FromLastError();
            }
        }

        return ret;
    }

    ~Authorizer()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        generated.Authorizer* handle;
        lock (this)
        {
            handle = _handle;
            _handle = null;
        }
        if (handle != null)
        {
            authorizer_free(handle);
        }
    }

    public override string ToString()
    {
        sbyte* cstr = null;
        try
        {
            lock (this)
            {
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(Authorizer));
                cstr = authorizer_print(_handle);
                GC.KeepAlive(this);
            }
            if (cstr == null)
            {
                throw BiscuitException.FromLastError();
            }
            return CString.ToString(cstr);
        }
        finally
        {
            if (cstr != null)
            {
                string_free(cstr);
            }
        }
    }
}
