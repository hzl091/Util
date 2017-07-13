﻿using System.Text;
using Newtonsoft.Json;
using Util.Helpers;
using Util.Tests.Samples;
using Util.Tests.XUnitHelpers;
using Xunit;

namespace Util.Tests.Helpers {
    /// <summary>
    /// Json测试
    /// </summary>
    public class JsonTest {
        /// <summary>
        /// 测试循环引用序列化
        /// </summary>
        [Fact]
        public void TestLoop() {
            A a = new A { Name = "a" };
            B b = new B { Name = "b" };
            C c = new C { Name = "c" };
            a.B = b;
            b.C = c;
            c.A = a;
            AssertHelper.Throws<JsonSerializationException>( () => Json.ToJson( c ) );
        }

        /// <summary>
        /// 转成Json,验证空
        /// </summary>
        [Fact]
        public void TestToJson_Null() {
            Assert.Equal( "{}", Json.ToJson( null ) );
        }

        /// <summary>
        /// 测试转成Json
        /// </summary>
        [Fact]
        public void TestToJson() {
            var result = new StringBuilder();
            result.Append( "{" );
            result.Append( "\"Name\":\"a\"," );
            result.Append( "\"nickname\":\"b\"," );
            result.Append( "\"Value\":null," );
            result.Append( "\"Date\":\"2012/1/1 0:00:00\"," );
            result.Append( "\"Age\":1," );
            result.Append( "\"isShow\":true" );
            result.Append( "}" );
            Assert.Equal( result.ToString(), Json.ToJson( JsonTestSample.Create() ) );
        }

        /// <summary>
        /// 测试转成对象
        /// </summary>
        [Fact]
        public void TestToObject() {
            var customer = Json.ToObject<JsonTestSample>( $"{{\"Name\":\"a\"}}" );
            Assert.Equal( "a", customer.Name );
        }
    }
}
