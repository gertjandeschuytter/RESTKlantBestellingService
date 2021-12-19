using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_KlantBestelling.Repositories_Exceptions {
    public class BestellingRepositoryADOException : Exception {
        public BestellingRepositoryADOException() {
        }

        public BestellingRepositoryADOException(string message) : base(message) {
        }

        public BestellingRepositoryADOException(string message, Exception innerException) : base(message, innerException) {
        }

        protected BestellingRepositoryADOException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
