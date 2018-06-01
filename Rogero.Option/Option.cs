using System;
using System.Collections;
using System.Collections.Generic;

namespace Rogero.Options
{
    public class Option<T> : IEnumerable<T>
    {
        #region Equals Implementation

        protected bool Equals(Option<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Option<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public static bool operator ==(Option<T> left, Option<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Option<T> left, Option<T> right)
        {
            return !Equals(left, right);
        }

        #endregion

        public T Value { get; private set; }
        public bool HasValue => this != None;
        public bool HasNoValue => this == None;

        public static readonly Option<T> None = new Option<T>(default(T));

        public static Option<T> Some(T value)
        {
            return new Option<T>(value);
        }

        private Option(T value)
        {
            Value = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (HasValue)
                yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public static implicit operator Option<T>(T obj)
        {
            if (Equals(obj, default(T)))
                return None;
            else
                return new Option<T>(obj);
        }

        public static implicit operator T(Option<T> option)
        {
            if (option.HasValue)
            {
                return option.Value;
            }
            else
            {
                return default(T);
            }
        }
    }
}