using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;
using ConsoleApp20;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using R1.MarketplaceManagement.OrderReturnService.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Toolchains;
using System.Threading.Tasks;

namespace Standard
{
    public static class Connection
    {
        public static readonly string String = "Server=192.168.100.7;Database=OrderReturnService;user=yapaxi;password=test1234";
    }

    public interface INavigationSource : IDisposable
    {
        IQueryable<OrderReturn> GetOrderReturnsWithAllIncludes();
        IQueryable<OrderReturnSimple> GetOrderReturnsNoIncludes();
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;

            //Console.WriteLine("ready");
            //Console.ReadKey(false);
            //new Benchmarks().linq2db_join20times();
            //Console.ReadKey(false);
            //new Benchmarks().efCore_join();
            //Console.ReadKey(false);
            //new Benchmarks().efCoreNoTrack_join();

            var summary = BenchmarkRunner.Run<ReadBenchmarks>();
        }
    }
}