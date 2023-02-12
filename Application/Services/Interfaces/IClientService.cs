using Application.DTOs.Client;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Services.Interfaces;

public interface IClientService
{
    public Task<List<ClientDTO>> GetClients();
    public Task<ClientDTO> CreateClient(ClientInsertDTO clientToInput);
    public Task<ClientDTO?> GetClientById(int clientId);
    public Task<ClientDTO?> UpdateClient(int clientId, JsonPatchDocument clientInfo);
}
