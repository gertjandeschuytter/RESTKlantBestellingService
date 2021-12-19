using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer_KlantBestelling.ServiceExceptions {
    public class KlantServiceException : Exception {
        public KlantServiceException() {
        }

        public KlantServiceException(string message) : base(message) {
        }

        public KlantServiceException(string message, Exception innerException) : base(message, innerException) {
        }

        protected KlantServiceException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
