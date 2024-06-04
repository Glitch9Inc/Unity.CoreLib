using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Glitch9
{
    public class Ranged<T> where T : IComparable<T>
    {
        public static implicit operator T(Ranged<T> ranged) => ranged.Value;

        [JsonIgnore] private T _value;

        public T Min { get; }
        public T Max { get; }
        public T Value
        {
            get => _value;
            set
            {
                if (value.CompareTo(Min) < 0) _value = Min;
                else if (value.CompareTo(Max) > 0) _value = Max;
                else _value = value;
            }
        }

        public Ranged(T value, T min, T max)
        {
            if (min.CompareTo(max) > 0)
            {
                throw new ArgumentException($"'{nameof(min)}' cannot be greater than '{nameof(max)}'.");
            }

            Min = min;
            Max = max;
            _value = min; // 초기화를 Min 값으로 설정
            Value = value; // This ensures the value is within the bounds
        }

        public Ranged(T min, T max) : this(min, min, max) { } // 기본값을 min으로 설정

        // equal check
        public static bool operator ==(Ranged<T> ranged1, Ranged<T> ranged2)
            => ranged1 is null && ranged2 is null
               || ranged1 is not null && ranged1.Equals(ranged2);

        public static bool operator !=(Ranged<T> ranged1, Ranged<T> ranged2)
            => !(ranged1 == ranged2);

        public static bool operator <(Ranged<T> ranged1, Ranged<T> ranged2) => ranged1.Value.CompareTo(ranged2.Value) < 0;
        public static bool operator >(Ranged<T> ranged1, Ranged<T> ranged2) => ranged1.Value.CompareTo(ranged2.Value) > 0;
        public static bool operator <=(Ranged<T> ranged1, Ranged<T> ranged2) => ranged1.Value.CompareTo(ranged2.Value) <= 0;
        public static bool operator >=(Ranged<T> ranged1, Ranged<T> ranged2) => ranged1.Value.CompareTo(ranged2.Value) >= 0;

        protected bool Equals(Ranged<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value) && EqualityComparer<T>.Default.Equals(Min, other.Min) && EqualityComparer<T>.Default.Equals(Max, other.Max);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Ranged<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Min, Max);
        }
    }


    public static class RangedExtensions
    {
        public static Ranged<int> Add(this Ranged<int> ranged, int value)
        {
            int newValue = ranged.Value + value;
            return new Ranged<int>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<float> Add(this Ranged<float> ranged, float value)
        {
            float newValue = ranged.Value + value;
            return new Ranged<float>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<double> Add(this Ranged<double> ranged, double value)
        {
            double newValue = ranged.Value + value;
            return new Ranged<double>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<int> Subtract(this Ranged<int> ranged, int value)
        {
            int newValue = ranged.Value - value;
            return new Ranged<int>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<float> Subtract(this Ranged<float> ranged, float value)
        {
            float newValue = ranged.Value - value;
            return new Ranged<float>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<double> Subtract(this Ranged<double> ranged, double value)
        {
            double newValue = ranged.Value - value;
            return new Ranged<double>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<int> Multiply(this Ranged<int> ranged, int value)
        {
            int newValue = ranged.Value * value;
            return new Ranged<int>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<float> Multiply(this Ranged<float> ranged, float value)
        {
            float newValue = ranged.Value * value;
            return new Ranged<float>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<double> Multiply(this Ranged<double> ranged, double value)
        {
            double newValue = ranged.Value * value;
            return new Ranged<double>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<int> Divide(this Ranged<int> ranged, int value)
        {
            int newValue = ranged.Value / value;
            return new Ranged<int>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<float> Divide(this Ranged<float> ranged, float value)
        {
            float newValue = ranged.Value / value;
            return new Ranged<float>(ranged.Min, ranged.Max, newValue);
        }

        public static Ranged<double> Divide(this Ranged<double> ranged, double value)
        {
            double newValue = ranged.Value / value;
            return new Ranged<double>(ranged.Min, ranged.Max, newValue);
        }
    }
}