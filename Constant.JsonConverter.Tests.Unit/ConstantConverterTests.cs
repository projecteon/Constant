using Newtonsoft.Json;
using Xunit;

namespace Constant.JsonConverter.Tests.Unit
{
    public class ConstantConverterTests
    {
        [Fact]
        public void GivenObjectHasSmartEnumProperty_WhenSerializingObjectWithConverter_SmartEnumIsSerializedCorrectly()
        {
            var expected = "{\"SmartEnum\":\"1\"}";
            var theObject = new TestObject() { SmartEnum = SmartEnum.FirstEnum };

            var json = JsonConvert.SerializeObject(theObject, new ConstantConverter<string, SmartEnum>());

            Assert.Equal(expected, json);
        }
        
        [Fact]
        public void GivenObjectHasSmartEnumProperty_WhenSeSerializingObjectWithConverter_SmartEnumIsDeSerializedCorrectly() {
            var theObject = new TestObject() { SmartEnum = SmartEnum.FirstEnum };
            var json = JsonConvert.SerializeObject(theObject, new ConstantConverter<string, SmartEnum>());

            var result = JsonConvert.DeserializeObject<TestObject>(json, new ConstantConverter<string, SmartEnum>());

            Assert.Equal(result.SmartEnum, SmartEnum.FirstEnum);
        }

        class SmartEnum : Constant<string, SmartEnum> {
            public static SmartEnum FirstEnum = new SmartEnum("1");

            public SmartEnum(string key) : base(key) {
            }

            public string Tag => this.Key;
        }

        class TestObject {
            public SmartEnum SmartEnum { get; set; }

        }
    }
}
