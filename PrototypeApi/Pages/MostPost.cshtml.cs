using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrototypeApi;
using PrototypeApi.DbModels;

namespace PrototypeApi.Pages
{
    public class MostPostModel : PageModel
    {
        private readonly PrototypeApi.ApiContext _context;

        public MostPostModel(PrototypeApi.ApiContext context)
        {
            _context = context;
        }

        public IList<User> UserPosts { get;set; }
        public IList<Post> Posts { get; set; }
        public string DateSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            IQueryable<Post> sortedPosts = _context.Posts;
            var posts = _context.Users
                .OrderByDescending(p => p.Posts.Count)
                .Take(1);

            UserPosts = await posts.ToListAsync();
            switch (sortOrder)
            {
                case "Date":
                    sortedPosts = sortedPosts.OrderBy(p => p.CreationDate);
                    break;
                case "date_desc":
                    sortedPosts = sortedPosts.OrderByDescending(p => p.CreationDate);
                    break;
            }
            Posts = await sortedPosts.ToListAsync();

        }
    }
}
