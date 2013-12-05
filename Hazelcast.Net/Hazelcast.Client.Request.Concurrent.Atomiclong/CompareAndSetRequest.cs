using Hazelcast.IO.Serialization;
using Hazelcast.Serialization.Hook;

namespace Hazelcast.Client.Request.Concurrent.Atomiclong
{
    public class CompareAndSetRequest : AtomicLongRequest
    {
        private long expect;

        public CompareAndSetRequest()
        {
        }

        public CompareAndSetRequest(string name, long expect, long value) : base(name, value)
        {
            this.expect = expect;
        }

        public override int GetClassId()
        {
            return AtomicLongPortableHook.CompareAndSet;
        }

        /// <exception cref="System.IO.IOException"></exception>
        public override void WritePortable(IPortableWriter writer)
        {
            base.WritePortable(writer);
            writer.WriteLong("e", expect);
        }

        /// <exception cref="System.IO.IOException"></exception>
        public override void ReadPortable(IPortableReader reader)
        {
            base.ReadPortable(reader);
            expect = reader.ReadLong("e");
        }
    }
}