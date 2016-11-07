using System;
using System.Runtime.Serialization;

namespace Savchin.CodeGeneration
{
    [Serializable]
    public class CodeGenerationException : Exception
    {
        public CodeGenerationException() { }
        public CodeGenerationException(string message) : base(message) { }
        public CodeGenerationException(string message, Exception inner) : base(message, inner) { }
        protected CodeGenerationException(SerializationInfo info,StreamingContext context): base(info, context) { }
    }
    
}
