// See https://aka.ms/new-console-template for more information
using YamlDotNet.Core;
using YamlDotNet.Serialization;

Console.WriteLine("Hello, World!");


string yaml = $@"MyBool: true
hi: 1
MyChar: h
MyDateTime: {DateTime.Now}
MyDecimal: 123.935
MyDouble: 456.789
MyEnumY: Y
MyEnumZ: 1
MyInt16: {short.MaxValue}
MyInt32: {int.MaxValue}
MyInt64: {long.MaxValue}
MySByte: {sbyte.MaxValue}
MySingle: {float.MaxValue}
MyString: hello world
MyUInt16: {ushort.MaxValue}
MyUInt32: {uint.MaxValue}
MyUInt64: {ulong.MaxValue}
Inner:
  Text: yay
MyArray:
  myArray:
  - 1
  - 2
  - 3
MyDictionary:
  x: y
  a: b
MyList:
  - a
  - b
";

var input = new StringReader(yaml);

#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
var aotContext = new YamlDotNet.Static.StaticContext();
var deserializer = new DeserializerBuilder()
    .WithStaticContext(aotContext)
    .Build();
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.

var x = deserializer.Deserialize<PrimitiveTypes>(input);
Console.WriteLine("Object read:");
Console.WriteLine("MyBool: <{0}>", x.MyBool);
Console.WriteLine("MyByte: <{0}>", x.MyByte);
Console.WriteLine("MyChar: <{0}>", x.MyChar);
Console.WriteLine("MyDateTime: <{0}>", x.MyDateTime);
Console.WriteLine("MyEnumY: <{0}>", x.MyEnumY);
Console.WriteLine("MyEnumZ: <{0}>", x.MyEnumZ);
Console.WriteLine("MyInt16: <{0}>", x.MyInt16);
Console.WriteLine("MyInt32: <{0}>", x.MyInt32);
Console.WriteLine("MyInt64: <{0}>", x.MyInt64);
Console.WriteLine("MySByte: <{0}>", x.MySByte);
Console.WriteLine("MyString: <{0}>", x.MyString);
Console.WriteLine("MyUInt16: <{0}>", x.MyUInt16);
Console.WriteLine("MyUInt32: <{0}>", x.MyUInt32);
Console.WriteLine("MyUInt64: <{0}>", x.MyUInt64);
Console.WriteLine("Inner == null: <{0}>", x.Inner == null);
Console.WriteLine("Inner.Text: <{0}>", x.Inner?.Text);
Console.WriteLine("MyArray == null: <{0}>", x.MyArray == null);
Console.WriteLine("MyArray.myArray == null: <{0}>", x.MyArray?.myArray == null);

if (x.MyArray?.myArray != null)
{
Console.WriteLine("MyArray.myArray: <{0}>", string.Join(',', x.MyArray.myArray));
}

Console.WriteLine("MyDictionary == null: <{0}>", x.MyDictionary == null);
if (x.MyDictionary != null)
{
foreach (var kvp in x.MyDictionary)
{
Console.WriteLine("MyDictionary[{0}] = <{1}>", kvp.Key, kvp.Value);
}
}

Console.WriteLine("MyList == null: <{0}>", x.MyList == null);
if (x.MyList != null)
{
foreach (var value in x.MyList)
{
Console.WriteLine("MyList = <{0}>", value);
}
}

Console.WriteLine("==============");
Console.WriteLine("Serialized:");

#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
var serializer = new SerializerBuilder()
    .WithStaticContext(new YamlDotNet.Static.StaticContext())
    .Build();
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.

var output = serializer.Serialize(x);
Console.WriteLine(output);

[YamlSerializable]
public class MyArray
{
    public int[] myArray { get; set; }
}

[YamlSerializable]
public class Inner
{
    public string Text { get; set; }
}

[YamlSerializable]
public class PrimitiveTypes
{
    [YamlMember(Description = "hi world!")]
    public bool MyBool { get; set; }
    [YamlMember(Alias = "hi")]
    public byte MyByte { get; set; }
    public char MyChar { get; set; }
    public decimal MyDecimal { get; set; }
    public double MyDouble { get; set; }
    public DateTime MyDateTime { get; set; }
    public MyTestEnum MyEnumY { get; set; }
    public MyTestEnum MyEnumZ { get; set; }
    public short MyInt16 { get; set; }
    public int MyInt32 { get; set; }
    public long MyInt64 { get; set; }
    public sbyte MySByte { get; set; }
    public float MySingle { get; set; }
    [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
    public string MyString { get; set; }
    public ushort MyUInt16 { get; set; }
    public uint MyUInt32 { get; set; }
    public ulong MyUInt64 { get; set; }
    public Inner Inner { get; set; }
    public MyArray MyArray { get; set; }
    public Dictionary<string, string> MyDictionary { get; set; }
    public List<string> MyList { get; set; }
}

public enum MyTestEnum
{
    Y = 0,
    Z = 1,
}



namespace YamlDotNet.Static
{
    // The rest of this partial class gets generated at build time
    public partial class StaticContext : YamlDotNet.Serialization.StaticContext
    {
    }
}
