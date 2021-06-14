using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jvedio.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Test test = null;

            if (test?.str != "123")
            {
                Assert.AreEqual(null, test?.str); // 验证待测试方法的执行结果是否与预期相同
            }


        }
    }

    class Test
    {
        public static string s;
        public string str;
        public int a;
    }
}
