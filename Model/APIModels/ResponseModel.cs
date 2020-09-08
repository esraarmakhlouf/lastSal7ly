using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Model.APIModels
{
    public class ResponseModel
    {
        public EN_ModelState ModelState { get; set; } = EN_ModelState.Success;

        public string Message { get; set; }

        public EN_ResponseStatus Status { get; set; } = EN_ResponseStatus.Success;
        public object Data { get; set; } = new object();
    }

    public class ApiResponseModel
    {

        public string Message { get; set; }

        public EN_ResponseStatus Status { get; set; } = EN_ResponseStatus.Success;
        public object Data { get; set; } = new object();
        public string[] Errors { get; set; }
    }
    public enum EN_ModelState
    {
        Success = 1,
        Error = 2,
        NotValid = 3,
        NotConfirm = 4,
        NotFound = 5,
        Duplicate = 6,
        LockoutEnabled = 7
    }

    public enum EN_ResponseStatus
    {
        Success = 1,
        Faild = 2
    }
}
