using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace Rogero.Options.Tests
{
    public class ToOptionTests
    {
        [Fact()]
        [Trait("Category", "Instant")]
        public void NullToNone()
        {
            object o = null;
            var nullOption = o.ToOption();
            Assert.True(nullOption.HasValue == false);
            Assert.True(nullOption == o);
            Assert.True(o == nullOption);
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void NotNullToSome()
        {
            object o = new object();
            var notNullOption = o.ToOption();
            Assert.True(notNullOption.HasValue == true);
            Assert.True(notNullOption == o);
            Assert.True(o == notNullOption);
        }
    }

    public class OptionEqualsTests
    {
        [Fact()]
        [Trait("Category", "Instant")]
        public void NonEqual()
        {
            object o1 = new object();
            object o2 = new object();
            Assert.False(o1 == o2);

            var option1 = o1.ToOption();
            var option2 = o2.ToOption();
            Assert.False(option1 == option2);
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void Equal()
        {
            object o1 = new object();
            Assert.True(o1 == o1);

            var option1 = o1.ToOption();
            var option2 = o1.ToOption();
            Assert.True(option1 == option2);
        }
    }

    public class OptionTrySelectTests
    {
        private class TestClass
        {
            public string Name { get; set; }
        }
        
        [Fact()]
        [Trait("Category", "Instant")]
        public void SimpleHasValue()
        {
            var sut = new TestClass() {Name = "John"};
            var result = sut.ToOption().TrySelect(z => z.Name);

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe("John");
        }
        
        [Fact()]
        [Trait("Category", "Instant")]
        public void SimpleHasNoValue()
        {
            Option<TestClass> sut = Option<TestClass>.None;
            var result = sut.TrySelect(z => z.Name);

            result.HasValue.ShouldBeFalse();
            result.HasNoValue.ShouldBeTrue();
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void SimpleNonNullList()
        {
            var list = new List<string>() {"1", "2"};
            var optionList = list.ToOption();
            var intList = optionList.TrySelect(int.Parse);

            var total = intList.Value.Sum(z => z);
            Assert.True(total == 3);
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void SimpleNullList()
        {
            List<string> list = null;
            var optionList = list.ToOption();
            var intList = optionList.TrySelect(int.Parse);

            Assert.True(intList.HasNoValue);

            var set = new HashSet<int>();
        }
    }
}
