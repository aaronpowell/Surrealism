using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Surrealism.Tests
{
    [TestFixture]
    class SurealTests
    {
        [Test]
        public void DefaultConstructor_WillBeZero()
        {
            var s = new Surreal();

            Assert.That(s.IsZero(), Is.EqualTo(true));
        }

        [TestCase(1)]
        [TestCase(10)]
        public void PositiveNumberToCtor_WillNotBeZero(int num)
        {
            var s = new Surreal(num);

            Assert.That(s.IsZero(), Is.EqualTo(false));
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void NegativeNumberToCtor_WillNotBeZero(int num)
        {
            var s = new Surreal(num);

            Assert.That(s.IsZero(), Is.EqualTo(false));
        }

        [TestCase(1)]
        [TestCase(10)]
        public void PositiveNumber_WillBePositive(int num)
        {
            var s = new Surreal(num);
            Assert.That(s.IsPositive(), Is.EqualTo(true));
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void NegativeNumber_WillBePositive(int num)
        {
            var s = new Surreal(num);
            Assert.That(s.IsNegative(), Is.EqualTo(true));
        }

        [TestCase(1, 1)]
        [TestCase(10, 10)]
        [TestCase(-1, -1)]
        [TestCase(-10, -10)]
        public void MatchingRealNumbers_ShouldMatchSurreal(int first, int second)
        {
            var s1 = new Surreal(first);
            var s2 = new Surreal(second);

            Assert.That(s1.IsEqualTo(s2), Is.EqualTo(true));
        }

        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(10, 20)]
        [TestCase(-1, 1)]
        [TestCase(-10, -1)]
        public void FirstNumber_LessThanOrEqualTo_SecondNumber(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            Assert.That(s1.IsLessThanOrEqualTo(s2), Is.EqualTo(true));
        }

        [TestCase(1, 2)]
        [TestCase(10, 20)]
        [TestCase(-1, 1)]
        [TestCase(-10, -1)]
        public void FirstNumber_LessThan_SecondNumber(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            Assert.That(s1.IsLessThan(s2), Is.EqualTo(true));
        }

        [TestCase(2, 1)]
        [TestCase(2, 2)]
        [TestCase(20, 10)]
        [TestCase(1, -1)]
        [TestCase(-1, -10)]
        public void FirstNumber_GreaterThanOrEqualTo_SecondNumber(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            Assert.That(s1.IsGreaterThanOrEqualTo(s2), Is.EqualTo(true));
        }

        [TestCase(2, 1)]
        [TestCase(20, 10)]
        [TestCase(1, -1)]
        [TestCase(-1, -10)]
        public void FirstNumber_GreaterThan_SecondNumber(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            Assert.That(s1.IsGreaterThan(s2), Is.EqualTo(true));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(-1)]
        [TestCase(-10)]
        public void SurrealNumber_CanBeConvertedToRealNumber(int num)
        {
            var s = new Surreal(num);

            Assert.That(s.Real(), Is.EqualTo(num));
        }

        [TestCase(0, 1)]
        [TestCase(1, 10)]
        [TestCase(0, -1)]
        [TestCase(-10, -1)]
        [TestCase(1, -1)]
        [TestCase(0, -10)]
        public void CanAddTwoNumbers(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            var result = s1 + s2;

            Assert.That(result.Real(), Is.EqualTo(a + b));
        }

        [TestCase(0, 1)]
        [TestCase(1, 10)]
        [TestCase(0, -1)]
        [TestCase(-10, -1)]
        [TestCase(1, -1)]
        [TestCase(0, -10)]
        public void CanSubtractTwoNumbers(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            var result = s1 - s2;

            Assert.That(result.Real(), Is.EqualTo(a - b));
        }

        [TestCase(2, 10)]
        [TestCase(0, -1)]
        [TestCase(-10, -1)]
        [TestCase(1, -1)]
        public void CanMultiplyTwoNumbers(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            var result = s1 * s2;

            Assert.That(result.Real(), Is.EqualTo(a * b));
        }

        [TestCase(10, 2)]
        [TestCase(9, 3)]
        [TestCase(9, -3)]
        [TestCase(-9, -3)]
        public void CanDivideTwoNumbers(int a, int b)
        {
            var s1 = new Surreal(a);
            var s2 = new Surreal(b);

            var result = s1 / s2;

            Assert.That(result.Real(), Is.EqualTo(a / b));
        }

        [Test]
        public void ZeroDenominator_WillThrowException()
        {
            var numerator = new Surreal(42);
            var denominator = new Surreal();

            Assert.IsTrue(denominator.IsZero());
            Assert.Throws<DivideByZeroException>(() =>
            {
                var x = numerator / denominator;
            });
        }

        [Test]
        public void ZeroNumerator_WillReturnZero()
        {
            var numerator = new Surreal();
            var realDenominator = 42;
            var denominator = new Surreal(realDenominator);

            var result = numerator / denominator;

            Assert.IsTrue(numerator.IsZero());
            Assert.IsTrue(result.IsZero(), "0 / 42 should be zero");
        }

        [TestCase(1)]
        [TestCase(-1)]
        public void IncrementOperator_WillIncrementTheRealValue(int a)
        {
            var s = new Surreal(a);

            var result = s++;
            var expected = a++;

            Assert.That(result.Real(), Is.EqualTo(expected));
        }

        [TestCase(1)]
        [TestCase(-1)]
        public void DecrementOperator_WillIncrementTheRealValue(int a)
        {
            var s = new Surreal(a);

            var result = s--;
            var expected = a--;

            Assert.That(result.Real(), Is.EqualTo(expected));
        }
    }
}
