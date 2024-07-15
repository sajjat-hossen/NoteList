using AutoMapper;
using NoteList.DomainLayer.Models;
using NoteList.ServiceLayer.Models;

namespace NoteList.ServiceLayer.MappingProfiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<NoteViewModel, Note>();
            CreateMap<Note, NoteViewModel>();
        }
    }
}
