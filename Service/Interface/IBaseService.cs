
using minio.Models.DB;
using Microsoft.AspNetCore.Mvc; 
using Sieve.Models;
using System.Runtime.CompilerServices;

namespace minio.Interface;

public interface IBaseService<T,TDTO> where T : AuditableEntity where TDTO : class
{
    Task<ActionResult> Create(TDTO entity);
    Task<ActionResult> Retrieve(SieveModel sieveModel);
    Task<ActionResult> RetrieveByID(int id);
    Task<ActionResult> Update(int id, TDTO entity);
    Task<ActionResult> Delete(int id);
}
