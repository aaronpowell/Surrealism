using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surrealism
{
    public class Surreal
    {
        private static Func<dynamic> zero = () => new[] { new object[0], new object[0] };
        private static Func<Surreal, dynamic> L = s => s.value.ElementAt(0);
        private static Func<Surreal, dynamic> R = s => s.value.ElementAt(1);

        private static int Size(dynamic s)
        {
            var i = 0;
            while (s != null)
            {
                i++;
                s = s.Length == 0 ? null : s[0];
            }

            return i;
        }

        internal IEnumerable<object> value;

        public Surreal()
        {
            value = zero();
        }

        public Surreal(int num)
            : this()
        {
            if (num == 0)
                return;

            if (num > 0)
                for (int i = 0; i < num; i++)
                    this.Increment();
            else
                for (int i = num; i < 0; i++)
                    this.Decrement();
        }

        private Surreal(IEnumerable<object> surreal)
        {
            value = surreal;
        }

        private void Increment()
        {
            this.value = this.Successor().value;
        }

        private void Decrement()
        {
            this.value = this.Predecessor().value;
        }

        private Surreal Successor()
        {
            if (this.IsZero() || this.IsPositive())
                return new Surreal(new[] { this.value, new object[0] });
            return new Surreal(R(this));
        }

        private Surreal Predecessor()
        {
            if (this.IsZero() || this.IsNegative())
                return new Surreal(new[] { new object[0], this.value });
            return new Surreal(L(this));
        }

        public bool IsZero()
        {
            return L(this).Length == 0 && R(this).Length == 0;
        }

        public bool IsPositive()
        {
            return Size(L(this)) > Size(R(this));
        }

        public bool IsNegative()
        {
            return Size(L(this)) < Size(R(this));
        }

        public bool IsEqualTo(Surreal to)
        {
            return this.IsLessThanOrEqualTo(to) && this.IsGreaterThanOrEqualTo(to);
        }

        public bool IsLessThanOrEqualTo(Surreal to)
        {
            var me = new Surreal(this.value);
            var them = new Surreal(to.value);

            if ((me.IsNegative() || me.IsZero()) && (them.IsPositive() || them.IsZero()))
                return true;

            if ((me.IsPositive() || me.IsZero()) && (them.IsNegative() || them.IsZero()))
                return false;

            if (me.IsPositive() && them.IsPositive())
            {
                while (!me.IsZero() && !them.IsZero())
                {
                    me.Decrement();
                    them.Decrement();
                }
                return me.IsZero();
            }

            if (me.IsNegative() && them.IsNegative())
            {
                while (!me.IsZero() && !them.IsZero())
                {
                    me.Increment();
                    them.Increment();
                }
                return them.IsZero();
            }

            return false;
        }

        public bool IsGreaterThanOrEqualTo(Surreal to)
        {
            return to.IsLessThanOrEqualTo(this);
        }

        public bool IsLessThan(Surreal s2)
        {
            return !IsGreaterThanOrEqualTo(s2);
        }

        public bool IsGreaterThan(Surreal s2)
        {
            return !IsLessThanOrEqualTo(s2);
        }
    }
}
