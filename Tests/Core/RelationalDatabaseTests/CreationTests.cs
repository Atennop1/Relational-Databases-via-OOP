﻿using System;
using System.Net.Sockets;
using Npgsql;
using NUnit.Framework;

namespace RelationalDatabasesViaOOP.Tests.Core.RelationalDatabaseTests
{
    public sealed class CreationTests
    {
        [Test]
        public void IsCreationCorrect1() 
            => Assert.Throws<ArgumentException>(() => { var database = new RelationalDatabase(null!); });

        [Test]
        public void IsCreationCorrect2()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var database = new RelationalDatabase("");
                database.SendReadingRequest("select * from humans");
            });
        }
        
        [Test]
        public void IsCreationCorrect3()
        {
            Assert.Throws<SocketException>(() =>
            {
                var database = new RelationalDatabase(@"Server=blahblah;Port=5434;User Id=postgres;Password=igay123Aa;Database=TestDB");
                database.SendReadingRequest("select * from humans");
            });
        }
        
        [Test]
        public void IsCreationCorrect4()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var database = new RelationalDatabase(@"Server=localhost;Port=blahblah;User Id=postgres;Password=igay123Aa;Database=TestDB");
                database.SendReadingRequest("select * from humans");
            });
        }
        
        [Test]
        public void IsCreationCorrect5()
        {
            Assert.Throws<PostgresException>(() =>
            {
                var database = new RelationalDatabase(@"Server=localhost;Port=5434;User Id=blahblah;Password=pleaseDoNotDropMyTables;Database=TestDB");
                database.SendReadingRequest("select * from humans");
            });
        }
        
        [Test]
        public void IsCreationCorrect6()
        {
            Assert.Throws<PostgresException>(() =>
            {
                var database = new RelationalDatabase(@"Server=localhost;Port=5434;User Id=postgres;Password=pleaseDoNotDropMyTables;Database=TestDB");
                database.SendReadingRequest("select * from humans");
            });
        }
        
        [Test]
        public void IsCreationCorrect7()
        {
            Assert.Throws<PostgresException>(() =>
            {
                var database = new RelationalDatabase(@"Server=localhost;Port=5434;User Id=postgres;Password=pleaseDoNotDropMyTables;Database=blahblah");
                database.SendReadingRequest("select * from humans");
            });
        }
        
        [Test]
        public void IsCreationCorrect8()
        {
            var database = new RelationalDatabase(@"Server=localhost;Port=5434;User Id=postgres;Password=pleaseDoNotDropMyTables;Database=DatabaseForLibraryTests");
            database.SendReadingRequest("select * from humans");
        }
    }
}