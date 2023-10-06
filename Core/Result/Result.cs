using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Result
{
    public class Result
    {
        internal Result()
        {
            this.Errors = new List<Error>();
            this.InfoLogs = new List<string>();
        }
        public void AddError(Error error)
        {
            this.Errors.Add(error);
        }

        public List<Error> Errors { get; private set; }

        public Error FirstError => Errors.FirstOrDefault();

        public List<string> InfoLogs { get; set; }

        public static Result<bool> ErrorResult(string description, Exception ex = null)
        {
            return new Error(description, ex);
        }
        public static Result<T> ErrorResult<T>(string description, Exception ex = null)
        {
            return new Error(description, ex);
        }
    }

    public class Result<T> : Result
    {
        public bool Success
        {
            get
            {
                if (typeof(T) == typeof(bool))
                    return (this as Result<bool>).Data && Errors.Count == 0;
                return Errors.Count == 0;
            }
        }
        public T Data { get; set; }

        private Result()
        {
        }
        public Result(T data) : this()
        {
            this.Data = data;
        }

        public static implicit operator Result<T>(Error error)
        {
            return new Result<T>(error);
        }

        public static implicit operator Result<T>(T value)
        {
            return new Result<T>(value);
        }

        public static implicit operator bool(Result<T> value)
        {
            return value.Success;
        }


        public static implicit operator T(Result<T> value)
        {
            return value.Data;
        }


        public static implicit operator Result<T>(Exception exp)
        {
            return new Result<T>(exp);
        }

        public Result(Error error) : this()
        {
            this.AddError(error);
        }

        public Result<E> ToError<E>()
        {
            var result = new Result<E>();
            foreach (var error in Errors)
            {
                result.AddError(error);
            }
            return result;
        }

        public static Result<T> NotFound
        {
            get
            {
                return new Error("Record Not Found!");
            }
        }
    }

    public class Error
    {
        public string MethodName { get; set; }
        public string Description { get; private set; }
        public Exception Exception { get; private set; }

        public Error(string description, Exception exception = null/*, string methodName = null*/)
        {
            this.Description = description;
            this.Exception = exception;
            //this.MethodName = methodName;
        }
        public static implicit operator Error(Exception exp)
        {
            return new Error(exp.Message, exp);
        }

        public readonly static Error NotFound = new Error("NotFound");
    }
}
