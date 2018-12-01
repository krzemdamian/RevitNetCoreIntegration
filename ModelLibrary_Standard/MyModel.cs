using ProtoBuf;
using System;


namespace ModelLibrary_Standard
{
    [ProtoContract]
    public class MyModel
    {
        [ProtoMember(1)]
        public int test;
    }
}
