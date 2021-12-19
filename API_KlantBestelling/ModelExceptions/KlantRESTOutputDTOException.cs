using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace API_KlantBestelling.ModelExceptions {
    public class KlantRESTOutputDTOException : Exception {
        public KlantRESTOutputDTOException() {
        }

        public KlantRESTOutputDTOException(string message) : base(message) {
        }

        public KlantRESTOutputDTOException(string message, Exception innerException) : base(message, innerException) {
        }

        protected KlantRESTOutputDTOException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
