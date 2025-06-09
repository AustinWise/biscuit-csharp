using System.Runtime.InteropServices;

namespace us.awise.biscuits.generated
{
    internal enum ErrorKind
    {
        None,
        InvalidArgument,
        InternalError,
        FormatSignatureInvalidFormat,
        FormatSignatureInvalidSignature,
        FormatSealedSignature,
        FormatEmptyKeys,
        FormatUnknownPublicKey,
        FormatDeserializationError,
        FormatSerializationError,
        FormatBlockDeserializationError,
        FormatBlockSerializationError,
        FormatVersion,
        FormatInvalidBlockId,
        FormatExistingPublicKey,
        FormatSymbolTableOverlap,
        FormatPublicKeyTableOverlap,
        FormatUnknownExternalKey,
        FormatUnknownSymbol,
        AppendOnSealed,
        LogicInvalidBlockRule,
        LogicUnauthorized,
        LogicAuthorizerNotEmpty,
        LogicNoMatchingPolicy,
        LanguageError,
        TooManyFacts,
        TooManyIterations,
        Timeout,
        ConversionError,
        FormatInvalidKeySize,
        FormatInvalidSignatureSize,
        FormatInvalidKey,
        FormatSignatureDeserializationError,
        FormatBlockSignatureDeserializationError,
        FormatSignatureInvalidSignatureGeneration,
        AlreadySealed,
        Execution,
        UnexpectedQueryResult,
        FormatPKCS8,
    }

    internal enum SignatureAlgorithm
    {
        Ed25519,
        Secp256r1,
    }

    internal partial struct Authorizer
    {
    }

    internal partial struct AuthorizerBuilder
    {
    }

    internal partial struct Biscuit
    {
    }

    internal partial struct BiscuitBuilder
    {
    }

    internal partial struct BlockBuilder
    {
    }

    internal partial struct KeyPair
    {
    }

    internal partial struct PublicKey
    {
    }

    internal static unsafe partial class Methods
    {
        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* error_message();

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("enum ErrorKind")]
        public static extern ErrorKind error_kind();

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint64_t")]
        public static extern ulong error_check_count();

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint64_t")]
        public static extern ulong error_check_id([NativeTypeName("uint64_t")] ulong check_index);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint64_t")]
        public static extern ulong error_check_block_id([NativeTypeName("uint64_t")] ulong check_index);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* error_check_rule([NativeTypeName("uint64_t")] ulong check_index);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte error_check_is_authorizer([NativeTypeName("uint64_t")] ulong check_index);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct KeyPair *")]
        public static extern KeyPair* key_pair_new([NativeTypeName("const uint8_t *")] byte* seed_ptr, [NativeTypeName("uintptr_t")] nuint seed_len, [NativeTypeName("enum SignatureAlgorithm")] SignatureAlgorithm algorithm);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct PublicKey *")]
        public static extern PublicKey* key_pair_public([NativeTypeName("const struct KeyPair *")] KeyPair* kp);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uintptr_t")]
        public static extern nuint key_pair_serialize([NativeTypeName("const struct KeyPair *")] KeyPair* kp, [NativeTypeName("uint8_t *")] byte* buffer_ptr);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct KeyPair *")]
        public static extern KeyPair* key_pair_deserialize([NativeTypeName("uint8_t *")] byte* buffer_ptr, [NativeTypeName("enum SignatureAlgorithm")] SignatureAlgorithm algorithm);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* key_pair_to_pem([NativeTypeName("const struct KeyPair *")] KeyPair* kp);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct KeyPair *")]
        public static extern KeyPair* key_pair_from_pem([NativeTypeName("const char *")] sbyte* pem);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void key_pair_free([NativeTypeName("struct KeyPair *")] KeyPair* _kp);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uintptr_t")]
        public static extern nuint public_key_serialize([NativeTypeName("const struct PublicKey *")] PublicKey* kp, [NativeTypeName("uint8_t *")] byte* buffer_ptr);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct PublicKey *")]
        public static extern PublicKey* public_key_deserialize([NativeTypeName("uint8_t *")] byte* buffer_ptr, [NativeTypeName("enum SignatureAlgorithm")] SignatureAlgorithm algorithm);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* public_key_to_pem([NativeTypeName("const struct PublicKey *")] PublicKey* kp);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct PublicKey *")]
        public static extern PublicKey* public_key_from_pem([NativeTypeName("const char *")] sbyte* pem);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte public_key_equals([NativeTypeName("const struct PublicKey *")] PublicKey* a, [NativeTypeName("const struct PublicKey *")] PublicKey* b);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void public_key_free([NativeTypeName("struct PublicKey *")] PublicKey* _kp);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct BiscuitBuilder *")]
        public static extern BiscuitBuilder* biscuit_builder();

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte biscuit_builder_set_context([NativeTypeName("struct BiscuitBuilder *")] BiscuitBuilder* builder, [NativeTypeName("const char *")] sbyte* context);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte biscuit_builder_set_root_key_id([NativeTypeName("struct BiscuitBuilder *")] BiscuitBuilder* builder, [NativeTypeName("uint32_t")] uint root_key_id);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte biscuit_builder_add_fact([NativeTypeName("struct BiscuitBuilder *")] BiscuitBuilder* builder, [NativeTypeName("const char *")] sbyte* fact);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte biscuit_builder_add_rule([NativeTypeName("struct BiscuitBuilder *")] BiscuitBuilder* builder, [NativeTypeName("const char *")] sbyte* rule);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte biscuit_builder_add_check([NativeTypeName("struct BiscuitBuilder *")] BiscuitBuilder* builder, [NativeTypeName("const char *")] sbyte* check);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct Biscuit *")]
        public static extern Biscuit* biscuit_builder_build([NativeTypeName("const struct BiscuitBuilder *")] BiscuitBuilder* builder, [NativeTypeName("const struct KeyPair *")] KeyPair* key_pair, [NativeTypeName("const uint8_t *")] byte* seed_ptr, [NativeTypeName("uintptr_t")] nuint seed_len);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void biscuit_builder_free([NativeTypeName("struct BiscuitBuilder *")] BiscuitBuilder* _builder);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct Biscuit *")]
        public static extern Biscuit* biscuit_from([NativeTypeName("const uint8_t *")] byte* biscuit_ptr, [NativeTypeName("uintptr_t")] nuint biscuit_len, [NativeTypeName("const struct PublicKey *")] PublicKey* root);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uintptr_t")]
        public static extern nuint biscuit_serialized_size([NativeTypeName("const struct Biscuit *")] Biscuit* biscuit);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uintptr_t")]
        public static extern nuint biscuit_sealed_size([NativeTypeName("const struct Biscuit *")] Biscuit* biscuit);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uintptr_t")]
        public static extern nuint biscuit_serialize([NativeTypeName("const struct Biscuit *")] Biscuit* biscuit, [NativeTypeName("uint8_t *")] byte* buffer_ptr);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uintptr_t")]
        public static extern nuint biscuit_serialize_sealed([NativeTypeName("const struct Biscuit *")] Biscuit* biscuit, [NativeTypeName("uint8_t *")] byte* buffer_ptr);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uintptr_t")]
        public static extern nuint biscuit_block_count([NativeTypeName("const struct Biscuit *")] Biscuit* biscuit);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern sbyte* biscuit_block_context([NativeTypeName("const struct Biscuit *")] Biscuit* biscuit, [NativeTypeName("uint32_t")] uint block_index);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct BlockBuilder *")]
        public static extern BlockBuilder* create_block();

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct Biscuit *")]
        public static extern Biscuit* biscuit_append_block([NativeTypeName("const struct Biscuit *")] Biscuit* biscuit, [NativeTypeName("const struct BlockBuilder *")] BlockBuilder* block_builder, [NativeTypeName("const struct KeyPair *")] KeyPair* key_pair);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct Authorizer *")]
        public static extern Authorizer* biscuit_authorizer([NativeTypeName("const struct Biscuit *")] Biscuit* biscuit);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void biscuit_free([NativeTypeName("struct Biscuit *")] Biscuit* _biscuit);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte block_builder_set_context([NativeTypeName("struct BlockBuilder *")] BlockBuilder* builder, [NativeTypeName("const char *")] sbyte* context);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte block_builder_add_fact([NativeTypeName("struct BlockBuilder *")] BlockBuilder* builder, [NativeTypeName("const char *")] sbyte* fact);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte block_builder_add_rule([NativeTypeName("struct BlockBuilder *")] BlockBuilder* builder, [NativeTypeName("const char *")] sbyte* rule);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte block_builder_add_check([NativeTypeName("struct BlockBuilder *")] BlockBuilder* builder, [NativeTypeName("const char *")] sbyte* check);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void block_builder_free([NativeTypeName("struct BlockBuilder *")] BlockBuilder* _builder);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct AuthorizerBuilder *")]
        public static extern AuthorizerBuilder* authorizer_builder();

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte authorizer_builder_add_fact([NativeTypeName("struct AuthorizerBuilder *")] AuthorizerBuilder* builder, [NativeTypeName("const char *")] sbyte* fact);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte authorizer_builder_add_rule([NativeTypeName("struct AuthorizerBuilder *")] AuthorizerBuilder* builder, [NativeTypeName("const char *")] sbyte* rule);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte authorizer_builder_add_check([NativeTypeName("struct AuthorizerBuilder *")] AuthorizerBuilder* builder, [NativeTypeName("const char *")] sbyte* check);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte authorizer_builder_add_policy([NativeTypeName("struct AuthorizerBuilder *")] AuthorizerBuilder* builder, [NativeTypeName("const char *")] sbyte* policy);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct Authorizer *")]
        public static extern Authorizer* authorizer_builder_build([NativeTypeName("struct AuthorizerBuilder *")] AuthorizerBuilder* builder, [NativeTypeName("const struct Biscuit *")] Biscuit* token);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("struct Authorizer *")]
        public static extern Authorizer* authorizer_builder_build_unauthenticated([NativeTypeName("struct AuthorizerBuilder *")] AuthorizerBuilder* builder);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void authorizer_builder_free([NativeTypeName("struct AuthorizerBuilder *")] AuthorizerBuilder* _builder);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("_Bool")]
        public static extern byte authorizer_authorize([NativeTypeName("struct Authorizer *")] Authorizer* authorizer);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern sbyte* authorizer_print([NativeTypeName("struct Authorizer *")] Authorizer* authorizer);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void authorizer_free([NativeTypeName("struct Authorizer *")] Authorizer* _authorizer);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void string_free([NativeTypeName("char *")] sbyte* ptr);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* biscuit_print([NativeTypeName("const struct Biscuit *")] Biscuit* biscuit);

        [DllImport("biscuit_auth", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* biscuit_print_block_source([NativeTypeName("const struct Biscuit *")] Biscuit* biscuit, [NativeTypeName("uint32_t")] uint block_index);
    }
}
