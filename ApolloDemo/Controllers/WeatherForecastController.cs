using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApolloDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;

        //private readonly Configs _config;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            //_config = config;
        }

        [HttpGet]
        public string Get(/*[FromServices]IOptions<Abc2> options*/)
        {
            //var a2 = options.Value;
            //return $"{a2.Abcd2}";
            return $"appolo-abc:{Configs.ApolloAbc} \r\naaa:{Configs.Aaa} \r\nAbc:{Configs.Abc}  \r\nAbc2:{Configs.Abc2?.Abcd2}";//configuration["abc:abcd"];
        }

        [HttpGet("GetDis")]
        public IActionResult GetDis([FromServices] IDistributedCache cache, /*[FromServices] IMemoryCache memoryCache, [FromServices] IEasyCachingProvider easyCaching,*/ [FromQuery] string query)
        {

            #region IDistributedCache
            var key = $"GetDis-{query ?? ""}";
            var time = cache.GetString(key);
            if (string.IsNullOrEmpty(time)) //此处需要考虑并发情形
            {
                var option = new DistributedCacheEntryOptions();
                time = "time-val-apollo";
                cache.SetString(key, time, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600) });
            }
            #endregion

            #region IEasyCachingProvider
            //var key = $"GetDis-{query ?? ""}";
            //var time = easyCaching.Get(key, () => DateTime.Now.ToString(), TimeSpan.FromSeconds(600));


            #endregion

            return Content("abc" + time);
        }
    }
}
