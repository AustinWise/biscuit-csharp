using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace us.awise.biscuits;

public enum ErrorKind
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
