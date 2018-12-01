using ProtoBuf;
using System;


namespace ModelLibrary_Standard
{
    [ProtoContract]
    public class MyDataModel
    {
        [ProtoMember(1)]
        public int test;
    }
}
