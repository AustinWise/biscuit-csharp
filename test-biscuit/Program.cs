// See https://aka.ms/new-console-template for more information
using us.awise.biscuits;

Console.WriteLine("Hello, World!");

using var rootKp = new KeyPair();
using var root = rootKp.GetPublicKey();

Biscuit biscuit;
using (var builder = new BiscuitBuilder())
{
    builder.AddFact("right(\"file1\", \"read\")"u8);
    biscuit = builder.Build(rootKp);
}

using var kp2 = new KeyPair();

Biscuit b2;
using (var builder = new BlockBuilder())
{
    builder.AddCheck("check if operation(\"read\")"u8);
    builder.AddFact("hello(\"world\")"u8);

    b2 = biscuit.AppendBlock(builder, kp2);
}

Authorizer authorizer;
using (var builder = new AuthorizerBuilder())
{
    builder.AddCheck("check if right(\"efgh\")"u8);
    builder.AddPolicy("allow if true"u8);
    authorizer = builder.Build(b2);
}

Console.ReadLine();

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

biscuit.Dispose();
b2.Dispose();
authorizer.Dispose();

Console.WriteLine("DONE!");
