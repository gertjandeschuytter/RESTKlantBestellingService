using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace API_KlantBestelling.Mappers.MapperExceptions {
    public class MapException : Exception {
        public MapException() {
        }

        public MapException(string message) : base(message) {
        }

        public MapException(string message, Exception innerException) : base(message, innerException) {
        }

        protected MapException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
