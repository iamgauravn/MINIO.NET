using AutoMapper;
using Azure;
using minio.Model.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minio.Models.DB;
using minio.Interface;
using Sieve.Models;
using Sieve.Services;
using System.Runtime.CompilerServices;

namespace minio.Services.Implementation;

public class BaseService<T, TDTO> : IBaseService<T, TDTO> where T : AuditableEntity where TDTO : class
{
    private readonly IUnitOfWork _unitOfWork; 
    private readonly IMapper _mapper;
    protected readonly Context _dbContext; 
    private readonly ISieveProcessor _sieveProcessor;

    public BaseService(IUnitOfWork unitOfWork, IMapper mapper, Context dbContext, ISieveProcessor sieveProcessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _dbContext = dbContext;
        _sieveProcessor = sieveProcessor;
    }

    public async Task<ActionResult> Create(TDTO entity)
    {
        var new_entity = _mapper.Map<TDTO, T>(entity);
        new_entity.SetAuditableProperties();

        await _unitOfWork.Repository<T>().Insert(new_entity);
        await _unitOfWork.SaveChangesAsync();
 
        var response = _mapper.Map<T, TDTO>(new_entity);
        return new OkObjectResult(response);
    }

    public async Task<ActionResult> Delete(int id)
    {
        var response = await _unitOfWork.Repository<T>().GetById(id);

        if (response == null)
        {
            return new NotFoundObjectResult("Entity was not found.");
        }

        response.UpdateAuditableProperties();
        _unitOfWork.Repository<T>().Delete(response);
        await _unitOfWork.SaveChangesAsync();
         
        return new OkObjectResult(_mapper.Map<T, TDTO>(response));
    }

    public async Task<ActionResult> Retrieve(SieveModel sieveModel)
    {
        var entities = _dbContext.Set<T>().Where(x => x.IsDeleted == false).AsQueryable();
        var response = _sieveProcessor.Apply(sieveModel, entities.AsNoTracking());
        var result = await response.ToListAsync();
        return new OkObjectResult(_mapper.Map<List<T>, List<TDTO>>(result));
    }
      
    public async Task<ActionResult> RetrieveByID(int id)
    {
        try
        {
            var response = await _unitOfWork.Repository<T>().GetById(id);

            if (response == null)
            {
                return new NotFoundObjectResult("Entity was not found.");
            }

            return new OkObjectResult(_mapper.Map<T, TDTO>(response));
        }
        catch (Exception e)
        {
            return new NotFoundObjectResult("Entity was not found.");
        }
    }

    public async Task<ActionResult> Update(int id, TDTO entity)
    {
        var entity_response = await _unitOfWork.Repository<T>().GetById(id);

        if (entity_response == null)
        {
            return new NotFoundObjectResult("Entity was not found.");
        }

        var mapEntity = _mapper.Map(entity, entity_response);

        mapEntity.UpdateAuditableProperties();

        _unitOfWork.Repository<T>().Update(mapEntity);

        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<T, TDTO>(mapEntity);

        return new OkObjectResult(response);
    }
     
}
