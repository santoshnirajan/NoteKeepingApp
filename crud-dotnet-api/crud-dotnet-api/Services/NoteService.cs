//using crud_dotnet_api.Data;
//using crud_dotnet_api.Interface;
//using crud_dotnet_api.Model.DTOs;
//using crud_dotnet_api.Model;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;


//namespace crud_dotnet_api.Services
//{
//    public class NoteService : INoteService
//    {
//        private readonly AppDbContext _appDbContext;
        
//        public NoteService(AppDbContext appContext)
//        {
//            _appDbContext = appContext;
            
//        }



//        public async Task CreateNoteAsync(NoteDTO noteDTO)
//        {
//            if(noteDTO == null)
//            {

//            }
//            var note = new NoteModel();
//            note.UserId = noteDTO.UserId;
//            note.CreatedOn = DateTime.Now;
//            note.Title = noteDTO.Title;
//            note.Label = noteDTO.Label;
//            note.BackgroundColor = noteDTO.BackgroundColor;
//            note.FontColor = noteDTO.FontColor;
//            note.Content = noteDTO.Content;

//            _appDbContext.Notes.Add(note);
//            await _appDbContext.SaveChangesAsync();
//        }

//        public async Task<IEnumerable<NoteModel>> GetNotesByUserIdAsync(Guid userId)
//        {
//            try
//            {
//                var notes = await _appDbContext.Notes
//                    .Where(n => n.UserId == userId)
//                    .Select(n => new NoteModel
//                    {
//                        Id = n.Id,
//                        Title = n.Title,
//                        Content = n.Content,
//                        Label = n.Label,
//                        BackgroundColor = n.BackgroundColor,
//                        FontColor = n.FontColor,
//                        CreatedOn = n.CreatedOn,
//                        ModifiedOn = n.ModifiedOn
//                    })
//                    .ToListAsync();

//                return notes;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Error retrieving notes: " + ex.Message);
//            }
//        }
//        public async Task UpdateNote(NoteEditDTO noteEditDTO)
//        {
//            var existingNote = await _appDbContext.Notes.FindAsync(noteEditDTO.Id);

//            existingNote.Title = noteEditDTO.Title;
//            existingNote.Content = noteEditDTO.Content;
//            existingNote.BackgroundColor = noteEditDTO.BackgroundColor;
//            existingNote.FontColor = noteEditDTO.FontColor;
//            existingNote.Label = noteEditDTO.Label;
//            existingNote.ModifiedOn = DateTime.Now;

//            _appDbContext.Notes.Update(existingNote);
//            await _appDbContext.SaveChangesAsync();
//        }

//        public async Task DeleteNoteAsync(Guid noteId)
//        {
//            try
//            {
//                var note = await _appDbContext.Notes.FirstOrDefaultAsync(n => n.Id == noteId);
//                if (note == null)
//                {
//                    throw new Exception("Note not found.");
//                }

//                _appDbContext.Notes.Remove(note);
//                await _appDbContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }
//    }
//}


