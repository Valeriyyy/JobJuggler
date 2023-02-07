using Application.DTOs;
using Application.DTOs.Location;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;
public interface ILocationService
{
    public Task<List<LocationDTO>> GetAllLocations();
    public Task<LocationDTO> CreateLocation(LocationInsertDTO locationToInsert);
}
