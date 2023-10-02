using Application.DTOs.Client;
using Application.Exceptions;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services;

public class ClientService : IClientService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ClientService(DataContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ClientDTO>> GetClients() {
        var clients = await _context.Clients.ProjectTo<ClientDTO>(_mapper.ConfigurationProvider).ToListAsync();
        return clients;
    }

    public async Task<ClientDTO> CreateClient(ClientInsertDTO clientToInput) {
        var client = _mapper.Map<Client>(clientToInput);
        _context.Add(client);

        await _context.SaveChangesAsync();
        return _mapper.Map<ClientDTO>(client);
    }

    public async Task<ClientDTO?> GetClientById(int clientId) {
        var client = await _context.Clients.ProjectTo<ClientDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(client => client.Id == clientId);

        return client is null ? throw new RecordNotFoundException(typeof(Client), clientId) : client;
    }

    public async Task<ClientDTO?> UpdateClient(int clientId, JsonPatchDocument clientInfo) {
        var existingClient = await _context.Clients.FirstOrDefaultAsync(cl => cl.Id == clientId) ?? throw new RecordNotFoundException(typeof(Client), clientId);
        clientInfo.ApplyTo(existingClient);

        await _context.SaveChangesAsync();
        var clientToReturn = _mapper.Map<ClientDTO>(existingClient);
        return clientToReturn;
    }

    public async Task<ClientProfile?> GetProfile(int clientId) {
        var profile = await _context.Clients
            .Select(c => new ClientProfile {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                Jobs = c.Jobs.Select(j => new ClientProfileJob {
                    Id = j.Id,
                    Price = j.Price,
                    Notes = j.Notes,
                    IsCompleted = j.IsCompleted,
                    IsCanceled = j.IsCanceled,
                    CancelReason = j.CancelReason,
                    CanceledDate = j.CanceledDate,
                    ScheduledDate = j.ScheduledDate,
                    ScheduledArrivalStartDate = j.ScheduledArrivalStartDate,
                    ScheduledArrivalEndDate = j.ScheduledArrivalEndDate,
                    StartedDate = j.StartedDate,
                    CompletedDate = j.CompletedDate,
                    Location = new ClientProfileLocation {
                        LocationType = j.Location.LocationType,
                        Street1 = j.Location.Street1,
                        Street2 = j.Location.Street2,
                        City = j.Location.City,
                        State = j.Location.State,
                        PostalCode = j.Location.PostalCode,
                        Country = j.Location.Country,
                        Latitude = j.Location.Latitude,
                        Longitude = j.Location.Longitude,
                    }
                })
            }).FirstOrDefaultAsync(c => c.Id == clientId);

        return profile;
    }
}
