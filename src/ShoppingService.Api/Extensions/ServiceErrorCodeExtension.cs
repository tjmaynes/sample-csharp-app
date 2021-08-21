using ShoppingService.Core.Common;

namespace ShoppingService.Api.Extensions {
    public static class ServiceErrorCodeExtension
    {
        public static int ToStatusCode(this ServiceErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ServiceErrorCode.InvalidItem: return 422;
                case ServiceErrorCode.ItemNotFound: return 404;
                default: return 500;
            }
        }
    }
}