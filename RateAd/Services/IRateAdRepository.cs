using Microsoft.AspNetCore.Mvc;
using RateAd.DTO;
using RateAd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateAd.Services
{
    public interface IRateAdRepository
    {
        User GetUser(long userId); // get
        IEnumerable<User> GetUsers();//get
        void CreateUser(User user);
        //void UpdateCourse(Course course);
        //void DeleteCourse(Course course);
        //IEnumerable<Author> GetAuthors();
        //IEnumerable<Author> GetAuthors(AuthorResourceParameters authorResourseParameters);
        //Author GetAuthor(Guid authorId);
        //IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        //void AddAuthor(Author author);
        //void DeleteAuthor(Author author);
        //void UpdateAuthor(Author author);
        bool UserExist(long userId);
        bool Save();
    }
}
