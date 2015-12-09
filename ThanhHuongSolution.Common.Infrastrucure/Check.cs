using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ThanhHuongSolution.Common.LocResources;

namespace ThanhHuongSolution.Common.Infrastrucure
{
    [DebuggerStepThrough]
    public static class Check
    {
        public static void NotNull<T>(T value, string parameterName)
        {
            if (ReferenceEquals(value, null))
                throw new InvalidOperationException(string.Format(ErrorResources.OBJECT_IS_NOT_NULL, parameterName));
        }

        public static void NotEmpty(string value, string parameterName)
        {
            if (ReferenceEquals(value, null))
                throw new InvalidOperationException(string.Format(ErrorResources.OBJECT_IS_NOT_NULL_OR_EMPTY, parameterName));

            if (value.Trim().Length == 0)
                throw new InvalidOperationException(string.Format(ErrorResources.OBJECT_IS_NOT_NULL_OR_EMPTY, parameterName));
        }

        public static bool IsNull<T>(T value)
        {
            return ReferenceEquals(value, null);
        }

        public static bool IsNullOrEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value);
        }

        public static void ThrowExceptionIfNullOrEmpty(string value, string errorMessage, Exception innerException = null)
        {
            if (IsNullOrEmpty(value))
                throw new CustomException(errorMessage, innerException);
        }

        public static void ThrowExceptionIfZero(object value, string errorMessage, Exception innerException = null)
        {
            if (value == null)
                throw new CustomException(errorMessage, innerException);

            double parseValue;

            var bParsed = double.TryParse(value.ToString(), out parseValue);

            if (!bParsed)
                throw new CustomException(errorMessage, innerException);

            if (parseValue == 0)
                throw new CustomException(errorMessage, innerException);
        }

        public static void ThrowExceptionIfNull(object value, string errorMessage, Exception innerException = null)
        {
            if (IsNull(value))
                throw new CustomException(errorMessage, innerException);
        }

        public static void ThrowExceptionIfNotNull(object value, string errorMessage, Exception innerException = null)
        {
            if (!IsNull(value))
                throw new CustomException(errorMessage, innerException);
        }

        public static void ThrowExceptionIfCollectionIsNullOrZero<T>(IEnumerable<T> value, string errorMessage, Exception innerException = null)
        {
            if (value == null)
                throw new CustomException(errorMessage, innerException);

            if (value.ToList().Count == 0)
                throw new CustomException(errorMessage, innerException);
        }

        public static void ThrowException(string errorMessage, Exception innerException = null)
        {
            throw new CustomException(errorMessage, innerException);
        }

        public static void ThrowExceptionIfTrue(bool value, string errorMessage, Exception innerException = null)
        {
            if (value)
                throw new CustomException(errorMessage, innerException);
        }

        public static void ThrowExceptionIfFalse(bool value, string errorMessage, Exception innerException = null)
        {
            if (!value)
                throw new CustomException(errorMessage, innerException);
        }

        public static bool CollectionIsNullOrEmpty<T>(IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static void ThrowExceptionIfIsNotStartWith(string value, string target, string errorMessage)
        {
            if (!IsStartWith(value, target))
            {
                throw new CustomException(errorMessage);
            }
        }

        public static bool IsStartWith(string value, string target)
        {
            return value.StartsWith(target);
        }

        public static void ThrowExceptionIfIsNotEndWith(string value, string target, string errorMessage)
        {
            if (!IsEndWith(value, target))
            {
                throw new CustomException(errorMessage);
            }
        }

        public static bool IsEndWith(string value, string target)
        {
            return value.EndsWith(target);
        }

        public static bool IsValidIdType(string value)
        {
            ObjectId id;

            var check = ObjectId.TryParse(value, out id);

            return check;
        }

        public static void ThrowExceptionIfIsInvalidIdType(string value, string errorMessage)
        {
            if (!IsValidIdType(value)) throw new CustomException(errorMessage);
        }

        public static void ThrowExceptionIfIsInvalidIdType(string value)
        {
            if (!IsValidIdType(value))
                throw new CustomException(string.Format(CommonResources.ID_INVALID, value));
        }

        public static void ThrowExceptionIfIsInvalidIdType(IList<string> values)
        {
            if (values == null || values.Count == 0)
                return;

            foreach (var value in values)
            {
                if (!IsValidIdType(value))
                    throw new CustomException(string.Format(CommonResources.ID_INVALID, value));
            }
        }
    }
}
