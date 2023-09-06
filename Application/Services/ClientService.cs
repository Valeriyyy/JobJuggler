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
}
