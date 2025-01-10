

using minio.Model.DB;
using minio.Model.DTO;

namespace minio.Mapper;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<minio.Model.DB.FileInfo, FileInfoDTO>().ReverseMap(); 
        CreateMap<minio.Model.DB.UserMaster, UserMasterDTO>().ReverseMap(); 
    }
}
