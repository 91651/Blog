using AutoMapper;
using Blog.Data;
using Blog.Data.Entities;
using Blog.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.Service;

public class CommentService : ICommentService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CommentService(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> AddCommentAsync(CommentModel model)
    {
        if (!string.IsNullOrWhiteSpace(model.ParentId))
        {
            var existComment = _db.Comments.Any(c => c.Id == model.ParentId);
            if (!existComment)
            {
                return false;
            }
        }
        var comment = _mapper.Map<Comment>(model);
        comment.Id = Guid.NewGuid().ToString();
        comment.Created = DateTime.UtcNow;
        await _db.Comments.AddAsync(comment);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<List<CommentModel>> GetCommentsAsync(string ownerId, string pid)
    {
        var comments = await _db.Comments.Where(c => c.OwnerId.EndsWith(ownerId)).OrderByDescending(c => c.Created).ToListAsync();
        var list = _mapper.Map<List<CommentModel>>(comments);
        list.ForEach(m => m.Comments = list.Where(c => c.ParentId == m.Id).ToList());
        var result = list.Where(t => t.ParentId == pid).ToList();
        return result;
    }
}