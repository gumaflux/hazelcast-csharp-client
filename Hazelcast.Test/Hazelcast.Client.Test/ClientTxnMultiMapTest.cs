using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace Hazelcast.Client.Test
{
    [TestFixture]
    public class ClientTxnMultiMapTest : HazelcastBaseTest
    {
        private string _name;

        [SetUp]
        public void Init()
        {
            _name = TestSupport.RandomString();
        }

        [TearDown]
        public static void Destroy()
        {
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestPutGetRemove()
        {
            var mm = Client.GetMultiMap<object, object>(_name);

            for (var i = 0; i < 10; i++)
            {
                var key = i + "key";
                Client.GetMultiMap<object, object>(_name).Put(key, "value");
                var context = Client.NewTransactionContext();
                context.BeginTransaction();
                var multiMap = context.GetMultiMap<object, object>(_name);
                Assert.IsFalse(multiMap.Put(key, "value"));
                Assert.IsTrue(multiMap.Put(key, "value1"));
                Assert.IsTrue(multiMap.Put(key, "value2"));
                Assert.AreEqual(3, multiMap.Get(key).Count);
                context.CommitTransaction();
                Assert.AreEqual(3, mm.Get(key).Count);
            }
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestPutGetRemove2()
        {
            var mm = Client.GetMultiMap<object, object>(_name);
            var key = "key";
            Client.GetMultiMap<object, object>(_name).Put(key, "value");
            var context = Client.NewTransactionContext();

            context.BeginTransaction();

            var multiMap = context.GetMultiMap<object, object>(_name);

            Assert.IsFalse(multiMap.Put(key, "value"));
            Assert.IsTrue(multiMap.Put(key, "value1"));
            Assert.IsTrue(multiMap.Put(key, "value2"));
            Assert.AreEqual(3, multiMap.Get(key).Count);

            context.CommitTransaction();

            Assert.AreEqual(3, mm.Get(key).Count);
        }

        [Test]
        public virtual void TestRemove()
        {
            const string key = "key";
            const string value = "value";

            var multiMap = Client.GetMultiMap<string, string>(_name);

            multiMap.Put(key, value);
            var tx = Client.NewTransactionContext();

            tx.BeginTransaction();
            tx.GetMultiMap<string, string>(_name).Remove(key, value);
            tx.CommitTransaction();

            Assert.AreEqual(new List<string>(), multiMap.Get(key));
        }

        [Test]
        public virtual void TestRemoveAll()
        {
            const string key = "key";
            const string value = "value";
            var name = TestSupport.RandomString();
            var multiMap = Client.GetMultiMap<string, string>(name);
            for (var i = 0; i < 10; i++)
            {
                multiMap.Put(key, value + i);
            }


            var tx = Client.NewTransactionContext();

            tx.BeginTransaction();
            tx.GetMultiMap<string, string>(name).Remove(key);
            tx.CommitTransaction();

            Assert.AreEqual(new List<string>(), multiMap.Get(key));
        }

        [Test]
        public void TestSize()
        {
            var key = "key";
            var value = "value";

            var mm = Client.GetMultiMap<object, object>(_name);
            mm.Put(key, value);

            var tx = Client.NewTransactionContext();
            tx.BeginTransaction();
            var txMultiMap = tx.GetMultiMap<object, object>(_name);

            txMultiMap.Put(key, "newValue");
            txMultiMap.Put("newKey", value);

            Assert.AreEqual(3, txMultiMap.Size());

            tx.CommitTransaction();
        }

        [Test]
        public void TestValueCount()
        {
            var key = "key";
            var value = "value";

            var mm = Client.GetMultiMap<object, object>(_name);
            mm.Put(key, value);

            var tx = Client.NewTransactionContext();
            tx.BeginTransaction();
            var txMultiMap = tx.GetMultiMap<object, object>(_name);

            txMultiMap.Put(key, "newValue");

            Assert.AreEqual(2, txMultiMap.ValueCount(key));

            tx.CommitTransaction();
        }
    }
}