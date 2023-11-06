using AutoMapper;
using ElasticSearchKibana.Entities;

namespace ElasticSearchKibana.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDto, Product>().ReverseMap();
    }
}