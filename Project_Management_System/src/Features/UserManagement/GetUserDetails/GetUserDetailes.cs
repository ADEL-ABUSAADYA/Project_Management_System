// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Infrastructure;
// using Project_Management_System.Common;
// using Project_Management_System.Common.BaseEndpoints;
// using Project_Management_System.Common.Data.Enums;
// using Project_Management_System.Common.Views;
// using Project_Management_System.Features.UserManagement.GetUserDetalies;
//
//
//
// namespace Project_Management_System.Features.UserManagement.GetUserDetails
// {
//   
//     public class UserDetaileEndpoint : BaseEndpoint<RequestUserDetailesEndpoint ,ResponseUserDetailsEndpoint >
//     {
//         public UserDetaileEndpoint(BaseEndpointParameters<RequestUserDetailesEndpoint> parameters) : base(parameters)
//         {
//         }
//
//         [HttpGet]
//         public async Task<EndpointResponse<ResponseUserDetailsEndpoint>> GetUSerDetails([FromQuery] RequestUserDetailesEndpoint request)
//         {
//             var validationResult = ValidateRequest(request);
//             if (!validationResult.isSuccess)
//                 return EndpointResponse<ResponseUserDetailsEndpoint>.Failure(ErrorCode.InvalidInput);
//             
//     
//
//
//
//        
//
//             return EndpointResponse<ResponseUserDetailsEndpoint>.Success(default);
//
//         }
//
//
//     }
// }
