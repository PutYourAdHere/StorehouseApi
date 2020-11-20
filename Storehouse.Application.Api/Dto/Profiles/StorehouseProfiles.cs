using AutoMapper;
using Storehouse.Domain.Contracts.Model;

namespace Storehouse.Application.Api.Dto.Profiles
{
    /// <summary>
    /// AutoMapper profile for Application Layer to Domain Layer
    /// </summary>
    public class StorehouseProfiles : Profile
    {
        /// <summary>
        /// Default Constructor. It sets the maps for both directions, Application -> Domain and Domain -> Application
        /// </summary>
        public StorehouseProfiles()
        {
            ModelToDto();

            DtoToModel();
        }

        /// <summary>
        /// Maps for Application -> Model
        /// </summary>
        private void DtoToModel()
        {
            CreateMap<ProductDto, Product>();
        }

        /// <summary>
        /// Maps for Model -> Application
        /// </summary>
        private void ModelToDto()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
