﻿using JobJuggler.DTO.Location;
using Microsoft.AspNetCore.JsonPatch;

namespace JobJuggler.Application.Services.Interfaces;
public interface ILocationService {
    public Task<List<LocationDTO>> GetAllLocations();
    public Task<LocationDTO> CreateLocation(LocationInsertDTO locationToInsert);
    public Task<LocationDTO> GetLocationById(int locationId);
    public Task<LocationDTO> UpdateLocation(int locationId, JsonPatchDocument locationInfo);
}
