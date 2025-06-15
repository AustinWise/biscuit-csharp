// See https://aka.ms/new-console-template for more information
using us.awise.biscuits;

Console.WriteLine("Hello, World!");

using var rootKp = new KeyPair(SignatureAlgorithm.Ed25519);
using var root = rootKp.GetPublicKey();

using Biscuit biscuit = Biscuit.Create(rootKp, static builder =>
{
    builder.AddFact("right(\"file1\", \"read\")"u8);
});

using var kp2 = new KeyPair(SignatureAlgorithm.Ed25519);

using Biscuit b2 = biscuit.AppendBlock(kp2, static builder =>
{
    builder.AddCheck("check if operation(\"read\")"u8);
    builder.AddFact("hello(\"world\")"u8);
});

using Authorizer authorizer = Authorizer.Create(b2, static builder =>
{
    builder.AddCheck("check if right(\"efgh\")"u8);
    builder.AddPolicy("allow if true"u8);
});

try
{
    authorizer.Authorize();
}
catch (BiscuitException ex)
{
    Console.WriteLine("Expected auth failure: " + ex.Message);
    Console.WriteLine("authorizer world:");
    Console.WriteLine(authorizer);
}

Console.WriteLine("DONE!");
