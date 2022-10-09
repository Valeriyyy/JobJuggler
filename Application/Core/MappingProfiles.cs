using Application.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<ClientInsertDTO, Client>();
        CreateMap<JobClientDTO, Client>();
        CreateMap<Client, ClientDTO>();
        CreateMap<Location, LocationDTO>();
        CreateMap<LocationInsertDTO, Location>();
        CreateMap<JobLocationDTO, Location>();
        CreateMap<JobInsertDTO, Job>();
    }
}