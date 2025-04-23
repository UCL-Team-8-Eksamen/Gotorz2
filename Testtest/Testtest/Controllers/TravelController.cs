//Testformål - Login

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Testtest.Client;
using Testtest.Client.Models;



namespace Testtest.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TravelController : ControllerBase
	{
		private static readonly string[] ArrivalCountries = new[]
		{
			"Malta", "Spain", "Turkey", "Germany", "Thailand", "Canada"
		};

		private readonly ILogger<TravelController> logger;

		public TravelController(ILogger<TravelController> logger)
		{
			this.logger = logger;
		}

		[HttpGet]
		[Authorize]
		public IEnumerable<Travel> Get()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new Travel
			{
				DepartureTime = DateTime.Now.AddDays(index),
				NumberOfBagageItems = rng.Next(1, 15),
				ArrivalCountry = ArrivalCountries[rng.Next(ArrivalCountries.Length)],
				UserName = User.Identity?.Name ?? string.Empty
			});
		}


		[HttpGet("{date}")]
		[Authorize]
		public Travel Get(DateTime date)
		{
			var rng = new Random();
			return new Travel
			{
				DepartureTime = date,
				NumberOfBagageItems = rng.Next(1, 15),
				ArrivalCountry = ArrivalCountries[rng.Next(ArrivalCountries.Length)],
				UserName = User.Identity?.Name ?? string.Empty
			};
		}

	}
}