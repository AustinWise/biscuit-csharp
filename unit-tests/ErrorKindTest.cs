using us.awise.biscuits;

namespace unit_tests;

public class ErrorKindTest
{
    static Dictionary<string, int> CreateEnumDictionary(Type enumType)
    {
        var ret = new Dictionary<string, int>();
        foreach (object obj in Enum.GetValues(enumType))
        {
            ret.Add(obj.ToString()!, (int)obj);
        }
        return ret;
    }

    [Fact]
    public void TestGeneratedAndPublicEnumsMatch()
    {
        Type publicEnum = typeof(ErrorKind);
        Type generatedEnum = publicEnum.Assembly.GetType("us.awise.biscuits.generated.ErrorKind", throwOnError: true)!;
        Assert.Equal(typeof(int), Enum.GetUnderlyingType(publicEnum));
        Assert.Equal(typeof(int), Enum.GetUnderlyingType(generatedEnum));

        var publicValues = CreateEnumDictionary(publicEnum);
        var generatedValues = CreateEnumDictionary(generatedEnum);

        Assert.Equal(generatedValues.Count, publicValues.Count);
        foreach (var kvp in generatedValues)
        {
            Assert.True(publicValues.TryGetValue(kvp.Key, out int publicValue));
            Assert.Equal(kvp.Value, publicValue);
        }
    }
}