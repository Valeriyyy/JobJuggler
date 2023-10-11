using JobJuggler.Application.DTOs;
using JobJuggler.Application.DTOs.Client;
using JobJuggler.Application.DTOs.Job;
using JobJuggler.Application.DTOs.Location;
using JobJuggler.Domain.Models;

namespace JobJuggler.Application.Core;

public class MappingProfiles : AutoMapper.Profile {
    public MappingProfiles() {
        CreateMap<ClientInsertDTO, Client>();
        CreateMap<JobClientDTO, Client>();
        CreateMap<Client, ClientDTO>();
        CreateMap<Location, LocationDTO>();
        CreateMap<LocationInsertDTO, Location>();
        CreateMap<JobLocationDTO, Location>();
        CreateMap<JobInsertDTO, Job>();
        CreateMap<Job, JobReadDTO>();
        CreateMap<Invoice, InvoiceDTO>();
        CreateMap<InvoiceLine, LineDTO>();
        CreateMap<PaymentMethod, PaymentMethodDTO>();
        CreateMap<LineItem, ItemDTO>().ForMember(i => i.PriceType, o => o.MapFrom(s => s.PriceType.ToString()));
    }
}