﻿using AutoMapper;
using EnglishBattle.BLL.DTOs;
using EnglishBattle.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattle.BLL.Mappers
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<IrregularVerb, IrregularVerbDto>();

            CreateMap<Game, GameDto>()
                .ForMember(x => x.PlayerName, opt => opt.MapFrom(x => x.Player.UserName));
        }
    }
}
