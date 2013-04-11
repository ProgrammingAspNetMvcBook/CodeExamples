using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Ebuy.Website.Models;

namespace Ebuy.Website.App_Start
{
	public class AutoMapperConfig
	{
		public static void RegisterMappings()
		{
			Mapper.CreateMap<Auction, AuctionViewModel>();

			Mapper.AssertConfigurationIsValid();
		}
	}
}