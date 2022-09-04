using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services;

public class ClientService : IClientService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ClientService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ClientDTO>> GetClients()
    {
        var clients = await _context.Clients.ProjectTo<ClientDTO>(_mapper.ConfigurationProvider).ToListAsync();
        return clients;
    }

    public async Task<ClientDTO> CreateClient(ClientInsertDTO clientToInput)
    {
        var client = _mapper.Map<Client>(clientToInput);
        _context.Add(client);

        var res = await _context.SaveChangesAsync();
        Console.WriteLine("This is the save chanes response " + res);
        return _mapper.Map<ClientDTO>(client);
    }

    public async Task<ClientDTO> GetClientById(int clientId)
    {
        var client = await _context.Clients.ProjectTo<ClientDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(client => client.Id == clientId);

        return client;
    }
}
