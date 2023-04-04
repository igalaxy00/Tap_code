using System.Text;

namespace TestP;
//using NUnit.Framework;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TestTap
{
    [TestMethod]
    public void Setup1()
    {
        
        var clearBytes1 = File.ReadAllBytes("test/testoutput1.json");
        var clearBytes2 = File.ReadAllBytes("test/output1.json");
        Console.WriteLine(Program.subOut);
        Assert.AreEqual(ConvertBytesToHex(clearBytes1), ConvertBytesToHex(clearBytes2));
    }
    
    [TestMethod]
    public void Setup2()
    {
        var clearBytes1 = File.ReadAllBytes("test/testoutput2.json");
        var clearBytes2 = File.ReadAllBytes("test/output2.json");
        Console.WriteLine(Program.subOut);
        Assert.AreEqual(ConvertBytesToHex(clearBytes1), ConvertBytesToHex(clearBytes2));
    }
    
    [TestMethod]
    public void Setup3()
    {
        var clearBytes1 = File.ReadAllBytes("test/testoutput3.json");
        var clearBytes2 = File.ReadAllBytes("test/output3.json");
        Console.WriteLine(Program.subOut);
        Assert.AreEqual(ConvertBytesToHex(clearBytes1), ConvertBytesToHex(clearBytes2));
    }
    
    
    public string ConvertBytesToHex(byte[] bytes)
    {
        var sb = new StringBuilder();

        for(var i=0; i<bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString("x"));
        }
        return sb.ToString();
    }
    
}