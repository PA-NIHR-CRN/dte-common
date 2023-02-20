using System;
using System.Collections.Generic;
using System.Net;
using Dte.Common.Exceptions;
using Dte.Common.Exceptions.Common;

namespace Dte.Common.Responses
{
    public class Response<T> : ResponseBase<T>
    {
        public Response()
        {
            Errors = new List<Error>();
        }

        public static Response<T> CreateSuccessfulContentResponse(T response, string conversationId = null, int version = 1)
        {
            return new Response<T> { IsSuccess = true, Content = response, ConversationId = conversationId, Version = version};
        }
        
        public static Response<T> CreateSuccessfulResponse(string conversationId = null, int version = 1)
        {
            return new Response<T> { IsSuccess = true, ConversationId = conversationId, Version = version};
        }
        
        public static Response<T> CreateErrorMessageResponse(string service, string component, string customCode, string errorMessage, string conversationId = null, int version = 1)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Errors = new List<Error> { new Error(service, component, customCode, errorMessage) },
                ConversationId = conversationId,
                Version = version
            };
        }
        
        public static Response<T> CreateErrorMessageResponse(IEnumerable<Error> errors, string conversationId = null, int version = 1)
        {
            return new Response<T> { IsSuccess = false, Errors = errors, ConversationId = conversationId, Version = version };
        }
        
        public static Response<T> CreateExceptionResponse(string service, string component, string customCode, Exception ex, string conversationId = null, int version = 1)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Errors = new List<Error> { new Error(service, component, ex.GetType().Name, customCode, ex.Message) },
                ConversationId = conversationId,
                Version = version
            };
        }
        
        public static Response<T> CreateHttpExceptionResponse(string component, HttpServiceException ex, string customCode, string conversationId = null, int version = 1)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Errors = new List<Error>
                {
                    new Error(ex.ServiceName, component, ex.GetType().Name, ex.HttpStatusCode.ToString(), (int)ex.HttpStatusCode, ex.ResponseContent, customCode, ex.Message)
                },
                ConversationId = conversationId,
                Version = version
            };
        }
        
        public static Response<T> CreateNotFoundResponse(string service, string component, string customCode, string errorMessage, string conversationId = null, int version = 1)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Errors = new List<Error>
                {
                    new Error(service, component, null, HttpStatusCode.NotFound.ToString(), (int)HttpStatusCode.NotFound, null, customCode, errorMessage)
                },
                ConversationId = conversationId,
                Version = version
            };
        }
    }
}