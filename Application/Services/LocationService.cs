using Application.DTOs;
using Application.DTOs.Location;
using Application.Exceptions;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services;
public class LocationService : ILocationService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public LocationService(DataContext dataContext, IMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public async Task<List<LocationDTO>> GetAllLocations()
    {
        var locations = await _context.Locations.ProjectTo<LocationDTO>(_mapper.ConfigurationProvider).ToListAsync();
        return locations;
    }

    public async Task<LocationDTO> CreateLocation(LocationInsertDTO locationToInsert)
    {
        var location = _mapper.Map<Location>(locationToInsert);
        _context.Add(location);

        await _context.SaveChangesAsync();

        return _mapper.Map<LocationDTO>(location);
    }

    public async Task<LocationDTO> GetLocationById(int locationId)
    {
        var location = await _context.Locations.FindAsync(locationId);
        if (location is null)
        {
            var exception = new RecordNotFoundException($"No Location found with Id {locationId}");
            throw exception;
        }
        var mappedLoc = _mapper.Map<LocationDTO>(location);
        return mappedLoc;
    }

    public Task<LocationDTO> UpdateLocation(int locationId, JsonPatchDocument locationInfo)
    {
        throw new NotImplementedException();
    }
}
