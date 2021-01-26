using mysite.Models;
using mysite.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mysite.Models.Comments;
using Microsoft.EntityFrameworkCore;

namespace mysite.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }
        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {

            return _context.Posts.ToList();
        }

        public IndexViewModel GetAllPosts(
            int pageNumber, 
            string category,
            string search)
        {
            Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };

            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);

            var query = _context.Posts.AsNoTracking().AsQueryable();

            if (!String.IsNullOrEmpty(category))
                query = query.Where(x => InCategory(x));

            if (!String.IsNullOrEmpty(search))
                query = query.Where(x => x.Title.Contains(search)
                 || x.Body.Contains(search)
                 || x.Description.Contains(search));

            int postsCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)postsCount / pageSize);

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                NextPage = postsCount > skipAmount + pageSize,
                Category = category,
                Search = search,
                Posts = query
                       .Skip(skipAmount)
                       .Take(pageSize)
                       .ToList()
            };
        }

        public Post GetPost(int id)
        {
            return _context.Posts
                .Include(p=>p.MainComments)
                         .ThenInclude(m=>m.SubComments)
                .FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public async Task<bool> SaveChangeAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void AddSubComment(SubComment comment)
        {
            _context.SubComments.Add(comment);
        }
    }
}
