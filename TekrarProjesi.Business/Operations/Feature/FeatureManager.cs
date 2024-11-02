using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekrarProjesi.Business.Operations.Feature.Dtos;
using TekrarProjesi.Business.Types;
using TekrarProjesi.Data.Entities;
using TekrarProjesi.Data.Repositories;
using TekrarProjesi.Data.UnitOfWork;

namespace TekrarProjesi.Business.Operations.Feature
{
    public class FeatureManager : IFeatureService
    {
        private readonly IRepository<FeatureEntity> _featureRepository;
        private readonly IUnitOfWork _unitOfWork;
        public FeatureManager(IRepository<FeatureEntity> featureRepository, IUnitOfWork unitOfWork)
        {
            _featureRepository = featureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceMessage> AddFeature(AddFeatureDto featureDto)
        {
            var featureEntity = _featureRepository.GetAll(x => x.Title == featureDto.Title);

            if (featureEntity.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu özellik mevcut"
                };
            }

            var feature = new FeatureEntity
            {
                Title = featureDto.Title
            };

            _featureRepository.Add(feature);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("İşlem gerçekleştiğinde bir hata oluştu");             
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = $"{feature.Title} özelliği başarıyla veritabanına eklenmiştir."
            };
        }
    }
}
