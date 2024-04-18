using System;

namespace Glitch9
{
    public readonly struct ErrorMessage
    {
        public static explicit operator string(ErrorMessage message) => message._value;
        public static explicit operator ErrorMessage(Exception exception) => new(exception);
        public static explicit operator ErrorMessage(Issue code) => new(code);
        public static explicit operator ErrorMessage(string value) => new(value);

        private readonly string _value;

        public ErrorMessage(string value)
        {
            this._value = value;
            GNLog.Error(value);
        }

        public ErrorMessage(Exception exception)
        {
            _value = $"{exception.Message}\n{exception.StackTrace}";
            GNLog.Error(_value);
        }

        public ErrorMessage(Issue code)
        {
            _value = code.ToString();
            GNLog.Error(_value);
        }

        public override string ToString() => _value;
        public static bool operator ==(ErrorMessage left, ErrorMessage right) => left._value == right._value;
        public static bool operator !=(ErrorMessage left, ErrorMessage right) => left._value != right._value;
        public override bool Equals(object obj) => obj is ErrorMessage message && _value == message._value;
        public override int GetHashCode() => _value.GetHashCode();
    }
}