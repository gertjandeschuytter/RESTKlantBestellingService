using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer_KlantBestelling.ServiceExceptions {
    public class BestellingServiceException : Exception {
        public BestellingServiceException() {
        }

        public BestellingServiceException(string message) : base(message) {
        }

        public BestellingServiceException(string message, Exception innerException) : base(message, innerException) {
        }

        protected BestellingServiceException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
