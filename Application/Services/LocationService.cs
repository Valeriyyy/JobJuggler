using Application.DTOs.Location;
using Application.Exceptions;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Services;
public class LocationService : ILocationService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<LocationService> _logger;

    public LocationService(DataContext dataContext, IMapper mapper, ILogger<LocationService> logger) {
        _context = dataContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<LocationDTO>> GetAllLocations() {
        var locations = await _context.Locations.ProjectTo<LocationDTO>(_mapper.ConfigurationProvider).ToListAsync();
        return locations;
    }

    public async Task<LocationDTO> CreateLocation(LocationInsertDTO locationToInsert) {
        var location = _mapper.Map<Location>(locationToInsert);
        _context.Add(location);

        await _context.SaveChangesAsync();

        return _mapper.Map<LocationDTO>(location);
    }

    public async Task<LocationDTO> GetLocationById(int locationId) {
        var location = await _context.Locations.FindAsync(locationId);
        if (location is null) {
            _logger.LogError("Record of type {objectName} with Id {id} not found", typeof(Location).Name, locationId);
            throw new RecordNotFoundException(typeof(Location), locationId);
        }
        var mappedLoc = _mapper.Map<LocationDTO>(location);
        return mappedLoc;
    }

    public async Task<LocationDTO> UpdateLocation(int locationId, JsonPatchDocument locationInfo) {
        var existingLocation = await _context.Locations.FindAsync(locationId);
        if (existingLocation is null) {
            _logger.LogError("Record of type {objectName} with Id {id} not found", typeof(Location).Name, locationId);
            throw new RecordNotFoundException(typeof(Location), locationId);
        }
        locationInfo.ApplyTo(existingLocation);

        await _context.SaveChangesAsync();
        var locationToReturn = _mapper.Map<LocationDTO>(existingLocation);
        return locationToReturn;
    }
}
