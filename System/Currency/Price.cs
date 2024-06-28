using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Glitch9
{
    [Serializable, JsonConverter(typeof(PriceConverter))]
    public class Price : IComparable
    {

        [SerializeField] private float priceInUsd;
        [SerializeField] private CurrencyCode currencyCode;
        [SerializeField] private bool isEstimate;

        public float PriceInUsd
        {
            get => priceInUsd;
            set => priceInUsd = value;
        }

        public CurrencyCode CurrencyCode
        {
            get => currencyCode;
            set
            {
                if (currencyCode == value) return;
                currencyCode = value;

                if (_value == 0) return;
                ApplyRate();
            }
        }

        public bool IsEstimate
        {
            get => isEstimate;
            set => isEstimate = value;
        }

        public float Value
        {
            get
            {
                if (_value == null) ApplyRate();
                return _value ?? 0;
            }
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_value == value) return;
                if (currencyCode == CurrencyCode.USD)
                {
                    priceInUsd = value;
                    _value = value;
                    return;
                }

                CurrencyConverter currencyConverter = new();
                priceInUsd = currencyConverter.Convert(value, currencyCode, CurrencyCode.USD);
                _value = value;
            }
        }
        private float? _value;

        public Price()
        {
            CurrencyCode = CurrencyCode.USD;
        }

        public Price(float priceInUsd, CurrencyCode currencyCode = CurrencyCode.USD, bool isEstimate = false)
        {
            this.priceInUsd = priceInUsd;
            this.currencyCode = currencyCode;
            this.isEstimate = isEstimate;
        }

        private void ApplyRate()
        {
            if (currencyCode == CurrencyCode.USD)
            {
                _value = priceInUsd;
                return;
            }

            CurrencyConverter currencyConverter = new();
            _value = currencyConverter.Convert(priceInUsd, CurrencyCode.USD, currencyCode);
        }

        // operators
        public static implicit operator float(Price price) => price?.priceInUsd ?? 0;
        public static implicit operator Price(float value) => new(value);
        public static implicit operator Price(int value) => new(value);
        public static implicit operator Price(double value) => new((float)value);
        public static implicit operator Price(decimal value) => new((float)value);
        public static implicit operator (float value, bool isEstimate)(Price price) => (price?.priceInUsd ?? 0, price?.isEstimate ?? false);

        public override string ToString() => isEstimate ? $"${priceInUsd} {currencyCode} (estimate)" : $"${priceInUsd} {currencyCode}";
        public int CompareTo(object obj) => obj is Price price ? priceInUsd.CompareTo(price?.priceInUsd ?? 0) : priceInUsd.CompareTo(obj);

        public static Price operator +(Price a, Price b) => new(a?.priceInUsd ?? 0 + b?.priceInUsd ?? 0);
        public static Price operator -(Price a, Price b) => new(a?.priceInUsd ?? 0 - b?.priceInUsd ?? 0);
        public static Price operator *(Price a, Price b) => new(a?.priceInUsd ?? 0 * b?.priceInUsd ?? 0);
        public static Price operator /(Price a, Price b) => new(a?.priceInUsd ?? 0 / b?.priceInUsd ?? 0);
        public static Price operator %(Price a, Price b) => new(a?.priceInUsd ?? 0 % b?.priceInUsd ?? 0);

        public static bool operator ==(Price a, Price b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return Math.Abs(a.priceInUsd - b.priceInUsd) < Tolerance.FLOAT && a.isEstimate == b.isEstimate;
        }

        public static bool operator !=(Price a, Price b) => !(a == b);
        public static bool operator >(Price a, Price b) => (a?.priceInUsd ?? 0) > (b?.priceInUsd ?? 0);
        public static bool operator <(Price a, Price b) => (a?.priceInUsd ?? 0) < (b?.priceInUsd ?? 0);
        public static bool operator >=(Price a, Price b) => (a?.priceInUsd ?? 0) >= (b?.priceInUsd ?? 0);
        public static bool operator <=(Price a, Price b) => (a?.priceInUsd ?? 0) <= (b?.priceInUsd ?? 0);

        public override bool Equals(object obj) => obj is Price price && this == price;
        public override int GetHashCode() => HashCode.Combine(priceInUsd, isEstimate);

        #region float 
        public static Price operator +(Price a, float b) => new((a?.priceInUsd ?? 0) + b);
        public static Price operator -(Price a, float b) => new((a?.priceInUsd ?? 0) - b);
        public static Price operator *(Price a, float b) => new((a?.priceInUsd ?? 0) * b);
        public static Price operator /(Price a, float b) => new((a?.priceInUsd ?? 0) / b);
        public static Price operator %(Price a, float b) => new((a?.priceInUsd ?? 0) % b);

        public static bool operator ==(Price a, float b) => Math.Abs((a?.priceInUsd ?? 0) - b) < Tolerance.FLOAT;
        public static bool operator !=(Price a, float b) => Math.Abs((a?.priceInUsd ?? 0) - b) > Tolerance.FLOAT;
        public static bool operator >(Price a, float b) => (a?.priceInUsd ?? 0) > b;
        public static bool operator <(Price a, float b) => (a?.priceInUsd ?? 0) < b;
        public static bool operator >=(Price a, float b) => (a?.priceInUsd ?? 0) >= b;
        public static bool operator <=(Price a, float b) => (a?.priceInUsd ?? 0) <= b;

        public static Price operator +(float a, Price b) => new(a + (b?.priceInUsd ?? 0));
        public static Price operator -(float a, Price b) => new(a - (b?.priceInUsd ?? 0));
        public static Price operator *(float a, Price b) => new(a * (b?.priceInUsd ?? 0));
        public static Price operator /(float a, Price b) => new(a / (b?.priceInUsd ?? 0));
        public static Price operator %(float a, Price b) => new(a % (b?.priceInUsd ?? 0));

        public static bool operator ==(float a, Price b) => Math.Abs(a - (b?.priceInUsd ?? 0)) < Tolerance.FLOAT;
        public static bool operator !=(float a, Price b) => Math.Abs(a - (b?.priceInUsd ?? 0)) > Tolerance.FLOAT;
        public static bool operator >(float a, Price b) => a > (b?.priceInUsd ?? 0);
        public static bool operator <(float a, Price b) => a < (b?.priceInUsd ?? 0);
        public static bool operator >=(float a, Price b) => a >= (b?.priceInUsd ?? 0);
        public static bool operator <=(float a, Price b) => a <= (b?.priceInUsd ?? 0);
        #endregion

        #region int
        public static Price operator +(Price a, int b) => new((a?.priceInUsd ?? 0) + b);
        public static Price operator -(Price a, int b) => new((a?.priceInUsd ?? 0) - b);
        public static Price operator *(Price a, int b) => new((a?.priceInUsd ?? 0) * b);
        public static Price operator /(Price a, int b) => new((a?.priceInUsd ?? 0) / b);
        public static Price operator %(Price a, int b) => new((a?.priceInUsd ?? 0) % b);

        public static bool operator ==(Price a, int b) => Math.Abs((a?.priceInUsd ?? 0) - b) < Tolerance.FLOAT;
        public static bool operator !=(Price a, int b) => Math.Abs((a?.priceInUsd ?? 0) - b) > Tolerance.FLOAT;
        public static bool operator >(Price a, int b) => (a?.priceInUsd ?? 0) > b;
        public static bool operator <(Price a, int b) => (a?.priceInUsd ?? 0) < b;
        public static bool operator >=(Price a, int b) => (a?.priceInUsd ?? 0) >= b;
        public static bool operator <=(Price a, int b) => (a?.priceInUsd ?? 0) <= b;

        public static Price operator +(int a, Price b) => new(a + (b?.priceInUsd ?? 0));
        public static Price operator -(int a, Price b) => new(a - (b?.priceInUsd ?? 0));
        public static Price operator *(int a, Price b) => new(a * (b?.priceInUsd ?? 0));
        public static Price operator /(int a, Price b) => new(a / (b?.priceInUsd ?? 0));
        public static Price operator %(int a, Price b) => new(a % (b?.priceInUsd ?? 0));

        public static bool operator ==(int a, Price b) => Math.Abs(a - (b?.priceInUsd ?? 0)) < Tolerance.FLOAT;
        public static bool operator !=(int a, Price b) => Math.Abs(a - (b?.priceInUsd ?? 0)) > Tolerance.FLOAT;
        public static bool operator >(int a, Price b) => a > (b?.priceInUsd ?? 0);
        public static bool operator <(int a, Price b) => a < (b?.priceInUsd ?? 0);
        public static bool operator >=(int a, Price b) => a >= (b?.priceInUsd ?? 0);
        public static bool operator <=(int a, Price b) => a <= (b?.priceInUsd ?? 0);
        #endregion

        #region double
        public static Price operator +(Price a, double b) => new((a?.priceInUsd ?? 0) + (float)b);
        public static Price operator -(Price a, double b) => new((a?.priceInUsd ?? 0) - (float)b);
        public static Price operator *(Price a, double b) => new((a?.priceInUsd ?? 0) * (float)b);
        public static Price operator /(Price a, double b) => new((a?.priceInUsd ?? 0) / (float)b);
        public static Price operator %(Price a, double b) => new((a?.priceInUsd ?? 0) % (float)b);

        public static bool operator ==(Price a, double b) => Math.Abs((a?.priceInUsd ?? 0) - (float)b) < Tolerance.FLOAT;
        public static bool operator !=(Price a, double b) => Math.Abs((a?.priceInUsd ?? 0) - (float)b) > Tolerance.FLOAT;
        public static bool operator >(Price a, double b) => (a?.priceInUsd ?? 0) > (float)b;
        public static bool operator <(Price a, double b) => (a?.priceInUsd ?? 0) < (float)b;
        public static bool operator >=(Price a, double b) => (a?.priceInUsd ?? 0) >= (float)b;
        public static bool operator <=(Price a, double b) => (a?.priceInUsd ?? 0) <= (float)b;

        public static Price operator +(double a, Price b) => new((float)a + (b?.priceInUsd ?? 0));
        public static Price operator -(double a, Price b) => new((float)a - (b?.priceInUsd ?? 0));
        public static Price operator *(double a, Price b) => new((float)a * (b?.priceInUsd ?? 0));
        public static Price operator /(double a, Price b) => new((float)a / (b?.priceInUsd ?? 0));
        public static Price operator %(double a, Price b) => new((float)a % (b?.priceInUsd ?? 0));

        public static bool operator ==(double a, Price b) => Math.Abs((float)a - (b?.priceInUsd ?? 0)) < Tolerance.FLOAT;
        public static bool operator !=(double a, Price b) => Math.Abs((float)a - (b?.priceInUsd ?? 0)) > Tolerance.FLOAT;
        public static bool operator >(double a, Price b) => (float)a > (b?.priceInUsd ?? 0);
        public static bool operator <(double a, Price b) => (float)a < (b?.priceInUsd ?? 0);
        public static bool operator >=(double a, Price b) => (float)a >= (b?.priceInUsd ?? 0);
        public static bool operator <=(double a, Price b) => (float)a <= (b?.priceInUsd ?? 0);
        #endregion

        #region decimal
        public static Price operator +(Price a, decimal b) => new((a?.priceInUsd ?? 0) + (float)b);
        public static Price operator -(Price a, decimal b) => new((a?.priceInUsd ?? 0) - (float)b);
        public static Price operator *(Price a, decimal b) => new((a?.priceInUsd ?? 0) * (float)b);
        public static Price operator /(Price a, decimal b) => new((a?.priceInUsd ?? 0) / (float)b);
        public static Price operator %(Price a, decimal b) => new((a?.priceInUsd ?? 0) % (float)b);

        public static bool operator ==(Price a, decimal b) => Math.Abs((a?.priceInUsd ?? 0) - (float)b) < Tolerance.FLOAT;
        public static bool operator !=(Price a, decimal b) => Math.Abs((a?.priceInUsd ?? 0) - (float)b) > Tolerance.FLOAT;
        public static bool operator >(Price a, decimal b) => (a?.priceInUsd ?? 0) > (float)b;
        public static bool operator <(Price a, decimal b) => (a?.priceInUsd ?? 0) < (float)b;
        public static bool operator >=(Price a, decimal b) => (a?.priceInUsd ?? 0) >= (float)b;
        public static bool operator <=(Price a, decimal b) => (a?.priceInUsd ?? 0) <= (float)b;

        public static Price operator +(decimal a, Price b) => new((float)a + (b?.priceInUsd ?? 0));
        public static Price operator -(decimal a, Price b) => new((float)a - (b?.priceInUsd ?? 0));
        public static Price operator *(decimal a, Price b) => new((float)a * (b?.priceInUsd ?? 0));
        public static Price operator /(decimal a, Price b) => new((float)a / (b?.priceInUsd ?? 0));
        public static Price operator %(decimal a, Price b) => new((float)a % (b?.priceInUsd ?? 0));

        public static bool operator ==(decimal a, Price b) => Math.Abs((float)a - (b?.priceInUsd ?? 0)) < Tolerance.FLOAT;
        public static bool operator !=(decimal a, Price b) => Math.Abs((float)a - (b?.priceInUsd ?? 0)) > Tolerance.FLOAT;
        public static bool operator >(decimal a, Price b) => (float)a > (b?.priceInUsd ?? 0);
        public static bool operator <(decimal a, Price b) => (float)a < (b?.priceInUsd ?? 0);
        public static bool operator >=(decimal a, Price b) => (float)a >= (b?.priceInUsd ?? 0);
        public static bool operator <=(decimal a, Price b) => (float)a <= (b?.priceInUsd ?? 0);
        #endregion
    }

    // create a custom json converter
    public class PriceConverter : JsonConverter<Price>
    {
        public override void WriteJson(JsonWriter writer, Price value, JsonSerializer serializer)
        {
            // write the float value and the is estimate flag
            writer.WriteStartObject();
            writer.WritePropertyName(nameof(Price.CurrencyCode));
            writer.WriteValue(value.CurrencyCode.ToString());
            writer.WritePropertyName(nameof(Price.PriceInUsd));
            writer.WriteValue(value.PriceInUsd);
            writer.WritePropertyName(nameof(Price.IsEstimate));
            writer.WriteValue(value.IsEstimate);
            writer.WriteEndObject();
        }

        public override Price ReadJson(JsonReader reader, Type objectType, Price existingvalue, bool hasExistingvalue, JsonSerializer serializer)
        {
            // null check
            if (reader.TokenType == JsonToken.Null) return new Price();
            // read the float value and the is estimate flag
            JObject jObject = JObject.Load(reader);
            string currencyCodeAsString = jObject[nameof(Price.CurrencyCode)]?.Value<string>();
            CurrencyCode currencyCode = CurrencyCode.USD;
            if (currencyCodeAsString != null) currencyCode = (CurrencyCode)Enum.Parse(typeof(CurrencyCode), currencyCodeAsString);
            float value = (jObject[nameof(Price.PriceInUsd)] ?? 0f).Value<float>();
            bool isEstimate = (jObject[nameof(Price.IsEstimate)] ?? false).Value<bool>();
            return new Price(value, currencyCode, isEstimate);
        }
    }
}
