using AutoMapper;
using Domain.Dtos.User;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Mapping
{
	public class DtoToModelProfile : Profile
	{
		public DtoToModelProfile()
		{
			CreateMap<UserModel, UserDto>()
				.ReverseMap();
			CreateMap<UserModel, UserDtoCreate>()
		.ReverseMap();
			CreateMap<UserModel, UserDtoUpdate>()
		.ReverseMap();
		}
	}
}
