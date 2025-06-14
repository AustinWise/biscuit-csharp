namespace us.awise.biscuits;

public enum SignatureAlgorithm
{
    Ed25519,
    Secp256r1,
}

internal static class SignatureAlgorithmExtensions
{
    public static generated.SignatureAlgorithm ToGenerated(this SignatureAlgorithm algorithm)
    {
        return algorithm switch
        {
            SignatureAlgorithm.Ed25519 => generated.SignatureAlgorithm.Ed25519,
            SignatureAlgorithm.Secp256r1 => generated.SignatureAlgorithm.Secp256r1,
            _ => throw new ArgumentOutOfRangeException(nameof(algorithm)),
        };
    }
}