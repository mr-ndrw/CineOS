using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using en.AndrewTorski.CineOS.Logic.Core.ViewModels;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService.Controllers
{
    public class TestController : ApiController
    {
        [Route("api/test/2dseats")]
        [HttpGet]
        public List<IEnumerable<SeatViewModel>> Test()
        {
            var list = new List<SeatViewModel>()
		    {
		        new SeatViewModel("1", "2"), new SeatViewModel("1", "3"), new SeatViewModel("1", "2"),
                new SeatViewModel("3", "2"), new SeatViewModel("2", "1"), new SeatViewModel("2", "2"),
                new SeatViewModel("2", "3"), new SeatViewModel("3", "1"), new SeatViewModel("3", "3"), 
                new SeatViewModel("4", "3"), new SeatViewModel("4", "2"), new SeatViewModel("4", "1")
		    };

            var query = list.OrderBy(seat => seat.RowColumn).ToList();

            var rowMax = 3;
            var colMax = 4;

            var seat2dArray = new SeatViewModel[rowMax, colMax];

            var rows = new List<IEnumerable<SeatViewModel>>();

            var take1 = query.Take(rowMax).ToList();
            query.RemoveRange(0, rowMax);
            var take2 = query.Take(rowMax).ToList();
            query.RemoveRange(0, rowMax);
            var take3 = query.Take(rowMax).ToList();
            query.RemoveRange(0, rowMax);
            var take4 = query.Take(rowMax).ToList();
            query.RemoveRange(0, rowMax);

            Console.WriteLine("query count = {0}", query.Count);

            rows.Add(take1);
            rows.Add(take2);
            rows.Add(take3);
            rows.Add(take4);

            return rows;
        } 
    }
}
