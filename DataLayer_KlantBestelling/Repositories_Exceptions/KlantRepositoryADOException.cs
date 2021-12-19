using System;
using System.Runtime.Serialization;

namespace DataLayer_KlantBestelling {
    [Serializable]
    internal class KlantRepositoryADOException : Exception {
        public KlantRepositoryADOException() {
        }

        public KlantRepositoryADOException(string message) : base(message) {
        }

        public KlantRepositoryADOException(string message, Exception innerException) : base(message, innerException) {
        }

        protected KlantRepositoryADOException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}