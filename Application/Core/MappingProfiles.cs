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
        CreateMap<Client, ClientDTO>();
        CreateMap<Location, LocationDTO>();
        CreateMap<LocationInsertDTO, Location>();
        CreateMap<JobInsertDTO, Job>();
    }
}
/*{
  "clientId": 1,
  "locationId": 1,
  "scheduledDate": "2022-09-04T03:01:08.865Z",
  "scheduledArrivalStartDate": "2022-09-09T03:01:08.865Z",
  "scheduledArrivalEndDate": "2022-09-09T05:01:08.865Z"
}*/