using System;
using System.Diagnostics;

using LibreTranslate.Client.Net.Models;

namespace LibreTranslate.Client.Net;

[DebuggerDisplay("Success: {IsSuccess}")]
public sealed class LibreTranslateApiResult<TValue>
{
    public readonly TValue? Value;
    public readonly LibreTranslateApiError? Error;

    public readonly bool IsSuccess;

    private LibreTranslateApiResult(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = default;
    }

    private LibreTranslateApiResult(LibreTranslateApiError error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }


    public static implicit operator LibreTranslateApiResult<TValue>(TValue value)
    {
        return new LibreTranslateApiResult<TValue>(value);
    }

    public static implicit operator LibreTranslateApiResult<TValue>(LibreTranslateApiError error)
    {
        return new LibreTranslateApiResult<TValue>(error);
    }

    public LibreTranslateApiResult<TValue> Match(Func<TValue, LibreTranslateApiResult<TValue>> success, Func<LibreTranslateApiError, LibreTranslateApiResult<TValue>> failure) => IsSuccess ? success(Value!) : failure(Error!);
}