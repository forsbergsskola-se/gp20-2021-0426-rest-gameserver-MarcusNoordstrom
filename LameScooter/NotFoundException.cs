using System;

namespace LameScooter {
    public class NotFoundException : Exception {
        public NotFoundException(string message)
            : base(message) {
        }
    }
}