using JobJuggler.Domain.Models;
using JobJuggler.DTO.Client;
using JobJuggler.DTO.Job;

namespace JobJugglers.Mapping;

public static class ClientMapper
{
    // ReSharper disable once InconsistentNaming
    public static List<ClientDTO> ClientsToDTO(List<Client>? clients)
    {
        var dtos = new List<ClientDTO>();
        if (clients == null || clients.Count == 0)
        {
            return dtos;
        }

        for (var i = 0; i < clients.Count; i++)
        {
            var client = clients.ElementAt(i);
            dtos.Add(ClientToDTO(client));
        }

        return dtos;
    }
    
    public static ClientDTO ClientToDTO(Client client)
    {
        return new ClientDTO
        {
            Id = client.Id,
            Guid = client.Guid,
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone
        };
    }
    
    public static Client ClientInsertToClientModel(ClientInsertDTO clientInsert)
    {
        return new Client
        {
            Name = clientInsert.Name,
            Phone = clientInsert.Phone ?? string.Empty,
            Email = clientInsert.Email
        };
    }

    public static Client? JobClientToClientModel(JobClientDTO? jobClient)
    {
        if (jobClient is null)
        {
            return null;
        }

        return new Client
        {
            // id should be null checked before, and it should not be null
            // at this point
            Id = jobClient.Id ?? 0,
            Name = jobClient.Name ?? string.Empty,
            Phone = jobClient.Phone ?? string.Empty,
            Email = jobClient.Email
        };
    }

    public static ClientProfile? ClientToProfile(Client? client)
    {
        if (client is null)
        {
            return null;
        }

        var clientProfile = new ClientProfile
        {
            Id = client.Id,
            Guid = client.Guid,
            Name = client.Name,
            Phone = client.Phone,
            Email = client.Email,
            Jobs = client.Jobs.Select(j => new ClientProfileJob
            {
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
                Location = new ClientProfileLocation
                {
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
            }).ToList()
        };
        return clientProfile;
    }
}