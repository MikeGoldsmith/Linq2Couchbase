﻿using System;
using System.Linq;
using System.Net.Http;
using Couchbase.Configuration.Client;
using Couchbase.Linq.Extensions;
using Couchbase.Linq.Tests.Documents;
using Couchbase.N1QL;
using Couchbase.Views;
using NUnit.Framework;

namespace Couchbase.Linq.Tests
{
    [TestFixture]
    public sealed class QueryClientExtensionTests : N1QLTestBase
    {
        [Test]
        public void Test_POCO_Projection()
        {
            var config = new ClientConfiguration();
            var client = new QueryClient(new HttpClient(), new JsonDataMapper(config), config);
            var uri = new Uri("http://localhost:8093/query");
            const string bucket = "tutorial";

            var query = from c in client.Queryable<Contact>(bucket, uri)
                select c;

            foreach (var contact in query)
            {
                Console.WriteLine(contact.FirstName);
            }
        }

        [Test]
        public void Test_AnonymousType_Projection()
        {
            var config = new ClientConfiguration();
            var client = new QueryClient(new HttpClient(), new JsonDataMapper(config), config);
            var uri = new Uri("http://localhost:8093/query");
            const string bucket = "tutorial";

            var query = from c in client.Queryable<Contact>(bucket, uri)
                select new
                {
                    age = c.Age,
                    fname = c.FirstName
                };

            foreach (var contact in query)
            {
                Console.WriteLine("{0}, {1}", contact.fname, contact.age);
            }
        }

        [Test]
        public void Test_Select_Children()
        {
            var config = new ClientConfiguration();
            var client = new QueryClient(new HttpClient(), new JsonDataMapper(config), config);
            var uri = new Uri("http://localhost:8093/query");
            const string bucket = "tutorial";

            var query = from c in client.Queryable<Contact>(bucket, uri)
                select c.Children;

            foreach (var child in query)
            {
                Console.WriteLine("{0}, {1}", child, child);
            }
        }
    }
}