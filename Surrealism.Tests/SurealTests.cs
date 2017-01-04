using System;
using Xunit;

namespace Surrealism.Tests
{
    public class SurealTests
    {
        [Fact]
        public void DefaultConstructor_WillBeZero()
        {
            var s = new Surreal();

            Assert.Equal(s.IsZero(), true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void PositiveNumberToCtor_WillNotBeZero(int num)
        {
            var s = new Surreal(num);

            Assert.Equal(s.IsZero(), false);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void NegativeNumberToCtor_WillNotBeZero(int num)
        {
            var s = new Surreal(num);

            Assert.Equal(s.IsZero(), false);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void PositiveNumber_WillBePositive(int num)
        {
            var s = new Surreal(num);
            Assert.Equal(s.IsPositive(), true);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void NegativeNumber_WillBePositive(int num)
        {
            var s = new Surreal(num);
            Assert.Equal(s.IsNegative(), true);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(10, 10)]
        [InlineData(-1, -1)]
        [InlineData(-10, -10)]
        public void MatchingRealNumbers_ShouldMatchSurreal(int first, int second)
        {
            var s1 = new Surreal(first);
            var s2 = new Surreal(second);

            Assert.Equal(s1.IsEqualTo(s2), true);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(10, 20)]
        [InlineData(-1, 1)]
        [InlineData(-10, -1)]
        public void FirstNumber_LessThanOrEqualTo_SecondNumber(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            Assert.Equal(s1.IsLessThanOrEqualTo(s2), true);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(10, 20)]
        [InlineData(-1, 1)]
        [InlineData(-10, -1)]
        public void FirstNumber_LessThan_SecondNumber(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            Assert.Equal(s1.IsLessThan(s2), true);
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(20, 10)]
        [InlineData(1, -1)]
        [InlineData(-1, -10)]
        public void FirstNumber_GreaterThanOrEqualTo_SecondNumber(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            Assert.Equal(s1.IsGreaterThanOrEqualTo(s2), true);
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(20, 10)]
        [InlineData(1, -1)]
        [InlineData(-1, -10)]
        public void FirstNumber_GreaterThan_SecondNumber(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            Assert.Equal(s1.IsGreaterThan(s2), true);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void SurrealNumber_CanBeConvertedToRealNumber(int num)
        {
            var s = new Surreal(num);

            Assert.Equal(s.Real(), num);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 10)]
        [InlineData(0, -1)]
        [InlineData(-10, -1)]
        [InlineData(1, -1)]
        [InlineData(0, -10)]
        public void CanAddTwoNumbers(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            var result = s1 + s2;

            Assert.Equal(result.Real(), a + b);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 10)]
        [InlineData(0, -1)]
        [InlineData(-10, -1)]
        [InlineData(1, -1)]
        [InlineData(0, -10)]
        public void CanSubtractTwoNumbers(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            var result = s1 - s2;

            Assert.Equal(result.Real(), a - b);
        }

        [Theory]
        [InlineData(2, 10)]
        [InlineData(0, -1)]
        [InlineData(-10, -1)]
        [InlineData(1, -1)]
        public void CanMultiplyTwoNumbers(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            var result = s1 * s2;

            Assert.Equal(result.Real(), a * b);
        }

        [Theory]
        [InlineData(10, 2)]
        [InlineData(9, 3)]
        [InlineData(9, -3)]
        [InlineData(-9, -3)]
        public void CanDivideTwoNumbers(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            var result = s1 / s2;

            Assert.Equal(result.Real(), a / b);
        }

        [Fact]
        public void ZeroDenominator_WillThrowException()
        {
            var numerator = new Surreal(42);
            var denominator = new Surreal();

            Assert.True(denominator.IsZero());
            Assert.Throws<DivideByZeroException>(() =>
            {
                var x = numerator / denominator;
            });
        }

        [Fact]
        public void ZeroNumerator_WillReturnZero()
        {
            var numerator = new Surreal();
            var realDenominator = 42;
            var denominator = new Surreal(realDenominator);

            var result = numerator / denominator;

            Assert.True(numerator.IsZero());
            Assert.True(result.IsZero(), "0 / 42 should be zero");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public void IncrementOperator_WillIncrementTheRealValue(int a)
        {
            var s = new Surreal(a);

            var result = s++;
            var expected = a++;

            Assert.Equal(result.Real(), expected);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public void DecrementOperator_WillIncrementTheRealValue(int a)
        {
            var s = new Surreal(a);

            var result = s--;
            var expected = a--;

            Assert.Equal(result.Real(), expected);
        }
    }
}
