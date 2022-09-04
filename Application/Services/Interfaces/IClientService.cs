using Application.DTOs;
using Domain.Models;

namespace Application.Services.Interfaces;

public interface IClientService
{
    public Task<List<ClientDTO>> GetClients();
    public Task<ClientDTO> CreateClient(ClientInsertDTO clientToInput);
    public Task<ClientDTO> GetClientById(int clientId);
}
