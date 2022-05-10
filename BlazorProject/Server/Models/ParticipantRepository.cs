using BlazorProject.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Models
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly AppDbContext appDbContext;

        public ParticipantRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        
        public async Task<Participant> AddParticipant(Participant participant)
        {
            if (participant.Course != null)
            {
                appDbContext.Entry(participant.Course).State = EntityState.Unchanged;
            }

            var result = await appDbContext.Participants.AddAsync(participant);

            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteParticipant(int participantId)
        {
            var result = await appDbContext.Participants
                .FirstOrDefaultAsync(e => e.ParticipantId == participantId);

            if (result != null)
            {
                appDbContext.Participants.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Participant> GetParticipant(int participantId)
        {
            return await appDbContext.Participants
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.ParticipantId == participantId);
        }

        public async Task<Participant> GetParticipantByEmail(string email)
        {
            return await appDbContext.Participants
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Participant>> GetParticipants()
        {
            return await appDbContext.Participants.ToListAsync();
        }

        public async Task<IEnumerable<Participant>> Search(string name, Gender? gender)
        {
            IQueryable<Participant> query = appDbContext.Participants;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name)
                            || e.LastName.Contains(name));
            }

            if (gender != null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return await query.ToListAsync();
        }

        public async Task<Participant> UpdateParticipant(Participant participant)
        {
            var result = await appDbContext.Participants
                .FirstOrDefaultAsync(e => e.ParticipantId == participant.ParticipantId);

            if (result != null)
            {
                result.FirstName = participant.FirstName;
                result.LastName = participant.LastName;
                result.Email = participant.Email;
                result.DateOfBrith = participant.DateOfBrith;
                result.Gender = participant.Gender;
                if (participant.CourseId != 0)
                {
                    result.CourseId = participant.CourseId;
                }
                else if (participant.Course != null)
                {
                    result.CourseId = participant.Course.CourseId;
                }
                result.PhotoPath = participant.PhotoPath;

                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
