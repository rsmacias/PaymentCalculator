using System;

namespace Payment.Lib {
    public struct Schedule {
        public DateTime start_time;
        public DateTime end_time;

        public Schedule(DateTime start_time, DateTime end_time) {
            this.start_time = start_time;
            this.end_time = end_time;
        }
    }
}
