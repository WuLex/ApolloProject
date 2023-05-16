using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Enums;
using Com.Ctrip.Framework.Apollo.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApolloDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
                {
                    //��������־�������
                    LogManager.UseConsoleLogging(Com.Ctrip.Framework.Apollo.Logging.LogLevel.Trace);
                    //var c = configurationBuilder.Build().GetSection("Apollo").Get<ApolloOptions>();
                    //ע�밢��������
                    configurationBuilder.AddApollo(configurationBuilder.Build().GetSection("Apollo"))
                        //.AddDefault(Com.Ctrip.Framework.Apollo.Enums.ConfigFileFormat.Json)
                        .AddDefault() //��ӳ�ʼ����Ĭ��������
                        .AddNamespace("app-aidemo4438", ConfigFileFormat.Json)
                        .AddNamespace("application", ConfigFileFormat.Properties);

                    //���������ֵ����apollo
                    //optional:false �ļ������ڱ���
                    //reloadOnChange:true  �ļ�������¼�������
                    configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    //�������һ���ļ� ����ǰһ���ļ�
                    configurationBuilder.AddJsonFile("test.json", optional: false, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}