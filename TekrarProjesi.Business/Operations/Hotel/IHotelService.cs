using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekrarProjesi.Business.Operations.Hotel.Dtos;
using TekrarProjesi.Business.Types;

namespace TekrarProjesi.Business.Operations.Hotel
{
    public interface IHotelService
    {
        Task<ServiceMessage> AddHotel(AddHotelDto dto);
    }
}
