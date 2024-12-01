﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobJuggler.DTO.Client;
using JobJuggler.Application.Exceptions;
using JobJuggler.Application.Services.Interfaces;
using JobJuggler.Domain.Models;
using JobJuggler.Persistence;
using JobJugglers.Mapping;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace JobJuggler.Application.Services;

public class ClientService : IClientService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ClientService(DataContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ClientDTO>> GetClients() {
        var clients = await _context.Clients.ToListAsync();
        
        return ClientMapper.ClientsToDTO(clients);
    }

    public async Task<ClientDTO> CreateClient(ClientInsertDTO clientToInput) {
        var client = ClientMapper.ClientInsertToClientModel(clientToInput);
        _context.Add(client);
        
        await _context.SaveChangesAsync();
        return ClientMapper.ClientToDTO(client);
    }

    public async Task<ClientDTO?> GetClientById(int clientId)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(client => client.Id == clientId);
        return client == null ? null : ClientMapper.ClientToDTO(client);
    } 

    public async Task<ClientDTO?> UpdateClient(int clientId, JsonPatchDocument clientInfo) {
        var existingClient = await _context.Clients.FirstOrDefaultAsync(cl => cl.Id == clientId) 
                             ?? throw new RecordNotFoundException(typeof(Client), clientId);
        clientInfo.ApplyTo(existingClient);

        await _context.SaveChangesAsync();
        var clientToReturn = ClientMapper.ClientToDTO(existingClient);
        return clientToReturn;
    }

    public async Task<ClientProfile?> GetProfile(int clientId) {
        var client = await _context.Clients
            .Include(c => c.Jobs)
            .ThenInclude(j => j.Location)
            .FirstOrDefaultAsync(c => c.Id == clientId);
        return ClientMapper.ClientToProfile(client);
    }
}
