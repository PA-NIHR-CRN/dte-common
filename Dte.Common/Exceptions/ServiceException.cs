// using System;
// using System.Collections.Generic;
// using System.Net;
// using System.Runtime.Serialization;
// using System.Security.Permissions;
// using Dte.Common.Exceptions.Common;
//
// namespace Dte.Common.Exceptions
// {
//     [Serializable]
//     public abstract class ServiceException : Exception
//     {
//         protected ServiceException() { }
//
//         protected ServiceException(HttpStatusCode httpStatusCode, string message, Exception innerException) : base(message, innerException)
//         {
//             HttpStatusCode = httpStatusCode;
//         }
//
//         protected ServiceException(HttpStatusCode httpStatusCode, string message) : base(message)
//         {
//             HttpStatusCode = httpStatusCode;
//         }
//
//         protected ServiceException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
//         {
//             var httpStatusCode = Enum.TryParse(typeof(HttpStatusCode), serializationInfo.GetString(nameof(HttpStatusCode)), out var result) ? (HttpStatusCode)result : HttpStatusCode.InternalServerError;
//             HttpStatusCode = httpStatusCode;
//             Errors = (List<Error>) serializationInfo.GetValue(nameof (Errors), typeof (List<Error>));
//         }
//
//         public HttpStatusCode HttpStatusCode { get; }
//         public List<Error> Errors { get; } = new List<Error>();
//
//         [SecurityPermission((SecurityAction) 2, SerializationFormatter = true)]
//         public override void GetObjectData(SerializationInfo info, StreamingContext context)
//         {
//             if (info == null)
//             {
//                 throw new ArgumentNullException(nameof (info), "You must supply a non-null SerializationInfo property.");
//             }
//             
//             info.AddValue("HttpStatusCode", HttpStatusCode);
//             info.AddValue("Errors", Errors);
//             
//             base.GetObjectData(info, context);
//         }
//     }
// }