using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace soft_T
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //测试用例,倒是后开发人员就根据这个用例测 有BUG就写上已解决 没有就写未出现BUG 然后就不管了
            int a = 1, b = 2;
            Calc c = new Calc();
            int result1=c.Add(a,b);
            //手动实现测试测试方法的功能
            int result2 = a + b;
            //设置断言
            Assert.AreEqual(result1, result2);
        }
    }
    public partial class Calc
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
