using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogero.Options;
using Shouldly;
using Xunit;

namespace Rogero.Option.Tests
{
    public class FlatteningTests
    {
        private object _object = new object();

        [Fact()]
        [Trait("Category", "Instant")]
        public void TestSingleFlattenValue()
        {
            var outerOption = _object.ToOption();
            var innerOption = outerOption.ToOption();
            var result = innerOption.Flatten();
            result.ShouldBe(outerOption);
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void TestSingleFlattenNull()
        {
            var outerOption = ((object)null).ToOption();
            var innerOption = outerOption.ToOption();
            var result = innerOption.Flatten();
            result.ShouldBe(Option<object>.None);
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void TestSingleFlattenOptionNull()
        {
            var outerOption = ((Option<object>) null);
            var innerOption = outerOption.ToOption();
            var result = innerOption.Flatten();
            result.ShouldBe(Option<object>.None);
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void TestDoubleFlattenValue()
        {
            var outerOption = _object.ToOption();
            var middleOption = outerOption.ToOption();
            var innerOption = middleOption.ToOption();
            var result = innerOption.Flatten();
            result.ShouldBe(outerOption);
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void TestDoubleFlattenNull()
        {
            var outerOption = ((object)null).ToOption();
            var middleOption = outerOption.ToOption();
            var innerOption = middleOption.ToOption();
            var result = innerOption.Flatten();
            result.ShouldBe(Option<object>.None);
        }

        [Fact()]
        [Trait("Category", "Instant")]
        public void TestDoubleFlattenOuterOptionNull()
        {
            var option = ((Option<Option<object>>) null);
            var innerOption = option.ToOption();
            var result = innerOption.Flatten();
            result.ShouldBe(Option<object>.None);
        }
    }
}
