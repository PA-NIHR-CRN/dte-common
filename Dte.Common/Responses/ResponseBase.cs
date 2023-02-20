using System.Collections.Generic;
using Dte.Common.Exceptions.Common;
using Newtonsoft.Json;

namespace Dte.Common.Responses
{
    public abstract class ResponseBase
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("errors")]
        public IEnumerable<Error> Errors { get; set; }
        
        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }
        
        [JsonProperty("version")]
        public int Version { get; set; }
    }
    
    public abstract class ResponseBase<T> : ResponseBase
    {
        [JsonProperty("content")]
        public T Content { get; set; }
    }
}