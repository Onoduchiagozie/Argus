using AutoMapper;
using MyStudentApi.Models;
using MyStudentApi.Models.DTO;
using MyStudentApi.Repository.IRepo;

namespace MyStudentAttendance.Data;

public interface ILectureServices
{
    Task<bool> CreateClassAsync(SchoolClassDTO dto);
    Task<List<SchoolClassDTO>> GetAttendanceAsync();

}





public class LectureServices: ILectureServices
{
    private readonly ILecturesRepo _lectureRepo;
    private readonly IMapper _mapper;
    public LectureServices(ILecturesRepo lectureRepo, IMapper mapper)
    {
        _lectureRepo = lectureRepo;
        _mapper = mapper;
    }
    public async Task<List<SchoolClassDTO>> GetAttendanceAsync()
    {
        var box =await _lectureRepo.Getlectures();
       var box3 =_mapper.Map<List<SchoolClassDTO>>(box);
        return box3;
    }
    public async Task<bool> CreateClassAsync(SchoolClassDTO dto)
    {
  
            try
            {
                var entity = _mapper.Map<SchoolClass>(dto);

                await _lectureRepo.CreateLectures(entity);

                return true;
            }
            catch (Exception ex)
            {
             
                return false;
            }
    }


}