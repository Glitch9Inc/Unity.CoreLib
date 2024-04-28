
namespace Glitch9
{
    public class TaskResult<T>
    {
        public static TaskResult<T> Success => new() { IsSuccess = true };
        public static TaskResult<T> Fail => new() { IsSuccess = false };

        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public Error Error { get; set; }

    }
}