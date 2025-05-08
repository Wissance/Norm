namespace Wissance.nOrm.Settings
{
    public class DbRepositorySettings
    {
        /// <summary>
        ///    BufferThreshold value that used as a comparable value if buffer size >= threshold it means that
        ///    synchronization process must be started
        /// </summary>
        public int BufferThreshold { get; set; }
        /// <summary>
        ///    Sql command timeout in milliseconds, if command execution time > timeout then execution fails
        /// </summary>
        public int CommandTimeout { get; set; }
        /// <summary>
        ///     Timeout between synchronization operations iterations in milliseconds
        /// </summary>
        public int BufferSynchronizationDelayTimeout { get; set; }
        /// <summary>
        ///     Timeout in ms, after exceeding buffers will be synchronized. If set to -1 there is no force
        ///     synchronization. If > 0 then must be greater then BufferSynchronizationDelayTimeout
        /// </summary>
        public int ForceSynchronizationBufferDelay { get; set; }
    }
}