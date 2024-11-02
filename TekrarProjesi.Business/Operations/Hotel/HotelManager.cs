﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekrarProjesi.Business.Operations.Hotel.Dtos;
using TekrarProjesi.Business.Types;
using TekrarProjesi.Data.Entities;
using TekrarProjesi.Data.Repositories;
using TekrarProjesi.Data.UnitOfWork;

namespace TekrarProjesi.Business.Operations.Hotel
{
    public class HotelManager : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<HotelEntity> _hotelRepository;
        private readonly IRepository<HotelFeatureEntity> _hotelFeatureRepository;

        public HotelManager(IUnitOfWork unitOfWork, IRepository<HotelEntity> hotelRepository, IRepository<HotelFeatureEntity> hotelFeatureRepository)
        {
            _unitOfWork = unitOfWork;
            _hotelRepository = hotelRepository;
            _hotelFeatureRepository = hotelFeatureRepository;
        }

        public async Task<ServiceMessage> AddHotel(AddHotelDto dto)
        {
            var hasHotel = _hotelRepository.GetAll(x => x.Name.ToLower() == dto.Name.ToLower()).Any();

            if (hasHotel)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "İşlem başarısız oldu"
                };
            }

            await _unitOfWork.BeginTransaction();

            var hotelEntity = new HotelEntity
            {
                Name = dto.Name,
                Stars = dto.Stars,
                Location = dto.Location,
                AccomodationType = dto.AccomodationType,
            };

            _hotelRepository.Add(hotelEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Hotel kaydı sırasında bir sorunla karşılaşıldı");
            }

            foreach (var item in dto.FeatureId)
            {
                var hotelFeaturesEntity = new HotelFeatureEntity
                {
                    HotelId = hotelEntity.Id,
                    FeatureId = item
                };

                _hotelFeatureRepository.Add(hotelFeaturesEntity);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("HotelFeatures eklenirken bir hata ile karşılaşıldı.");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };

        }
    }
}
//1234

// 1,1, 1,2 1,3 1,4