using System.Collections.Generic;

namespace Constant.Tests.Unit
{
    using Xunit;

    public class ConstantTests
    {
        [Fact]
        public void GivenKeyDoesNotExistInConstants_WhenGetOrDefaultForIsCalled_DefaultConstantIsReturned()
        {
            var constant = TestableConstant.GetOrDefaultFor("notExistsingKey");

            Assert.Equal(TestableConstant.Default, constant);
        }

        [Fact]
        public void GivenKeyDoesExistInConstants_WhenGetOrDefaultForIsCalled_TheConstantIsReturned()
        {
            var constant = TestableConstant.GetOrDefaultFor("constant1");

            Assert.Equal(TestableConstant.Constant1, constant);
        }

        [Fact]
        public void GivenKeyDoesExistInConstants_WhenGetForIsCalled_TheConstantIsReturned()
        {
            var constant = TestableConstant.GetFor("constant1");

            Assert.Equal(TestableConstant.Constant1, constant);
        }

        [Fact]
        public void GivenKeyDoesNotExistInConstants_WhenGetForIsCalled_NullIsReturned()
        {
            var constant = TestableConstant.GetFor("notExistsingKey");

            Assert.Equal(null, constant);
        }

        [Fact]
        public void WhenGetAllIsCalled_AllConstantsAreReturned()
        {
            var constants = TestableConstant.GetAll();

            Assert.Equal(new List<TestableConstant>{ TestableConstant.Default, TestableConstant.Constant1}, constants);
        }

        private class TestableConstant : Constant<string, TestableConstant>
        {
            [DefaultKey]
            public static readonly TestableConstant Default = new TestableConstant("default");

            public static readonly TestableConstant Constant1 = new TestableConstant("constant1");

            private TestableConstant(string key): base(key)
            {
            }
        }
    }
}