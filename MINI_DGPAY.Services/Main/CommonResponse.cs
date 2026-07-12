using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_DGPAY.Services.Main
{
    public class CommonResponse<T>
    {
        public string? respCode { get; set; }
        public string? respMessage { get; set; }
        public RespType respType { get; set; }
        public bool isSuccess { get; set; }
        public bool isError { get { return !isSuccess; } }
        public bool isValidationError { get { return respType == RespType.ValidationError; } }
        public bool isNotFound { get { return respType == RespType.NotFound; } }
        public bool isDataNotExist { get { return respType == RespType.DataNotExist; } }
        public bool isSystemError { get { return respType == RespType.SystemError; } }

        public static CommonResponse<T> Success(string data = "200", string message = "Success")
        {
            return new CommonResponse<T>
            {
                respCode = data,
                respMessage = message,
                respType = RespType.Success,
                isSuccess = true
            };
        }

        public static CommonResponse<T> ValidationError(string data = "400", string message = "Validation Error")
        {
            return new CommonResponse<T>
            {
                respCode = data,
                respMessage = message,
                respType = RespType.ValidationError,
                isSuccess = false
            };
        }

        public static CommonResponse<T> NotFound(string data = "404", string message = "Not Found")
        {
            return new CommonResponse<T>
            {
                respCode = data,
                respMessage = message,
                respType = RespType.NotFound,
                isSuccess = false
            };
        }

        public static CommonResponse<T> DataNotExist(string data = "404", string message = "Data Not Exist")
        {
            return new CommonResponse<T>
            {
                respCode = data,
                respMessage = message,
                respType = RespType.DataNotExist,
                isSuccess = false
            };
        }

        public static CommonResponse<T> SystemError(string data = "500", string message = "System Error")
        {
            return new CommonResponse<T>
            {
                respCode = data,
                respMessage = message,
                respType = RespType.SystemError,
                isSuccess = false
            };
        }

        public static CommonResponse<T> BadRequest(string data = "400", string message = "Bad Request")
        {
            return new CommonResponse<T>
            {
                respCode = data,
                respMessage = message,
                respType = RespType.None,
                isSuccess = false
            };
        }
    }

    public enum RespType
    {
        None,
        Success,
        ValidationError,
        NotFound,
        DataNotExist,
        SystemError
    }
}
