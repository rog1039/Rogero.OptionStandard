using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogero.Options
{
    public static class OptionExtensions
    {
        public static Option<T> ToOption<T>(this T o) where T : class
        {
            return o == null
                ? Option<T>.None
                : Option<T>.Some(o);
        }

        public static T? ToNullable<T>(this Option<T> o) where T : struct
        {
            return o.HasValue
                ? o.Value
                : (T?) null;
        }

        public static Option<T> DoIfValue<T>(this Option<T> source, Action<T> action)
        {
            if (source.HasValue)
            {
                action(source.Value);
            }
            return source;
        }

        public static TRes Match<T,TRes>(this Option<T> option, Func<T, TRes> some, Func<TRes> none)
        {
            if (option.HasValue)
                return some.Invoke(option.Value);
            else
                return none.Invoke();
        }

        public static void Match<T>(this Option<T> option, Action<T> some, Action none)
        {
            if (option.HasValue)
                some.Invoke(option.Value);
            else
                none.Invoke();
        }

        /// <summary>
        /// Returns the value if the Option contains one, otherwise, returns the provided value.
        /// </summary>
        /// <typeparam name="T">g</typeparam>
        /// <param name="option"></param>
        /// <param name="otherValue">The value to return if the Option does not have a value.</param>
        /// <returns></returns>
        public static T ToValueOr<T>(this Option<T> option, T otherValue)
        {
            return option.HasValue
                ? option.Value
                : otherValue;
        }

        public static Option<TResult> TrySelect<T, TResult>(this Option<T> option, Func<T, TResult> transform)
        {
            if (option.HasValue)
            {
                return transform(option.Value);
            }

            return Option<TResult>.None;
        }

        public static Option<IEnumerable<TResult>> TrySelect<T, TResult>(this Option<List<T>> optionList, Func<T, TResult> func)
        {
            if (optionList.HasValue)
                return optionList.Value.Select(func).ToOption();
            return Option<IEnumerable<TResult>>.None;
        }

        public static Option<IEnumerable<TResult>> TrySelect<T, TResult>(this Option<IList<T>> optionList, Func<T, TResult> func)
        {
            if (optionList.HasValue)
                return optionList.Value.Select(func).ToOption();
            return Option<IEnumerable<TResult>>.None;
        }

        public static Option<IEnumerable<TResult>> TrySelect<T, TResult>(this Option<IEnumerable<T>> optionList, Func<T, TResult> func)
        {
            if (optionList.HasValue)
                return optionList.Value.Select(func).ToOption();
            return Option<IEnumerable<TResult>>.None;
        }

        public static Option<TResult> SelectToOption<T, TResult>(this Option<T> option, Func<Option<T>, Option<TResult>> func)
        {
            if(option.HasValue)
            {
                var result = func(option);
                return result;
            }
            else
            {
                return Option<TResult>.None;
            }
        }

        public static Option<T> Flatten<T>(this Option<Option<T>> option)
        {
            if(option.HasValue)
                return option.Value;
            else
                return Option<T>.None;
        }

        public static Option<T> Flatten<T>(this Option<Option<Option<T>>> option)
        {
            if(option.HasValue)
            {
                return option.Value.Flatten();
            }
            else
                return Option<T>.None;
        }
    }
}
