using BlazorProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Models
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<Participant>> Search(string name, Gender? gender);
        Task<IEnumerable<Participant>> GetParticipants();
        Task<Participant> GetParticipant(int participantId);
        Task<Participant> GetParticipantByEmail(string email);
        Task<Participant> AddParticipant(Participant participant);
        Task<Participant> UpdateParticipant(Participant participant);
        Task DeleteParticipant(int participantId);
    }
}
