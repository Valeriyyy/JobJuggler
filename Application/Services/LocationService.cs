using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
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
}
